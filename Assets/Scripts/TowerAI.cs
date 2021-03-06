﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    enum TOWER_TYPE { projectile, AOE };
    [SerializeField] private TOWER_TYPE type = TOWER_TYPE.projectile;
    [SerializeField] public System.Type effect;
    [SerializeField] public string EffectName = "Effect";
    [SerializeField] public float projectileSpeed = 5f;
    [SerializeField] public float range = 5f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] public int cost = 200;

    public SpriteRenderer rangeIndicator;
    private Vector3 idleRotation; 
    private float timer;
    private CreepAI target;

    private AudioSource ShootSound;
    private AudioSource ExplosionSound;

    
    // Start is called before the first frame update
    void Start()
    {
        rangeIndicator = GetComponentsInChildren<SpriteRenderer>()[1];
        rangeIndicator.gameObject.transform.localScale = new Vector3(range, range, 0) * 2;
        rangeIndicator.enabled = false;
        effect = System.Type.GetType(EffectName);
        timer = cooldown;
        idleRotation = transform.up;
        Idle();

        ShootSound = GameObject.Find("ShootSound").GetComponent<AudioSource>();
        ExplosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();

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
        bool enemyFound = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Creep");
        foreach (GameObject enemy in enemies)
        {
            CreepAI creep = enemy.GetComponent<CreepAI>() as CreepAI;
            if (TargetInRange(creep))
            {
                if (type == TOWER_TYPE.AOE)
                {
                    if (timer >= cooldown)
                    {
                        enemy.gameObject.AddComponent(effect);
                        enemyFound = true;
                    }
                }
                else
                {
                    float progress = enemy.GetComponent<CreepAI>().progress;
                    if (progress > priority)
                    {
                        newTarget = creep;
                        priority = progress;
                    }
                }
            }
        }

        if (enemyFound && type == TOWER_TYPE.AOE)
        {
            ExplosionSound.Play();
            AOE aoe = Instantiate(Resources.Load("AOE", typeof(AOE)), transform.position, Quaternion.identity) as AOE;
            aoe.gameObject.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            aoe.maxSize = range;
            timer = 0;
        }

        return newTarget;
    }

    private void OnMouseEnter()
    {
        if (enabled) rangeIndicator.enabled = true;
    }

    private void OnMouseExit()
    {
        if (enabled) rangeIndicator.enabled = false;
    }

    void Shoot()
    {
        GameObject proj = Instantiate(Resources.Load("Projectile", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Projectile init = proj.GetComponent<Projectile>();
        init.speed = projectileSpeed;
        init.effect = effect;
        init.target = target;
        ShootSound.Play();
    }

    void Idle()
    {
        transform.up = idleRotation;
    }
}


