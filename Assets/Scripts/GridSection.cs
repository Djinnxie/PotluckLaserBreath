using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSection : MonoBehaviour
{

    public GameObject gridSectionType;
    public  Vector3 position;
    public Quaternion rotation;
    public int[] coords { get; set; }
    public int rot;
    public int roomSize = 7 * 2;
    public GridSection()
    {
        coords = new int[3] { -1, -1, -1 };
    }
    public GridSection(int x, int y, int z, int rot)
    {
        position = new Vector3(x * roomSize, z * roomSize, y * roomSize);
        coords = new int[3] { x, y, z };

        switch (rot)
        {
            //forward
            default:
            case 0:
                rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            //right
            case 1:
                rotation = Quaternion.Euler(-90f, 90f, 0f);
                break;
            //backwards
            case 2:
                rotation = Quaternion.Euler(-90f, 180f, 0f);
                break;
            //right
            case 3:
                rotation = Quaternion.Euler(-90f, -90f, 0f);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class CorridorSection : GridSection
{
    public CorridorSection(GameObject type, int x, int y, int z, int rot) 
        : base(x, y, z, rot)
    {
        gridSectionType = type;
    }
}

public class RoomSection : GridSection
{

    public RoomObject[] parentRoom;

    public int roomPos { get; set; }
    public RoomSection() : base()
    {

    }
    public RoomSection(ref RoomObject type, int x, int y, int z, int rot, int pos)
        : base(x, y, z, rot)
    {
        roomPos = pos;
        parentRoom = new RoomObject[1] { type };
        parentRoom[0].AddRoomSection(this);
    }

}

