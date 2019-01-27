using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepAI : MonoBehaviour
{
    public float progress { get; private set; } = 0;
    [SerializeField] public float health = 100;
    [SerializeField] private float speed = 5;
    [SerializeField] public bool dead = false;
    private bool spawned = false;
    [SerializeField] public Vector2 spawnPoint = new Vector2(0, 0);
    [SerializeField] public Vector2 creepPosition;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] protected bool reachingEnd;
    private float xMod = 0;
    private float yMod = 0;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }


    // Update is called once per frame
    void Update()
    {
        creepPosition = transform.position;
        progress += Time.deltaTime * speed;
        checkDeath();

    }

    private void FixedUpdate()
    {
        move();
    }

    void Spawn()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(spawnPoint);
        spawned = true;
        targetPosition = spawnPoint;
    }

    void checkDeath()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Path"))
        {
            if (collision.GetComponent<Path>().endTile)
            {
                reachingEnd = true;
            }
            collision.GetComponent<Path>().visited = true;
        }
    }

    void move()
    {
        Vector2 estimateBig = new Vector2(transform.position.x + .05f, transform.position.y + .05f);
        Vector2 estimateSmall = new Vector2(transform.position.x - .05f, transform.position.y - .05f);
        if (targetPosition.x <= estimateBig.x && targetPosition.y <= estimateBig.y
            && targetPosition.x >= estimateSmall.x && targetPosition.y >= estimateSmall.y)
        {
            if (!reachingEnd)
            {
                this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                findNextMove();
            }
            else
            {
                this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
                this.transform.position = targetPosition;
                Destroy(gameObject);
            }
        }
    }

    void findNextMove()
    {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject path in paths)
        {
            if (path.GetComponent<Path>().tilePositionX == this.transform.position.x + 1
                && path.GetComponent<Path>().tilePositionY == this.transform.position.y
                && !path.GetComponent<Path>().visited)
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed * Time.deltaTime;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionX == this.transform.position.x - 1
                && path.GetComponent<Path>().tilePositionY == this.transform.position.y
                && !path.GetComponent<Path>().visited)
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * speed * Time.deltaTime;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionY == this.transform.position.y + 1
                && path.GetComponent<Path>().tilePositionX == this.transform.position.x
                && !path.GetComponent<Path>().visited)
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed * Time.deltaTime;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionY == this.transform.position.y - 1
                && path.GetComponent<Path>().tilePositionX == this.transform.position.x
                && !path.GetComponent<Path>().visited)
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed * Time.deltaTime;
                return;
            }
        }
    }


}
