﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLogic
{
    List<int[]> path = new List<int[]>();
    public int pathCount { get; set; }

    public PathLogic ()
    {
        pathCount = 0;
    }

    public int[] getPathPoint(int pos)
    {
        if (pos > pathCount)
        {
            return new int[2] { -1, -1 };
        }
        else
        {
            return path[pos];
        }
    }
        

    public int[] getPathPoint(int w, int l)
    {
        foreach (int[] wl in path)
        {
            if(wl[0] == w && wl[1] == l)
            {
                return wl;
            }
        }
        return new int[2] { -1, -1 };
    }

    public void addPathPoint(int w, int l)
    {
        if (pathCount < 1)
        {
            path.Add(new int[2] { w, l });
            pathCount++;
        }
        else if (checkPath(w, l))
        {
            path.Add(new int[2] { w, l });
            pathCount++;
            
        }
    }

    public bool checkPath(int w, int l)
    {
        int[] previousPoint = new int[2] { path[path.Count - 1][0], path[path.Count - 1][1] };

        if (previousPoint[0] == w || previousPoint[1] == l)
        {
            return true;
        }

        return false;
    }
}


public class GridMap : MonoBehaviour
{
    public int gridWidth;
    public int gridLength;
    public GameObject corridorObject;
    public GameObject tJunctionObject;
    public GameObject endObject;
    public GameObject cornerObject;
    public int roomSize;
    private GridSection[,] gridMap;
    private PathLogic corridorPath;

    public int innerGridSizeX { get; set; } = 5;
    public int innerGridSizeY { get; set; } = 5;
    public int innerGridDistance = 3;
    public int outerGridSizeX { get; set; } = 11;
    public int outerGridSizeY { get; set; } = 11;
    public int hallwaySegments = 10;

    private int lastPositionX = 0;
    private int lastPositionY = -1;
    private bool initRooms = false;
    private int[] currentHall;
    private int[] previousHall;

    // Start is called before the first frame update
    public void Start()
    {
        outerGridSizeX = innerGridSizeX + (innerGridDistance * 2);
        outerGridSizeY = innerGridSizeY + (innerGridDistance * 2);
        gridMap = new GridSection[outerGridSizeX, outerGridSizeY];
        for (int x = 0; x < outerGridSizeX; x++)
        {
            for (int y = 0; y < outerGridSizeY; y++)
            {
                gridMap[x, y] = new GridSection();
            }
        }
       
        previousHall = new int[3] { innerGridDistance, innerGridDistance-1, 0 };
        createHall(innerGridDistance, innerGridDistance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createHall(int x, int y, int count, int prev)
    {
        //Debug.Log("creating room " + count);

        GameObject pieceType = corridorObject;
        int[] nextHall = chooseNextHall(x, y, 0);
        int tileRotation = 0;
        //int rotationForNextTime = 0;


        //rotation code
        if (Mathf.Abs(nextHall[2] - previousHall[2]) == 1|| Mathf.Abs(nextHall[2] - previousHall[2]) == 3)
        {
            // corner
            pieceType = cornerObject;
            tileRotation = nextHall[2];
            switch (previousHall[2])
            {
                    // from below
                case 0:
                    switch (nextHall[2])
                    {
                        // to the right
                        case 1:
                            tileRotation = 1;
                            //print("zero one");
                            break;
                        // to the left
                        case 3:
                            tileRotation = 0;
                            break;
                    }
                    break;
                    // from the left
                case 1:
                    switch (nextHall[2])
                    {
                        // to above
                        case 0:
                            tileRotation = 3;
                            break;
                        // to below
                        case 2:
                            tileRotation = 0;
                            break;
                    }
                    break;
                    // from the top
                case 2:
                    switch (nextHall[2])
                    {
                        // to the right
                        case 1:
                            tileRotation = 2;
                            break;
                        // to the left
                        case 3:
                            tileRotation = 3;
                            break;
                    }
                    break;
                    // from the right
                case 3:
                    switch (nextHall[2])
                    {
                        // to above
                        case 0:
                            tileRotation = 2;
                            break;
                        // to below
                        case 2:
                            tileRotation = 1;
                            break;
                    }
                    break;
            }

        }
        else
        {
            tileRotation = nextHall[2];
            // straight
            pieceType = corridorObject;

        }

        if (nextHall[2] == -1)
        {
            //Debug.Log("giving up on creating hall " + count);
            return;
        }


        
        // if its the last piece, cap it off with an endpice
        if (count == hallwaySegments - 1)
        {
            gridMap[x, y] = new CorridorSection(pieceType, x, y, 0, previousHall[2]);
            Instantiate(endObject, gridMap[x, y].position, gridMap[x, y].rotation);
        }
        else
        {
            gridMap[x, y] = new CorridorSection(pieceType, x, y, 0, tileRotation);
            Instantiate(gridMap[x, y].gridSectionType, gridMap[x, y].position, gridMap[x, y].rotation);
        }


        previousHall = nextHall;
        currentHall = nextHall;

        if (count < hallwaySegments-1)
        {
            createHall(nextHall[0], nextHall[1], count+=1, nextHall[2]);
        }
        else
        {
            //Debug.Log("finished creating " + count + " halls");
        }
    }

    public int[] chooseNextHall(int x, int y, int count)
    {
        //Debug.Log("try " + count);
        if (count > 8)
        {
            return new int[3] { -1, -1, -1 };
        }
        int newX = x;
        int newY = y;
        int direction = -1;

        switch (Random.Range(0, 4))
        {
            // up
            case (0):
                newY++;
                direction = 0;
                break;
            // Right
            case (1):
                newX++;
                direction = 1;
                break;
            // Down
            case (2):
                newY--;
                direction = 2;
                break;
            // Left
            case (3):
                newX--;
                direction = 3;
                break;
        }

        // out of bounds sanity check
        if (newX < 0 || newY < 0 || newX>=innerGridSizeX+innerGridDistance || newY>=innerGridSizeY+innerGridDistance)
        {
            return chooseNextHall(x, y, count++);
        }

        // if position is vacant
        if (gridMap[newX, newY].coords[0] == -1)
        {
            return new int[3] { newX, newY, direction };
        }
        // else, try again
        else
        {
            return chooseNextHall(x, y, count+=1);
        }


    }

    public GridSection[,] GetGridMap()
    {
        return gridMap;
    }

    public GridSection GetGridSection(int x, int y)
    {
        if (x < 0 || y < 0 || x >= innerGridSizeX+innerGridDistance || y >= innerGridSizeY+innerGridDistance)
        {
            return new GridSection();
        }
        return gridMap[x, y];
    }
}