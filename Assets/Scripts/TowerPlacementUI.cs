using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementUI : MonoBehaviour
{
    SpriteRenderer sprite;
    Vector3Int gridPosition;
    [SerializeField] bool canPlace;

    
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<TowerAI>().enabled = false;
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
        Instantiate(Resources.Load("ProjectileTower"));
        enabled = false;
    }
}
