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
        withinAngle = new List<Transform>(20000);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var f in Faces)
        {
            f.GetComponent<Renderer>().material.color = Color.white;
        }
        
        GetTransformsWithinAngle();
        foreach (Transform transform1 in withinAngle)
        {
            //raycast to see if it's not blocked
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform1.position - transform.position, out hit, 999))
            {
                if (hit.collider.gameObject.CompareTag("Face"))
                {
                    transform1.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            // transform1.GetComponent<Renderer>().material.color = Color.red;

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
