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
            //bool[,] tileGridCopy = this.tileGrid;
            Vector2 floatVector = new Vector2(transform.position.x, transform.position.y);
            Vector2Int intVector = Vector2Int.RoundToInt(floatVector);
            GameObject Baby1 = Instantiate(Resources.Load("RatCreep"),  new Vector2(intVector.x, intVector.y), Quaternion.identity) as GameObject;
            Baby1.GetComponent<RatCreep>().tileGrid = tileGrid;
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
