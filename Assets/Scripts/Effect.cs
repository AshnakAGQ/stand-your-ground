using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private CreepAI target;
    public float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("BAM");
        if (duration <= 0)
            Destroy(gameObject);
        duration -= Time.deltaTime;
    }
}
