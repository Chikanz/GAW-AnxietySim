using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomlyRotate : MonoBehaviour
{
    //rotation in euler angles
    public float magnitude = 3.0f;
    public Vector3 axis;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation *= Quaternion.Euler(
            axis.x * Random.Range(-magnitude, magnitude), 
            axis.y *Random.Range(-magnitude, magnitude), 
            axis.z * Random.Range(-magnitude, magnitude));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
