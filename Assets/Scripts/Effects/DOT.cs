using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : Effect
{
    // Start is called before the first frame update
    void Start()
    {
        duration = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<CreepAI>().health -= 50 * Time.deltaTime;
        Debug.Log("Did " + 500 * Time.deltaTime + " Damage to " + gameObject.name);
        if (duration <= 0)
            Destroy(this);
        duration -= Time.deltaTime;
    }
}
