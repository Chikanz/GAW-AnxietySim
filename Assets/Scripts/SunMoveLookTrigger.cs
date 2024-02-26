using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoveLookTrigger : MonoBehaviour
{
    public Transform sun;
    public MatchSunRotation matchSunRotation;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LookTrigger>().OnLook += OnOnLook;
    }

    private void OnOnLook(Transform cam)
    {
        //Check if sun is facing the right way
        if (Vector3.Dot(sun.forward, transform.forward) < 0)
        {
            print("Yeet");
            //Get the camera from the transform
            Camera camera = cam.GetComponent<Camera>();

            //Rotate the sun (directional light) to just outside of the camera's bounds 
            var movePoint = camera.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10));
            Debug.DrawLine(movePoint, Vector3.zero, Color.yellow, 10);

            sun.LookAt(-movePoint);
            matchSunRotation.enabled = true;
            
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}