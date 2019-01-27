using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] public bool visited = false;
    [SerializeField] public bool endTile = false;
    [SerializeField] public Vector2 tilePosition;
    [SerializeField] public float tilePositionX;
    [SerializeField] public float tilePositionY;

    // Start is called before the first frame update
    void Start()
    {
        tilePosition = transform.position;
        tilePositionX = transform.position.x;
        tilePositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
