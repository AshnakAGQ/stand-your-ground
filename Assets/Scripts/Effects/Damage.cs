using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Effect
{
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<CreepAI>().health -= 100;
        if (duration <= 0)
            Destroy(this);
        duration -= Time.deltaTime;
    }
}
