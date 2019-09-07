using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridMap : MonoBehaviour
{
    public int gridWidth;
    public int gridLength;
    public RoomsController roomControllerScript;
    public GameObject corridorObject;
    public GameObject tJunctionObject;
    public GameObject endObject;
    public GameObject cornerObject;
    public int roomSize;
    private GridSection[,] gridMap;

    public int innerGridSizeX = 5;
    public int innerGridSizeY= 5;
    public int innerGridDistance = 3;
    public int outerGridSizeX { get; set; } = 11;
    public int outerGridSizeY { get; set; } = 11;
    public int hallwaySegments = 10;

    private int lastPositionX = 0;
    private int lastPositionY = -1;
    private bool initRooms = false;
    private int[] currentHall;
    private int[] previousHall;
    private bool placedPrison;

    public TownsfolkController townsfolkControllerScript;
    public PotionController potionControllerScript;

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

        townsfolkControllerScript.SummonTownsfolk();
        potionControllerScript.Start();
        potionControllerScript.SpawnPotions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createHall(int x, int y, int count, int prev)
    {
        GameObject pieceType = corridorObject;
        int[] nextHall = chooseNextHall(x, y, 0);
        int tileRotation = 0;

        GameObject newRoomObject;

        if (placedPrison == false)
        {
            BaseRoom prisonRoom = new BaseRoom(roomControllerScript.roomsTypes[5]);

            prisonRoom.Rotate(0);
            newRoomObject = roomControllerScript.AddRoomToList(prisonRoom, x-2, y - 3, 0);
            placedPrison = true;
        }

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
            return;
        }



        bool firstDoorBool = false;
        bool secondDoorBool = false;

        // if its the last piece, cap it off with an end piece
        if (count == hallwaySegments - 1)
        {
            gridMap[x, y] = new CorridorSection(pieceType, x, y, 0, previousHall[2]);
            Instantiate(endObject, gridMap[x, y].position, gridMap[x, y].rotation);
        }
        else
        {
            if (roomControllerScript.roomCount < roomControllerScript.maxRooms)
            {


                // try and create rooms
                int[] tryRoom = getWallFace(previousHall[2], nextHall[2]);

                // room code
                BaseRoom roomToTry = new BaseRoom(roomControllerScript.roomsTypes[Random.Range(0, 5)]);
               
                //for testing specific rooms
                //BaseRoom roomToTry = new BaseRoom(roomControllerScript.roomsTypes[4]);

                for (int v=0;v<2;v++)
                {
                    int newX = x;
                    int newY = y;
                    int wallDoorLocation = 0;
                    
                    switch (tryRoom[v])
                    {
                        case 0:
                            newY += 1;

                            wallDoorLocation = 0;
                            break;
                        case 1:
                            newX += 1;

                            wallDoorLocation = 1;
                            break;
                        case 2:
                            newY -= roomToTry.length;

                            wallDoorLocation = 2;
                            break;
                        case 3:
                            newX -= roomToTry.width;

                            wallDoorLocation = 3;
                            break;

                    }
                    int canFit = roomControllerScript.CanRoomFit(newX, newY, roomToTry, 0);
                    if (canFit != -1)
                    {
                        newRoomObject = roomControllerScript.AddRoomToList(roomToTry, newX, newY, canFit);
                        if (pieceType.name == "corridorTypeOne")
                        {
                            switch (tryRoom[v])
                            {
                                case 0:
                                    firstDoorBool = true;
                                    break;
                                case 1:
                                    firstDoorBool = true;
                                    break;
                                case 2:
                                    secondDoorBool = true;
                                    break;
                                case 3:
                                    secondDoorBool = true;
                                    break;

                            }
                        }
                        else
                        {
                            int doorPlacement = tryRoom[v]+tileRotation;
                            if (doorPlacement > 3)
                            {
                                doorPlacement -= 4;
                            }

                            switch (doorPlacement)
                            {
                                case 0:
                                    firstDoorBool = true;
                                    break;
                                case 1:
                                    secondDoorBool = true;
                                    break;

                            }
                        }
                       
                        if (newRoomObject.GetComponent<WallPlacer>() != null)
                        {
                            newRoomObject.GetComponent<WallPlacer>().RemoveWall(wallDoorLocation);
                        }
                        break;
                    }
                }
            }

            gridMap[x, y] = new CorridorSection(pieceType, x, y, 0, tileRotation);
            GameObject newHallPiece = Instantiate(gridMap[x, y].gridSectionType, gridMap[x, y].position, gridMap[x, y].rotation);
            newHallPiece.GetComponent<DoorPlacer>().AddDoor(firstDoorBool, secondDoorBool);
        }


        previousHall = nextHall;
        currentHall = nextHall;

        if (count < hallwaySegments-1)
        {
            Debug.Log("NextHall X: " + nextHall[0] + " Y: " + nextHall[1]);
            Debug.Log("hall Count: " + count);
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
/*        if (count > 8)
        {
            return new int[3] { -1, -1, -1 };
        }*/
        int newX = x;
        int newY = y;
        int direction = -1;

        //randomizes direction
        int newDirection = Random.Range(0, 4);

        //if randomizing direction fails, run through directions starting with up
        if (count > 3)
        {
            newDirection = count - 4;
        }

        // directions
        switch (newDirection)
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

        //Debug.Log("next X: " + newX + " next Y: " + newY);

        // out of bounds sanity check
        if (newX < innerGridDistance || newY < innerGridDistance || newX>innerGridSizeX+innerGridDistance || newY>innerGridSizeY+innerGridDistance)
        {
            return chooseNextHall(x, y, count += 1);
        }

        // if position is vacant
        if (gridMap[newX, newY].coords[0] == -1)
        {
            return new int[3] { newX, newY, direction };
        }
        // else, try again
        else
        {
            if (count > 6)
            {

               // Debug.Log("try " + count);
                return new int[3] { -1, -1, -1 };

            }
            else
            {
                return chooseNextHall(x, y, count += 1);
            }
                
        }


    }

    public int[] getWallFace(int previous, int next)
    {

        if (Mathf.Abs(previous - next) % 2 == 0)
        {
            if (previous == 0 || previous == 2)
            {
                return (new int[2] { 3, 1 });
            }
            else
            {
                return (new int[2] { 0, 2 });
            }
        }
        else
        {
            switch (previous)
            {
                // from below
                case 0:
                    switch (next)
                    {
                        // to the right
                        case 1:
                            return (new int[2] { 0, 3 });
                            break;
                        // to the left
                        case 3:
                            return (new int[2] { 0, 1 });
                            break;
                    }
                    break;
                // from the left
                case 1:
                    switch (next)
                    {
                        // to above
                        case 0:
                            return (new int[2] { 1, 2 });
                            break;
                        // to below
                        case 2:
                            return (new int[2] { 1, 0 });
                            break;
                    }
                    break;
                // from the top
                case 2:
                    switch (next)
                    {
                        // to the right
                        case 1:
                            return (new int[2] { 2, 3 });
                            break;
                        // to the left
                        case 3:
                            return (new int[2] { 2, 1 });
                            break;
                    }
                    break;
                // from the right
                case 3:
                    switch (next)
                    {
                        // to above
                        case 0:
                            return (new int[2] { 3, 2 });
                            break;
                        // to below
                        case 2:
                            return (new int[2] { 3, 0 });
                            break;
                    }
                    break;
            }
        }
        return (new int[2] { -1, -1 });
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

    public void ResetGridMap()
    {

    }
}