using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private float range { get; set; } = 5f;
    [SerializeField] private float cooldown = 5f;
    private Vector3 idleRotation; 
    private float timer;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        idleRotation = transform.up;
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // Out of Range of Previous Target
        if (target != null && !TargetInRange(target))
        {
            target = null;
        }

        // No Target
        if (target == null)
        {
            target = FindTarget();
        }

        // Fire loop
        if (target != null)
        {
            transform.up = target.transform.position - transform.position;
            if (timer >= cooldown)
            {
                Shoot();
                timer = 0;
            }
        }
        else
        {
            Idle();
        }

        if (timer < cooldown)
        {
            timer += Time.deltaTime;
        }
    }

    bool TargetInRange(GameObject target)
    {
        return Vector2.Distance(target.transform.position, transform.position) <= range;
    }

    GameObject FindTarget()
    {
        float priority = 0;
        GameObject newTarget = null;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Creep");
        foreach (GameObject enemy in enemies)
        {
            if (TargetInRange(enemy))
            {
                float progress = enemy.GetComponent<GabeCreep>().progress;
                if (progress > priority)
                {
                    newTarget = enemy;
                    priority = progress;
                }
            }
        }

        return newTarget;
    }

    void Shoot()
    {
        Debug.Log("Shooting at " + target.name);
    }

    void Idle()
    {
        transform.up = idleRotation;
    }
}
