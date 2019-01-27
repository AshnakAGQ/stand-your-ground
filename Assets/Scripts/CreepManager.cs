using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    public float count = 0;
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
        if (count == 0)
            StartLevel();
        else if (Time.realtimeSinceStartup >= 5 && Time.realtimeSinceStartup <= 6 && count == 1)
        {

            count++;
            Instantiate(Resources.Load("Creep"), new Vector3(-8, 3, 1), Quaternion.identity);
        }
    }

    void StartLevel()
    {
        count++;
        Instantiate(Resources.Load("Creep"), new Vector3(-8, 3, 1), Quaternion.identity);

    }
}
