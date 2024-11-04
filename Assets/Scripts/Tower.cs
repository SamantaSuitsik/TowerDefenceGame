using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public List<Health> targets = new List<Health>();
    public Projectile Projectile;
    public float Cooldown = 0.5f;
    private float nextSpawnTime = 0;

    private ScenarioData scenario;
    // private bool isVisible;

    private void Awake()
    {
        Events.OnScenarioLoaded += ScenarioLoaded;

    }

    private void ScenarioLoaded(ScenarioData data)
    {
        scenario = data;
    }

    private Health GetTarget()
    {
        foreach (var target in targets)
        {
            if (target == null)
            {
                targets.Remove(target);
            }
            else
            {
                return target; //returnib alati sama targeti
            }
            
        }

        return null;
    }
    void Update()
    {
        var target = GetTarget();

        if (!target.IsDestroyed() && nextSpawnTime <= Time.time)
        {
            nextSpawnTime = Time.time + Cooldown;
            Fire(target);
        }

    }
    
    private void Fire(Health target)
    {
        if (target == null)
        {
            return;
        }

        var projectile = Instantiate<Projectile>(Projectile);
        projectile.transform.position = transform.position;
        projectile.Target = target;
    }

    // private void OnBecameVisible()
    // {
    //     print("tower visible");
    //     isVisible = true;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            targets.Add(health);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            targets.Remove(health);
        }
        
    }
}
