﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodMother : CreepAI
{

    // Start is called before the first frame update
    override public void Start()
    {
        health = 200;
        speed = 100;
        maxSpeed = 100;
        value = 3;
        damage = 3;
        Spawn();
    }

    /*
    override public void CheckDeath()
    {
        if (health <= 0)
        {
            dead = true;
            //bool[,] tileGridCopy = this.tileGrid;
            Vector2 floatVector = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = GetComponent<Rigidbody2D>().velocity;
            Vector2Int intVector;
            if (direction.x < 0 || direction.y < 0)
            {
                intVector = Vector2Int.FloorToInt(floatVector);
            }
            else
            {
                intVector = Vector2Int.CeilToInt(floatVector);
            }
            GameObject Baby1 = Instantiate(Resources.Load("RatCreep"),  new Vector2(intVector.x, intVector.y), Quaternion.identity) as GameObject;
            Baby1.GetComponent<RatCreep>().tileGrid = tileGrid;
            level.AddGold(value);
            KillIt();
        }   

    }

    void KillIt()
    {
        Destroy(gameObject);
    }

    void SpawnBaby(bool[,] tileGridCopy)
    {
        GameObject Baby = (GameObject)Instantiate(Resources.Load("RatCreep"), new Vector2(Mathf.Ceil(transform.position.x), Mathf.Ceil(transform.position.y)), Quaternion.identity);
        Baby.GetComponent<RatCreep>().tileGrid = tileGridCopy;
    }
    */
}
