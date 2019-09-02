using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerationController : MonoBehaviour
{
    public GameObject corridor;
    public GameObject tJunction;
    private int roomSize = 14;


    // the grid map class
    private GridMap grid;

    //grid width and height
    public int gridWidth;
    public int gridHeight;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridMap(gridWidth, gridHeight, corridor);
        grid.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
