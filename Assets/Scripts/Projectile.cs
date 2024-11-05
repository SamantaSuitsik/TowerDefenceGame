using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;

    public float Speed;
    private Health target;

    public Health Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
        // if (target.IsDestroyed())
        // {
        //     Debug.Log("Target destroyed or null, destroying projectile");
        //     Destroy(this.gameObject);
        //     return;
        // }
        if (Target == null)
        {
            Destroy();
            return;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);

        var distance = Vector3.SqrMagnitude(transform.position - Target.transform.position);
        if (distance <= float.Epsilon)
        { 
            
            Target.ReduceHealth(Damage);
            Destroy();
        }
    }

    private void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}



























