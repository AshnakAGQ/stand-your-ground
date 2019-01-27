using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float duration;
    public float maxSize;
    public float time;
    Vector3 init;

    // Start is called before the first frame update
    void Start()
    {
        init = transform.localScale;
        transform.localScale = init * 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = init * time * maxSize * 2 / duration;
        time += Time.deltaTime;
        if (time >= duration) Destroy(gameObject);
    }
}
