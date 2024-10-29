using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint Next;

    private void OnDrawGizmos()
    {
        if (Next == null) return;
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, Next.transform.position);
    }

    public Waypoint GetNextWaypoint()
    {
        
        return Next;
    }
}
