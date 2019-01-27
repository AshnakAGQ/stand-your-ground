using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int totalCost = 5;
    public int currentCost = 0;
    public Vector3 spawn = new Vector3 (10, 14, 0);
    public GameObject[] creeps = new GameObject[6];
    public float size = 5;

    public float testTimer = 0;
    public float testRate = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnWave()
    {
        if (testTimer >= testRate)
        {
            while (currentCost < totalCost)
            {
                int randomNum = (int)Random.Range(0, size);
                Debug.Log(creeps[randomNum]);
                //if (creeps[randomNum].GetComponent<CreepAI>().value + currentCost <= totalCost)
                //currentCost += Instantiate(creeps[randomNum], spawn, Quaternion.identity).GetComponent<CreepAI>().value;
            }
            testTimer = 0;
        }
        else testTimer += Time.deltaTime;
    }
}
