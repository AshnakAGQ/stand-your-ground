using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodMother : RatCreep
{
    // Start is called before the first frame update
    void Start()
    {
        health = 150;
        speed = 50;
        value = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Spawn()
    {
        spawned = true;
        targetPosition = spawnPoint;
    }

    void checkDeath()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
            Instantiate(Resources.Load("RatCreep"), transform.position, Quaternion.identity);
            Instantiate(Resources.Load("RatCreep"), transform.position, Quaternion.identity);
        }

    }

}
