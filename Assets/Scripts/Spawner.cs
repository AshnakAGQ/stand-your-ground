using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int totalCost = 5;
    [SerializeField] private int currentCost = 0;
    [SerializeField] private Vector3 spawn;
    public GameObject[] creeps = new GameObject[7];
    [SerializeField] public float size = 6;
    [SerializeField] bool spawning;

    [SerializeField] private float testTimer = 0;
    [SerializeField] private float testRate = 2;

    private GameManager level;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        spawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning) SpawnWave();
    }

    public void StartWave()
    {
        totalCost += totalCost * (int) Mathf.Log10(level.wave);
        currentCost = 0;
        spawning = true;
    }

    public void SpawnWave()
    {
        if (testTimer >= testRate)
        {
            if (currentCost < totalCost)
            {
                int randomNum = (int)Random.Range(0, size);
                while (creeps[randomNum].GetComponent<CreepAI>().value + currentCost > totalCost)
                {
                    randomNum = (int)Random.Range(0, size);
                }
                currentCost += Instantiate(creeps[randomNum], spawn, Quaternion.identity).GetComponent<CreepAI>().value;
                level = GameObject.FindObjectOfType<GameManager>();
                level.creepCount += 1;
                testTimer = 0;
            }
            else if (currentCost >= totalCost && level.creepCount == 0)
            {   
                EndWave();
            }
        }
        else testTimer += Time.deltaTime;
    }

    void EndWave()
    {
        spawning = false;
        level.advanceLevel();
    }

}
