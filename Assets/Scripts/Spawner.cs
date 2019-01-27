using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<string> creeps = new List<string>();
    public int totalCost;
    public int currentCost = 0;
    public Vector3 spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        totalCost = 3;
        creeps.Add("BroodMother");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnWave()
    {
        float size = creeps.Count - 1;
        while (currentCost<totalCost)
        {
            int randoNum = (int) Random.Range(0, size);
            Instantiate(Resources.Load(creeps[randoNum]), spawn, Quaternion.identity);
        }
    }
}
