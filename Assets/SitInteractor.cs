using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitInteractor : MonoBehaviour
{
    private FirstPersonController player;
    private bool sitting = false;
    public LayerMask mask;

    private Transform currentSeat; 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
     //Raycast forward
        RaycastHit hit;
        if (!sitting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, mask))
                {
                    print(hit.transform.name);
                    if (hit.collider.gameObject.CompareTag("SitPoint"))
                    {

                        var seat = hit.collider.gameObject.transform;
                        SetSitting(true, seat);
                    }
                }
            }
        }

        if (sitting && Input.GetKeyDown(KeyCode.E))
        {
            SetSitting(false, currentSeat);
        }
    }

    private void SetSitting(bool isSitting, Transform seat)
    {
        this.sitting = isSitting;
        player.transform.position = seat.position;
        player.GetComponent<CapsuleCollider>().isTrigger = isSitting;
        player.GetComponent<Rigidbody>().isKinematic = isSitting;
        player.transform.rotation = seat.rotation;
        player.playerCanMove = !isSitting;
        player.enableHeadBob = !isSitting;
        // player.enabled = false;

        if (!sitting)
        {
            currentSeat = null;
        }
    }
}
