using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public Waypoint Next;
    public float Speed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        var current = transform.position;
        if (Next == null) return;
        transform.position = Vector3.MoveTowards(current, Next.transform.position, Time.deltaTime * Speed);

        if (Vector3.SqrMagnitude(current - Next.transform.position) <= float.Epsilon)
        {
            Next = Next.GetNextWaypoint();
            if (Next == null)
            {
                ReachEnd();
            }
        }
    }

    void ReachEnd()
    {
        Events.ChangeLives(Events.GetLives() - 1);
        Destroy(gameObject);
    }
}


























