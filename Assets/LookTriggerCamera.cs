using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTriggerCamera : MonoBehaviour
{
    public float TriggerTime = 2;
    
    public Dictionary<LookTrigger, float> LookTriggers;
    
    // Start is called before the first frame update
    void Start()
    {
        LookTriggers = new Dictionary<LookTrigger, float>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast and find an object with a LookTrigger tag
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 999))
        {
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


                    if (LookTriggers[lookTrigger] >= TriggerTime)
                    {
                        print("Triggered!");
                        lookTrigger.TriggerLook(transform);
                        LookTriggers.Remove(lookTrigger);
                    }
                }
            }
        }
    }
}
