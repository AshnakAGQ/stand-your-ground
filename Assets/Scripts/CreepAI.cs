using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreepAI : MonoBehaviour
{
    public float progress { get; private set; } = 0;
    [SerializeField] public float health = 100;
    [SerializeField] public float maxSpeed = 50;
    [SerializeField] public float speed = 50;
    [SerializeField] public bool dead = false;
    protected bool spawned = false;
    [SerializeField] public Vector2 creepPosition;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] protected bool reachingEnd;
    public uint value = 1; 
    public Path[] paths;
    //public List<bool> counting = new List<bool>();
    private bool[,] tileGrid = new bool[20,20];
    [SerializeField] protected uint damage = 1;



    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        paths = FindObjectsOfType<Path>();
        foreach (Path path in paths)
        {
            tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = false;
            
            
            
            //if (firstCount != 0)
            //{
                //counting.Add(false);
            //}
            //firstCount++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        creepPosition = transform.position;
    }

    void FixedUpdate()
    {
        CheckDeath();
        Move();
    }

    virtual public void Spawn()
    {
        spawned = true;
        targetPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void CheckDeath()
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
        }
    }


    void Move()
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
                FindNextMove();
            }
            else
            {
                DamagePlayer();
            }
        }
    }


    void DamagePlayer()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        GameManager l = FindObjectOfType<GameManager>();
        if (damage > l.playerHealth)
        {
            l.playerHealth = 0;
        }
        else
        {
            l.playerHealth -= damage;
        }
        Destroy(gameObject);
    }


    void FindNextMove()
    {

        int loopCount = 0;

        foreach (Path path in paths)
        {
            if (path.GetComponent<Path>().tilePositionX == this.transform.position.x + 1
                && path.GetComponent<Path>().tilePositionY == this.transform.position.y
                && !tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)])
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed * Time.deltaTime;
                tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = true;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionX == this.transform.position.x - 1
                && path.GetComponent<Path>().tilePositionY == this.transform.position.y
                && !!tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)])
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * speed * Time.deltaTime;
                tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = true;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionY == this.transform.position.y + 1
                && path.GetComponent<Path>().tilePositionX == this.transform.position.x
                && !tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)])
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed * Time.deltaTime;
                tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = true;
                return;
            }
            else if (path.GetComponent<Path>().tilePositionY == this.transform.position.y - 1
                && path.GetComponent<Path>().tilePositionX == this.transform.position.x
                && !tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)])
            {
                targetPosition = path.GetComponent<Path>().tilePosition;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed * Time.deltaTime;
                tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = true;
                return;
            }
            loopCount++;
        }
    }


}
