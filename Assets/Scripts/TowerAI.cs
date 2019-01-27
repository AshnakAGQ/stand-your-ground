using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    enum TOWER_TYPE { projectile, AOE };
    [SerializeField] private TOWER_TYPE type = TOWER_TYPE.projectile;
    [SerializeField] public Effect effect;
    [SerializeField] public float projectileSpeed;
    [SerializeField] private float range { get; set; } = 5f;
    [SerializeField] private float cooldown = 5f;
    private Vector3 idleRotation; 
    private float timer;
    private CreepAI target;

    
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

    bool TargetInRange(CreepAI target)
    {
        return Vector2.Distance(target.transform.position, transform.position) <= range;
    }

    CreepAI FindTarget()
    {
        float priority = 0;
        CreepAI newTarget = null;

        CreepAI[] enemies = CreepAI.FindObjectsOfType<CreepAI>();
        foreach (CreepAI enemy in enemies)
        {
            if (TargetInRange(enemy))
            {
                if (type == TOWER_TYPE.AOE)
                {
                    if (timer >= cooldown)
                    {
                        Instantiate(effect, enemy.transform);
                        timer = 0;
                    }
                }
                else
                {
                    float progress = enemy.GetComponent<CreepAI>().progress;
                    if (progress > priority)
                    {
                        newTarget = enemy;
                        priority = progress;
                    }
                }
            }
        }

        return newTarget;
    }

    void Shoot()
    {
        GameObject proj = Instantiate(Resources.Load("Projectile", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Projectile init = proj.GetComponent<Projectile>();
        init.speed = projectileSpeed;
        init.effect = effect;
        init.target = target;
    }

    void Idle()
    {
        transform.up = idleRotation;
    }
}


