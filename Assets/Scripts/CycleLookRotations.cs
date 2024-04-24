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
    public bool cyclingEnabled = true;
    
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

        StartCoroutine(cycle());
    }

    // Update is called once per frame
    void NextPosition()
    {
        if (forcedLook) return;
        target = GetNextTarget();
        startRot = transform.rotation;
        LookAnim.Play(this);
    }

    IEnumerator cycle()
    {
        yield return new WaitForSeconds(Random.Range(CycleTimeRange.x, CycleTimeRange.y));

        while (cyclingEnabled)
        {
            NextPosition();
            yield return new WaitForSeconds(Random.Range(CycleTimeRange.x, CycleTimeRange.y));
        }
    }

    //Used for looking at the player
    private bool forcedLook = false;
    public void ForceLook(Transform t)
    {
        forcedLook = true;
        StopAllCoroutines();
        StopCoroutine(cycle());
        StopCoroutine(LookAnim.Routine());
        
        startRot = transform.rotation;
        target = t;
        LookAnim.Play(this);
    }

    Transform GetNextTarget()
    {
        int index = Random.Range(0, LookDirectionRoot.childCount);
        return LookDirectionRoot.GetChild(index);
    }
    
    
}
