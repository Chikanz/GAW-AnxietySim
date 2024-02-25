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
        
        //If camera is facing towards our forward, pop the head forward, otherwise flip the direction
        direction = Vector3.Dot(cam.forward, -transform.right) > 0 ? transform.right : -transform.right;
        Head.gameObject.SetActive(true);
        
        Head.rotation = Quaternion.LookRotation(-direction) * Quaternion.Euler(0,-90,90);
        HeadMove.Play(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}
