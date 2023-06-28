using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public float meleeRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform FireTarget;
    public RaycastHit2D[] meleeTargets;
    public Transform swingTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero,0,targetLayer);
        FireTarget = GetNearest();
        meleeTargets = Physics2D.CircleCastAll(transform.position, meleeRange, Vector2.zero, 0, targetLayer);
        swingTarget = MeleeNear();
    }

    private Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos= transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
    private Transform MeleeNear()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D target in meleeTargets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}
