using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepAI : MonoBehaviour
{
    public float progress { get; private set; } = 0;
    private float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
    }
}
