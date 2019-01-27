using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCreep : CreepAI
{



    // Start is called before the first frame update
    override public void Start()
    {
        health = 100;
        speed = 50;
        value = 1;
        damage = 1;
        Spawn();

        Debug.Log("Rat Spawned!");
    }

}
