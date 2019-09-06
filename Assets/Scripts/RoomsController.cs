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
    public RoomSection[,] roomPieces;

    public int[] bottomLeft = new int[2];
    public int[] topRight = new int[2];
    

    public BaseRoom(int x, int y, int z, int rot, int w, int l)
    {
        width = w;
        length = l;
        coords = new int[3] { x, y, z };
        roomPieces = new RoomSection[w, l];
        // move the rooms to sit on the grid
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
        roomPieces = new RoomSection[width, length];
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

        // move the rooms to sit on the grid

        //if width is even
        if (width % 2 == 0)
        {
            bottomLeft[0] = x - (width / 2);
            newWidth = (width/2)*(roomSize/ 2);

        }
        //if width is odd and greater than 1
        else if (width % 2 == 1 && width > 2)
        {
            bottomLeft[0] = x - ((width - 1) / 2);
            newWidth = ((width - 1) / 2) * (roomSize / 2);
            if(width == 3)
            {
                newWidth += roomSize/2;
            }
        }
        //if width is 1
        else
        {
            bottomLeft[0] = x - ((width - 1) / 2);
        }
        //if length is even
        if (length % 2 == 0)
        {
            bottomLeft[1] = y - (length / 2);
            newLength = (length/2) * (roomSize / 2);
        }

        //if length is odd and greater than 1
        else if (length % 2 == 1 && length > 2)
        {
            bottomLeft[1] = y - ((length - 1) / 2);
            newLength = ((length - 1) / 2) * (roomSize / 2);
            if (length == 3)
            {
                newLength += roomSize / 2;
            }
        }
        //if length is 1
        else
        {
            bottomLeft[1] = y - ((length - 1) / 2);
        }
        position = new Vector3(x * roomSize +newWidth, z * roomSize, y * roomSize +newLength);// +length/2);

    }
}
public class RoomsController : MonoBehaviour
{
    public GridMap gridMapScript;
    public int maxRooms;
    public int roomCount = 0;
    public List<BaseRoom> roomsTypes = new List<BaseRoom>();
    public List<BaseRoom> roomsInPlay = new List<BaseRoom>();
    
    // Start is called before the first frame update
    void Start()
    {
        BaseRoom newRoom;
        //looping through bellow grid
        //Debug.Log(gridMapScript.outerGridSizeX / 2);
/*        for (int x = gridMapScript.outerGridSizeX/2; x > gridMapScript.outerGridSizeX; x++)
        {

            for (int y = gridMapScript.outerGridSizeY/2; y > 0; y--)
            {
                if(gridMapScript.GetGridSection(x,y).coords[0] == -1)
                {
                    Debug.Log("found an edge");
                    newRoom =  new BaseRoom( roomsTypes[0]);
                    newRoom.SetPosition(x, y, 0);
                    newRoom.Rotate(0);
                   
                    if(CanRoomFit(x,y, roomsTypes[0]))
                    {
                        AddRoomToList(newRoom,x,y,0);
                        roomCount++;
                        break;
                    }
                }
            }
        }
*/
/*        for (int y = gridMapScript.innerGridDistance; y < gridMapScript.outerGridSizeY; y++)
        {
            for (int x = gridMapScript.innerGridDistance; x < gridMapScript.outerGridSizeX; x++)
            {
                if (gridMapScript.GetGridSection(x, y).coords[0] != -1)
                {
                    newRoom = new BaseRoom(roomsTypes[0]);
                    newRoom.SetPosition(x-1, y, 0);
                    newRoom.Rotate(0);

                    if (CanRoomFit(x-1, y, roomsTypes[0]))
                    {
                        AddRoomToList(newRoom, x-1, y, 0);
                        roomCount++;
                        break;
                    }
                }
            }
        }*/

        /*x = -1;
        //looping through left of grid
        for (y = 0; y < gridMapScript.innerGridSizeY; y++)
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
        y = gridMapScript.innerGridSizeY + 1;
        for (x = 0; x < gridMapScript.innerGridSizeX; x++)
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
        x = gridMapScript.innerGridSizeX + 1;
        for (y = 0; y < gridMapScript.innerGridSizeY; y++)
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
        }*/

        /*        for(int x = 0; x < gridMapScript.outerGridSizeX; x++)
                {
                    string lineToPrint = "";
                    for (int y = 0; y < gridMapScript.outerGridSizeY; y++)
                    {
                        if (gridMapScript.GetGridSection(x, y).coords[0] == -1)
                        {
                            lineToPrint += "-";
                        }
                        else
                        {
                            lineToPrint += "X";
                        }
                    }
                    Debug.Log(x+"   "+lineToPrint);
                }
                //  =(
                 */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanRoomFit(int targetX, int targetY, BaseRoom targetRoom)
    {

        return true;
        /*int roomBottomLeftX, roomBottomLeftY, roomTopRightX, roomTopRightY;

        //changing where the bounds of bottom left and top right are

        // if X is to the right of the grid middle
        if (targetX > (gridMapScript.innerGridSizeX/2))
        {
            roomBottomLeftX = targetX;
            roomTopRightX = targetX + targetRoom.width;
        }

        // if X is to the left of the grid middle
        else if (targetX < (gridMapScript.innerGridSizeX / 2))
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
        if (targetY > (gridMapScript.innerGridSizeY/2))
        {
            roomBottomLeftY = targetY;
            roomTopRightY = targetY + targetRoom.length;
        }

        // if Y is bellow the grid middle
        else if (targetY < (gridMapScript.innerGridSizeY / 2))
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
        if (roomBottomLeftX > (gridMapScript.innerGridSizeX / 2))
        {
            for (int x = roomBottomLeftX; x < roomTopRightX; x++)
            {
                //checks which side above or bellow the grid middle
                if (roomBottomLeftY > (gridMapScript.innerGridSizeY / 2))
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
                if (roomBottomLeftY > (gridMapScript.innerGridSizeY / 2))
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
            if (roomBottomLeftX > (gridMapScript.innerGridSizeX / 2))
            {
                for (int x = roomBottomLeftX; x < roomTopRightX; x++)
                {
                    if (roomBottomLeftY > (gridMapScript.innerGridSizeY / 2))
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
                    if (roomBottomLeftY > (gridMapScript.innerGridSizeY / 2))
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

        return true;*/
    }

    public int CanRoomFit(int targetX, int targetY, BaseRoom targetRoom, int count)
    {
        //Debug.Log("LEN "+targetRoom.length);
        bool roomWorks = true;
        //int[] roomGridLocationX = new int[(targetRoom.width*targetRoom.length)];
        //int[] roomGridLocationY = new int[(targetRoom.width * targetRoom.length)];
        for (int x = 0; x < targetRoom.width; x++)
        {
            for (int y = 0; y < targetRoom.length; y++)
            {
                if (gridMapScript.GetGridSection(targetX+x, targetY+y).coords[0]!=-1)
                {
                    if (roomWorks)
                    {
                        //Debug.Log("obstacle at position " + (targetX + x) + ", "+ (targetY + y));
                    }
                    //roomGridLocationX[x] = targetX + x;
                    //roomGridLocationY[y] = targetX + x;
                    roomWorks = false;
                }
            }
        }
        //if it fits
        if (roomWorks)
        {
            Debug.Log("creating room at" + (targetX) + ", " + (targetY));
            return count;
        }
        //if its a square and doesnt fit or if its been rotated all the way already, dont bother rotating it.
        if (targetRoom.width == targetRoom.length||count==3)
        {
            return -1;
        }
        else
        {
            // write the flip code before enabling this
            //return CanRoomFit(targetX, targetY, targetRoom, count += 1);
            return -1;
        }
    }

    public GameObject AddRoomToList(BaseRoom newRoom, int gridPositionX, int gridPositionY, int rotation)
    {

        newRoom.SetPosition(gridPositionX, gridPositionY, 0);
        Debug.Log("Room Number: " + roomCount);

        int newX;
        int newY;

        Debug.Log("room Width: " + newRoom.width + "room Length: " + newRoom.length);
        for (int x = 0; x < newRoom.width; x++)
        {
            for (int y = 0; y < newRoom.length; y++)
            {
                newX = newRoom.bottomLeft[0] + x;
                newY = newRoom.bottomLeft[1] + y;
                //Debug.Log("Room X: " + newX + " Room Y: " + newY);
                newRoom.roomPieces[x, y] = new RoomSection(newRoom, x + gridPositionX, y + gridPositionY, 0, newRoom.rot);
                gridMapScript.GetGridMap()[x+gridPositionX,y+gridPositionY] = newRoom.roomPieces[x, y];
        //Debug.Log("Room Piece X " + gridMapScript.GetGridMap()[x+gridPositionX, y + gridPositionY].coords[0]
         //   + " Room Piece Y " + gridMapScript.GetGridMap()[x+gridPositionX, y + gridPositionY].coords[1]);
            }
        }

        newRoom.Rotate(rotation);

        GameObject newRoomObject = Instantiate(newRoom.roomObject, newRoom.position,
                            newRoom.rotation);
        roomCount += 1;

        return newRoomObject;
    }

}
