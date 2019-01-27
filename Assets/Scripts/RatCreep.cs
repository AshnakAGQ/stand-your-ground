using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCreep : CreepAI
{



    // Start is called before the first frame update
    override public void Start()
    {
        health = 150;
        speed = 50;
        value = 2;
        damage = 2;
        Spawn();
    }

}
