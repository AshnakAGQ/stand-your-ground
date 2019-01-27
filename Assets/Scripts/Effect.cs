using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float duration = 1f;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("BAM");
        if (duration <= 0)
            Destroy(this);
        duration -= Time.deltaTime;
    }
}
