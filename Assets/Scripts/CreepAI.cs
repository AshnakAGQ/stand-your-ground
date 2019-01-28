using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreepAI : MonoBehaviour
{
    public float progress { get; private set; } = 0;
    [SerializeField] public float health = 100;
    [SerializeField] public float maxSpeed = 100;
    [SerializeField] public float speed = 100;
    [SerializeField] public bool dead = false;
    protected bool spawned = false;
    [SerializeField] public Vector2 creepPosition;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] protected bool reachingEnd;
    public int value = 1; 
    public Path[] paths;
    public bool[,] tileGrid = new bool[30,20];
    [SerializeField] protected int damage = 1;
    public GameManager level;

    private void Awake()
    {
        paths = FindObjectsOfType<Path>();
        foreach (Path path in paths)
        {
            tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)] = false;
        }
        tileGrid[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)] = true;
    }

    // Start is called before the first frame update
    virtual public void Start()
    {
        Spawn();
    }


    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        creepPosition = transform.position;
    }

    protected void LateUpdate()
    {
        Vector2 test = GetComponent<Rigidbody2D>().velocity;
    
        if (test.x > 0) GetComponent<Animator>().SetInteger("direction", 0);
        else if (test.x < 0) GetComponent<Animator>().SetInteger("direction", 2);
        else if (test.y > 0) GetComponent<Animator>().SetInteger("direction", 1);
        else if (test.y < 0) GetComponent<Animator>().SetInteger("direction", 3);
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

    virtual public void CheckDeath()
    {
        if (health <= 0)
        {
            dead = true;
            level = GameObject.FindObjectOfType<GameManager>();
            level.creepCount -= 1;
            level.AddGold(value);
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
        Vector2 estimateBigX = new Vector2(transform.position.x + .05f, transform.position.y);
        Vector2 estimateBigY = new Vector2(transform.position.x, transform.position.y + .05f);
        Vector2 estimateSmallX = new Vector2(transform.position.x - .05f, transform.position.y);
        Vector2 estimateSmallY = new Vector2(transform.position.x, transform.position.y - .05f);

        if (((targetPosition.x <= estimateBigX.x && targetPosition.y == estimateBigX.y 
            || (targetPosition.y <= estimateBigY.y && targetPosition.x == estimateBigY.x))
            && ((targetPosition.x >= estimateSmallX.x && targetPosition.y == estimateSmallX.y) 
            || (targetPosition.y >= estimateSmallY.y && targetPosition.x == estimateSmallY.x))))
        {
            if (!reachingEnd)
            {
                this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                FindNextMove();
            }
            else if(reachingEnd)
            {
                DamagePlayer();
            }
        }
    }


    void DamagePlayer()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        GameManager l = FindObjectOfType<GameManager>();
        l.DamagePlayer(damage);
        l.creepCount -= 1;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    void FindNextMove()
    {

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
                && !tileGrid[Mathf.RoundToInt(path.tilePositionX), Mathf.RoundToInt(path.tilePositionY)])
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
        }
    }


}
