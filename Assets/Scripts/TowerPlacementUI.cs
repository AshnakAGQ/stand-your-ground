using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementUI : MonoBehaviour
{
    SpriteRenderer sprite;
    SpriteRenderer rangeIndicator;
    TowerAI settings;
    float range;
    Vector3Int gridPosition;
    [SerializeField] bool canPlace;

    
    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponentInParent<TowerAI>();
        settings.enabled = false;
        range = settings.range;
        rangeIndicator = GetComponentsInChildren<SpriteRenderer>()[1];
        rangeIndicator.enabled = true;
        rangeIndicator.transform.localScale  = new Vector3(range, range, 0) * 2;
        sprite = GetComponentInParent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward; //new Vector3 (0,0,Camera.main.transform.position.z)
        gridPosition = Vector3Int.RoundToInt(transform.position - new Vector3(0, 0, transform.position.z));

        CheckPlace();

        if (canPlace)   sprite.color = new Color(255, 255, 255, .5f);
        else            sprite.color = new Color(255, 0, 0, .5f);

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            Place();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }

    void CheckPlace()
    {
        canPlace = true;

        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower"); 
        foreach (GameObject tower in towers)
        {
            if (tower.transform.position.x == gridPosition.x && tower.transform.position.y == gridPosition.y)
            {
                canPlace = false;
            }
        }

        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject path in paths)
        {
            if (path.transform.position.x == gridPosition.x && path.transform.position.y == gridPosition.y)
            {
                canPlace = false;
            }
        }
    }

    void Place()
    {
        transform.position = gridPosition;
        sprite.color = new Color(255, 255, 255, 1f);
        GetComponentInParent<TowerAI>().enabled = true;
        Instantiate(Resources.Load("Projectile Tower")); //Remove Later
        enabled = false;
    }
}
