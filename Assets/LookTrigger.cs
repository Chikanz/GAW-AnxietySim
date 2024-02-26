using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookTrigger : MonoBehaviour
{
    public delegate void LookEvent(Transform Camera);
    public event LookEvent OnLook;

    public float duration = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerLook(Transform Camera)
    {
        OnLook?.Invoke(Camera);
        //Unity just crashes lol 
        // OnLook.Invoke(Camera);
    }
}
