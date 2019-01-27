using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementUI : MonoBehaviour
{
    SpriteRenderer sprite;
    Color defaultColor;
    GameManager gameManager;
    SpriteRenderer rangeIndicator;
    TowerAI settings;
    float range;
    Vector3Int gridPosition;
    Vector2 LBounds;
    Vector2 RBounds;
    [SerializeField] bool canPlace;

    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.canPurchase = false;
        settings = GetComponentInParent<TowerAI>();
        settings.enabled = false;
        LBounds = GameObject.FindGameObjectWithTag("BoundsL").GetComponent<Transform>().position;
        RBounds = GameObject.FindGameObjectWithTag("BoundsR").GetComponent<Transform>().position;

        range = settings.range;
        rangeIndicator = GetComponentsInChildren<SpriteRenderer>()[1];
        rangeIndicator.enabled = true;
        rangeIndicator.transform.localScale  = new Vector3(range, range, 0) * 2;
        sprite = GetComponentInParent<SpriteRenderer>();
        defaultColor = sprite.color;
        sprite.color = defaultColor - new Color(0, 0, 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward; //new Vector3 (0,0,Camera.main.transform.position.z)
        gridPosition = Vector3Int.RoundToInt(transform.position - new Vector3(0, 0, transform.position.z));

        CheckPlace();

        if (canPlace) sprite.color = defaultColor - new Color(0, 0, 0, .5f);
        else sprite.color = new Color(1, 1, 1, 1) - defaultColor + new Color(0, 0, 0, .5f);

        if (canPlace && Input.GetMouseButtonDown(0))
        {
            Place();
        }

        if (Input.GetMouseButtonUp(1))
        {
            gameManager.canPurchase = true;
            Destroy(gameObject);
        }
    }

    void CheckPlace()
    {
        canPlace = true;

        if (gridPosition.x < LBounds.x || gridPosition.y < LBounds.y ||
            gridPosition.x > RBounds.x || gridPosition.y > RBounds.y)
            canPlace = false;

        if (canPlace)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            foreach (GameObject tower in towers)
            {
                if (tower.transform.position.x == gridPosition.x && tower.transform.position.y == gridPosition.y)
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace)
        {
            GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
            foreach (GameObject path in paths)
            {
                if (path.transform.position.x == gridPosition.x && path.transform.position.y == gridPosition.y)
                {
                    canPlace = false;
                }
            }
        }
    }

    void Place()
    {
        transform.position = gridPosition;
        sprite.color = defaultColor;
        settings.enabled = true;
        gameManager.ConfirmPurchase();
        Destroy(this);
    }
}
