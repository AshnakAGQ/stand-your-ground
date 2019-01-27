using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public CreepAI target;
    public float speed = 5;
    public System.Type effect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target == null) Destroy(gameObject);
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Creep")
        {
            if (target != null) collider.gameObject.AddComponent(effect);
            Destroy(gameObject);
        }
    }
}
    