using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float visionAngle;

    private GameObject[] Faces;

    private List<Transform> withinAngle;

    // Start is called before the first frame update
    void Awake()
    {
        Faces = GameObject.FindGameObjectsWithTag("Face");
        withinAngle = new List<Transform>(2000);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var f in Faces)
        {
            f.GetComponent<Renderer>().material.color = Color.white;
        }
        
        GetTransformsWithinAngle();
        foreach (Transform face in withinAngle)
        {
            //Check to see if it's facing us with dot product
            if (Vector3.Dot(-transform.forward, face.forward) < 0.25f) return;

            //raycast to see if it's not blocked
            RaycastHit hit;
            if (Physics.Raycast(transform.position, face.position - transform.position, out hit, 99999))
            {
                // print(hit.transform.name);
                if (hit.collider.gameObject.CompareTag("Face"))
                {

                    StartCoroutine(ForceLook(face, 0.5f, 2.1f));
                    GameMan.instance.Setdeath("You made eye contact");

                    try
                    {
                        hit.collider.gameObject.GetComponentInParent<CycleLookRotations>().enabled = false;
                        hit.collider.gameObject.GetComponentInParent<CycleLookRotations>().ForceLook(transform);
                    }
                    catch (Exception e)
                    {
                        print("Couldn't find CycleLookRotations");
                    }
                    
                    //Force camera to look at enemy's eyes

                    return;
                }
            }
        }
    }
    
    //Rotate the camera to face the target
    IEnumerator ForceLook(Transform target, float duration, float hangTime)
    {
        float time = 0;
        Quaternion startRot = transform.rotation;
        while (time < duration)
        {
            Quaternion endRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(startRot, endRot,Mathf.Clamp(time / duration, 0, 1));
            time += Time.deltaTime;
            yield return null;
        }

        while (time < hangTime)
        {
            Quaternion endRot = Quaternion.LookRotation(target.position - transform.position);

            transform.rotation = Quaternion.Slerp(startRot, endRot,1);

            time += Time.deltaTime;
            yield return null;
        }
    }
        public void GetTransformsWithinAngle()
    {
        withinAngle.Clear();

        foreach (GameObject target in Faces)
        {
            var t = target.transform;
            Vector3 directionToTarget = (t.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            
            if (angle <= visionAngle)
            {
                withinAngle.Add(t);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 forward = transform.forward * 10;
        Vector3 left = Quaternion.Euler(0, visionAngle, 0) * forward;
        Vector3 right = Quaternion.Euler(0, -visionAngle, 0) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forward);
        Gizmos.DrawRay(transform.position, left);
        Gizmos.DrawRay(transform.position, right);
    }
}
