using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantMoveSunTrigger : MonoBehaviour
{
    public MatchSunRotation SunRotation; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LookTrigger>().OnLook += OnOnLook;
    }

    private void OnOnLook(Transform camera1)
    {
        SunRotation.enabled = true;
        GetComponent<LookTrigger>().duration = 2; //Set it so it works if the player looks out the window again
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
