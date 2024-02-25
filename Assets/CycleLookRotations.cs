using System.Collections;
using System.Collections.Generic;
using dook.tools.animatey;
using UnityEngine;

public class CycleLookRotations : MonoBehaviour
{
    //The root at which there will be other objects that will be looked at
    public Transform LookDirectionRoot;
    
    public Vector2 CycleTimeRange;

    public Animatey LookAnim;

    private Quaternion startRot;
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        //Fuck it
        if(LookDirectionRoot == null)
        {
            enabled = false;
            return;
        }

        LookAnim.Action += (val, args) =>
        {
            transform.rotation =
                Quaternion.Lerp(startRot, Quaternion.LookRotation(target.position - transform.position), val);
        };
        transform.rotation = Quaternion.LookRotation(GetNextTarget().position - transform.position);
            
        Invoke(nameof(NextPosition),Random.Range(CycleTimeRange.x, CycleTimeRange.y));
    }

    // Update is called once per frame
    void NextPosition()
    {
        target = GetNextTarget();
        startRot = transform.rotation;
        LookAnim.Play(this);
        Invoke(nameof(NextPosition),Random.Range(CycleTimeRange.x, CycleTimeRange.y));
    }

    Transform GetNextTarget()
    {
        int index = Random.Range(0, LookDirectionRoot.childCount);
        return LookDirectionRoot.GetChild(index);
    }
    
    
}
