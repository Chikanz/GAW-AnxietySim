using System;
using System.Collections;
using System.Collections.Generic;
using dook.tools.animatey;
using UnityEngine;

[RequireComponent(typeof(LookTrigger))]
public class PopHead : MonoBehaviour
{
    public Animatey HeadMove;
    public Transform Head;
    private Vector3 _startPos;

    private Vector3 direction;
    public float distance = 0.5f;

    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = Head.position;
        Head.gameObject.SetActive(false);
        HeadMove.Action += (val, args) =>
        {
            Head.position = Vector3.Lerp(_startPos, _startPos + (direction * distance), val);
        };

        GetComponent<LookTrigger>().OnLook += Trigger;

    }

    [ContextMenu("Test")]
    public void test()
    {
        Trigger(transform);
    }

    public void Trigger(Transform cam)
    {
        if (hasPlayed) return;
        hasPlayed = true;
        
        print("Triggered pop head!");
        
        //If camera is facing towards our forward, pop the head forward, otherwise flip the direction
        // direction = Vector3.Dot(cam.forward, -transform.right) > 0 ? transform.right : -transform.right;
        var dot = Vector3.Dot(cam.forward, -transform.right);
        if (dot > 0)
        {
            direction = transform.right;
            // Head.rotation *= Quaternion.Euler(0, 0, 180);
        }
        else
        {
            direction = -transform.right;
        }
        Head.gameObject.SetActive(true);

        // Head.rotation = Quaternion.LookRotation(-direction, transform.up);
        HeadMove.Play(this);

        HeadMove.ChainAt(0.4f);
        HeadMove.Chain += (sender, args) =>
        {
            ProgressMan.instance.SetGoal(ProgressMan.GoalStrings.underSeat, "wtf was that???", 7);
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}
