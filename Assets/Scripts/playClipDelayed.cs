using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playClipDelayed : MonoBehaviour
{
    public AudioClip clip;
    public float delay = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(playClip), delay);
    }

    void playClip()
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
