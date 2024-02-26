using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTriggerCamera : MonoBehaviour
{
    public float TriggerTime = 2;
    public float sphereRadius = 2;
    public float maxDistance = 999;

    private int layerMask;
    
    public Dictionary<LookTrigger, float> LookTriggers;
    
    RaycastHit[] results = new RaycastHit[10];
    
    // Start is called before the first frame update
    void Start()
    {
        LookTriggers = new Dictionary<LookTrigger, float>();
        layerMask = ~LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast and find an object with a LookTrigger tag
       int numHits = Physics.SphereCastNonAlloc(transform.position, sphereRadius, transform.forward, results, maxDistance, layerMask);
       
       // Process the results
       for (int i = 0; i < numHits; i++)
       {
           RaycastHit hit = results[i];
           if (hit.collider.gameObject.CompareTag("LookTrigger") || hit.collider.gameObject.CompareTag("SitPoint"))
           {
               var lookTrigger = hit.collider.gameObject.GetComponentInParent<LookTrigger>();
               if (!lookTrigger)
               {
                   hit.collider.gameObject.GetComponent<LookTriggerCamera>();
               }
               
               if (lookTrigger != null)
               {
                   if (!LookTriggers.ContainsKey(lookTrigger))
                   {
                       LookTriggers.Add(lookTrigger, 0);
                   }
                   LookTriggers[lookTrigger] += Time.deltaTime;
       
                   if (LookTriggers[lookTrigger] >= lookTrigger.duration)
                   {
                       print("Triggered!");
                       lookTrigger.TriggerLook(transform);
                       LookTriggers.Remove(lookTrigger);
                   }
               }
           }
       }
    }

  
    void OnDrawGizmosSelected()
    {
        // Set the color of the Gizmos
        Gizmos.color = Color.red;

        // Calculate the endpoint of the sphere cast
        Vector3 endPosition = transform.position + transform.forward * maxDistance;

        // Draw the starting sphere
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        // Optionally draw a line representing the direction of the cast
        Gizmos.DrawLine(transform.position, endPosition);

        // Draw the ending sphere
        Gizmos.DrawWireSphere(endPosition, sphereRadius);
    }
}
