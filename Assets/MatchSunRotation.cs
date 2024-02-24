using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSunRotation : MonoBehaviour
{
    public Transform playerCamera;
    public Transform sun;
    public float speed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Rotate this object slowly so the sun is always in the player's eyes lol
    //Only rotate on Y axis
    void Update()
    {
        //Project player forward vector onto the XZ plane
        Vector3 playerForward = playerCamera.forward;
        playerForward.y = 0;
        playerForward.Normalize();
        
       //Flip it so it's behind the player
        var playerBacwards = playerForward *= -1;
        Debug.DrawLine(playerBacwards, playerBacwards * 10, Color.red);
        
        //Rotate the world root only on the Y axis to make the player look at the sun
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerBacwards), speed * Time.deltaTime);

        //Only get the y component
        var goalRot = Quaternion.LookRotation(playerBacwards).eulerAngles.y;
        sun.rotation = Quaternion.Slerp(sun.rotation, Quaternion.Euler(sun.rotation.eulerAngles.x, goalRot, sun.rotation.eulerAngles.z), speed * Time.deltaTime);
        
    }
}
