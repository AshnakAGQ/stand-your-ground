using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabeCreep : MonoBehaviour
{
    public float progress { get; private set; } = 0;
    [SerializeField] public float health = 100;
    private float speed = 5;
    [SerializeField] public bool dead = false;
    private bool spawned = false;
    [SerializeField] public Vector2 spawnPoint = new Vector2(0, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        checkDeath();
        move();
    }

    void Spawn()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(spawnPoint);
        spawned = true;
    }

    void checkDeath()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }
    }

    void move()
    {
        
    }
}
