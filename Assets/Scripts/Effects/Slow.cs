using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Effect
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        duration = 5f;
        speed = GetComponentInParent<CreepAI>().speed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<CreepAI>().speed = speed;
        if (duration <= 0)
        {
            GetComponentInParent<CreepAI>().speed = speed * 2;
            Destroy(this);
        }
        duration -= Time.deltaTime;
    }
}
