using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int totalCost = 5;
    [SerializeField] private int currentCost = 0;
    [SerializeField] private Vector3 spawn;
    public GameObject[] creeps = new GameObject[6];
    [SerializeField] public float size = 5;

    [SerializeField] private float testTimer = 0;
    [SerializeField] private float testRate = 5;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        if (testTimer >= testRate)
        {
            Debug.Log("enter spawn wave");
            while (currentCost < totalCost)
            {
                int randomNum = (int)Random.Range(0, size);
                while (creeps[randomNum].GetComponent<CreepAI>().value + currentCost > totalCost)
                {
                    randomNum = (int)Random.Range(0, size);
                }
                currentCost += Instantiate(creeps[randomNum], spawn, Quaternion.identity).GetComponent<CreepAI>().value;
            }
            testTimer = 0;
        }
        else testTimer += Time.deltaTime;
    }
}
