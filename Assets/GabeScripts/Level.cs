using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float count = 0;
    [SerializeField] public uint playerHealth = 10;
    [SerializeField] public Vector3 spawn = new Vector3(1, 1, -1);

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            GameOver();
        }
        else if (count == 0)
        {
            StartLevel();
        }
        else if (Time.realtimeSinceStartup >= 5 && Time.realtimeSinceStartup <= 6 && count == 1)
        {
            count++;
            Instantiate(Resources.Load("BroodMother"), spawn, Quaternion.identity);
        }
        Debug.Log(playerHealth);
    }

    void StartLevel()
    {
        count++;
        Instantiate(Resources.Load("RatCreep"), spawn, Quaternion.identity);

    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
    }


}
