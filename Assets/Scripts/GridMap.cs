using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int gridWidth;
    public int gridLength;
    public GameObject corridorObject;
    public GameObject tJunctionObject;
    public int roomSize;
    private GridSection[] gridMap;
    
    public GridMap(int w, int l, GameObject corridor, GameObject tJunction)
    {
        gridWidth = w;
        gridLength = l;
        corridorObject = corridor;
        tJunctionObject = tJunction;
    }

    // Start is called before the first frame update
    public void Start()
    {
        gridMap = new GridSection[gridWidth*gridLength];

        for (int l = 0; l < gridLength; ++l)
        {
            for (int w = 0; w < gridWidth; ++w)
            {
                int gridPos = l + w;
                gridMap[gridPos] = new CorridorSection(corridorObject, w, l, 0, 1);
                Instantiate(gridMap[gridPos].gridSectionType, gridMap[gridPos].position, gridMap[gridPos].rotation);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
