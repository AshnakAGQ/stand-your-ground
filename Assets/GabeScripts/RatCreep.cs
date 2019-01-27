using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCreep : GabeCreep
{
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        speed = 50;
        value = 1;
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
}
