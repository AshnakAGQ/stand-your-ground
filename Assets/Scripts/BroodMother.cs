using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodMother : CreepAI
{

    // Start is called before the first frame update
    override public void  Start()
    {
        health = 150;
        speed = 50;
        value = 2;
        damage = 2;
        Spawn();
    }


    override public void CheckDeath()
    {
        if (health <= 0)
        {
            dead = true;
            Instantiate(Resources.Load("RatCreep"), transform.position, Quaternion.identity);
            Instantiate(Resources.Load("RatCreep"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

}
