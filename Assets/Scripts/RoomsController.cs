using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseRoom
{
    public int width;
    public int length;

    public GameObject roomObject;
    public Vector3 position;
    public Quaternion rotation;
    public int[] coords { get; set; }
    public int rot;
    public int roomSize = 7 * 2;

    public int[] bottomLeft = new int[2];
    public int[] topRight = new int[2];
    

    public BaseRoom(int x, int y, int z, int rot, int w, int l)
    {
        width = w;
        length = l;
        coords = new int[3] { x, y, z };
        if (width%2 == 0)
        {
            bottomLeft[0] = x - (width / 2);
        }
        else
        {
            bottomLeft[0] = x - ((width - 1) / 2);
        }

        if (length % 2 == 0)
        {
            bottomLeft[1] = y - (length / 2);
        }
        else
        {
            bottomLeft[1] = y - ((length - 1) / 2);
        }

        position = new Vector3(x * roomSize * (width/2), z * roomSize, y * roomSize * (length/2));

        switch (rot)
        {

            //forward
            case 0:
                rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            //right
            case 1:
                rotation = Quaternion.Euler(-90f, -90f, 0f);
                break;
            //backwards
            case 2:
                rotation = Quaternion.Euler(-90f, -180f, 0f);
                break;
            //left
            case 3:
                rotation = Quaternion.Euler(-90f, 90f, 0f);
                break;
            default:
                break;
        }
    }
    public BaseRoom(BaseRoom br)
    {
        width = br.width;
        length = br.length;
        roomObject = br.roomObject;
        roomSize = br.roomSize;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void Rotate(int rot)
    {
        switch (rot)
        {

            //forward
            case 0:
                rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            //right
            case 1:
                rotation = Quaternion.Euler(-90f, -90f, 0f);
                break;
            //backwards
            case 2:
                rotation = Quaternion.Euler(-90f, -180f, 0f);
                break;
            //left
            case 3:
                rotation = Quaternion.Euler(-90f, 90f, 0f);
                break;
            default:
                break;
        }
    }

    public virtual void SetPosition(int x, int y, int z)
    {
        int newWidth = 0;
        int newLength = 0; //height
        coords = new int[3] { x, y, z };
        
        if (width % 2 == 0)
        {
            bottomLeft[0] = x - (width / 2);
            newWidth = (width/2)*(roomSize/ 2);

        }
        else
        {
            bottomLeft[0] = x - ((width - 1) / 2);
        }

        if (length % 2 == 0)
        {
            bottomLeft[1] = y - (length / 2);
            newLength = length * (roomSize / 2);
        }
        else
        {
            bottomLeft[1] = y - ((length - 1) / 2);
        }
        position = new Vector3(x * roomSize * width+newWidth, z * roomSize, y * roomSize * length+newLength);// +length/2);

    }
}
public class RoomsController : MonoBehaviour
{
    public GridMap gridMapScript;
    public int maxRooms;
    private int roomCount;
    public List<BaseRoom> roomsTypes = new List<BaseRoom>();
    public List<BaseRoom> roomsInPlay = new List<BaseRoom>();
    
    // Start is called before the first frame update
    void Start()
    {
        int x;
        int y = -1;

        //looping through bellow grid
        for (x = 0; x < gridMapScript.gridSizeX; x++)
        {
            if (roomCount < maxRooms)
            {
                if (CanRoomFit(x, y, roomsTypes[0]))
                {
                    roomsInPlay.Add(new BaseRoom(roomsTypes[0]));
                    roomsInPlay[roomCount].SetPosition(x, y, 0);
                    roomsInPlay[roomCount].Rotate(0);
                    Instantiate(roomsInPlay[roomCount].roomObject, roomsInPlay[roomCount].position,
                        roomsInPlay[roomCount].rotation);
                    roomCount++;
                }
            }
        }

        x = -1;
        //looping through left of grid
        for (y = 0; y < gridMapScript.gridSizeY; y++)
        {
            if (roomCount < maxRooms)
            {
                if (CanRoomFit(x, y, roomsTypes[0]))
                {
                    roomsInPlay.Add(new BaseRoom(roomsTypes[0]));
                    roomsInPlay[roomCount].SetPosition(x, y, 0);
                    roomsInPlay[roomCount].Rotate(0);
                    Instantiate(roomsInPlay[roomCount].roomObject, roomsInPlay[roomCount].position,
                        roomsInPlay[roomCount].rotation);
                    roomCount++;
                }
            }
        }

        //looping through above grid
        y = gridMapScript.gridSizeY + 1;
        for (x = 0; x < gridMapScript.gridSizeX; x++)
        {
            if (roomCount < maxRooms)
            {
                if (CanRoomFit(x, y, roomsTypes[0]))
                {
                    roomsInPlay.Add(new BaseRoom(roomsTypes[0]));
                    roomsInPlay[roomCount].SetPosition(x, y, 0);
                    roomsInPlay[roomCount].Rotate(0);
                    Instantiate(roomsInPlay[roomCount].roomObject, roomsInPlay[roomCount].position,
                        roomsInPlay[roomCount].rotation);
                    roomCount++;
                }
            }
        }

        //looping through right of grid
        x = gridMapScript.gridSizeX + 1;
        for (y = 0; y < gridMapScript.gridSizeY; y++)
        {
            if (roomCount < maxRooms)
            {
                if (CanRoomFit(x, y, roomsTypes[0]))
                {
                    roomsInPlay.Add(new BaseRoom(roomsTypes[0]));
                    roomsInPlay[roomCount].SetPosition(x, y, 0);
                    roomsInPlay[roomCount].Rotate(0);
                    Instantiate(roomsInPlay[roomCount].roomObject, roomsInPlay[roomCount].position,
                        roomsInPlay[roomCount].rotation);
                    roomCount++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanRoomFit(int targetX, int targetY, BaseRoom targetRoom)
    {
        int roomBottomLeftX, roomBottomLeftY, roomTopRightX, roomTopRightY;

        //changing where the bounds of bottom left and top right are

        // if X is to the right of the grid middle
        if (targetX > (gridMapScript.gridSizeX/2))
        {
            roomBottomLeftX = targetX;
            roomTopRightX = targetX + targetRoom.width;
        }

        // if X is to the left of the grid middle
        else if (targetX < (gridMapScript.gridSizeX / 2))
        {
            roomTopRightX = targetX;
            roomBottomLeftX = targetX - targetRoom.width;
        }
        else
        {
            roomTopRightX = targetX;
            roomBottomLeftX = targetX - targetRoom.width;
        }

        // Y is above the grid middle
        if (targetY > (gridMapScript.gridSizeY/2))
        {
            roomBottomLeftY = targetY;
            roomTopRightY = targetY + targetRoom.length;
        }

        // if Y is bellow the grid middle
        else if (targetY < (gridMapScript.gridSizeY / 2))
        {
            roomTopRightY = targetY;
            roomBottomLeftY = targetY - targetRoom.length;
        }
        else
        {
            roomTopRightY = targetY;
            roomBottomLeftY = targetY - targetRoom.length;
        }

        // checks which side left or right of the middle of the grid the room is
        if (roomBottomLeftX > (gridMapScript.gridSizeX / 2))
        {
            for (int x = roomBottomLeftX; x < roomTopRightX; x++)
            {
                //checks which side above or bellow the grid middle
                if (roomBottomLeftY > (gridMapScript.gridSizeY / 2))
                {
                    for (int y = roomBottomLeftY; y < roomTopRightY; y++)
                    {
                        if (gridMapScript.GetGridSection(x,y).coords[0] != -1)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int y = roomTopRightY; y < roomBottomLeftY; y++)
                    {
                        if (gridMapScript.GetGridSection(x, y).coords[0] != -1)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            for (int x = roomTopRightX; x < roomBottomLeftX; x++)
            {
                //checks which side above or bellow the grid middle
                if (roomBottomLeftY > (gridMapScript.gridSizeY / 2))
                {
                    for (int y = roomBottomLeftY; y < roomTopRightY; y++)
                    {
                        if (gridMapScript.GetGridSection(x, y).coords[0] != -1)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int y = roomTopRightY; y < roomBottomLeftY; y++)
                    {
                        if (gridMapScript.GetGridSection(x, y).coords[0] != -1)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        //checks to see if the new suggested room is overlapping a current room
        foreach (BaseRoom br in roomsInPlay)
        {
            print(roomCount);
            if (roomBottomLeftX > (gridMapScript.gridSizeX / 2))
            {
                for (int x = roomBottomLeftX; x < roomTopRightX; x++)
                {
                    if (roomBottomLeftY > (gridMapScript.gridSizeY / 2))
                    {
                        for (int y = roomBottomLeftY; y < roomTopRightY; y++)
                        {
                            if ((x >= br.bottomLeft[0] && x < br.bottomLeft[0]+br.width) ||
                                (y >= br.bottomLeft[1] && y < br.bottomLeft[1]+br.length))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int y = roomTopRightY; y < roomBottomLeftY; y++)
                        {
                            if ((x >= br.bottomLeft[0] && x < br.bottomLeft[0] + br.width) ||
                                (y >= br.bottomLeft[1] && y < br.bottomLeft[1] + br.length))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = roomTopRightX; x < roomBottomLeftX; x++)
                {
                    if (roomBottomLeftY > (gridMapScript.gridSizeY / 2))
                    {
                        for (int y = roomBottomLeftY; y < roomTopRightY; y++)
                        {
                            if ((x >= br.bottomLeft[0] && x < br.bottomLeft[0] + br.width) ||
                                (y >= br.bottomLeft[1] && y < br.bottomLeft[1] + br.length))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int y = roomTopRightY; y < roomBottomLeftY; y++)
                        {
                            if ((x >= br.bottomLeft[0] && x < br.bottomLeft[0] + br.width) ||
                                (y >= br.bottomLeft[1] && y < br.bottomLeft[1] + br.length))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        return true;
    }
}
