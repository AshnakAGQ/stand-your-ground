using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodMother : CreepAI
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


    override public void CheckDeath()
    {
        if (health <= 0)
        {
            dead = true;
            bool[,] tileGridCopy = this.tileGrid;
            GameObject Baby1 = (GameObject) Instantiate(Resources.Load("RatCreep"), new Vector2(Mathf.Ceil(transform.position.x), Mathf.Ceil(transform.position.y)), Quaternion.identity);
            Baby1.GetComponent<RatCreep>().tileGrid = tileGridCopy;
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
}
