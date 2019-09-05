using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSection
{
    public GameObject gridSectionType;
    public  Vector3 position;
    public Quaternion rotation;
    public int[] coords { get; set; }
    public int rot;
    public int roomSize = 7 * 2;

    //public bool willDraw { get; set; }
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
    

    public CorridorSection(GameObject type, int x, int y, int z, int rot)//, bool draw) 
        : base(x, y, z, rot)
    {
        gridSectionType = type;
        //willDraw = draw;
    }
   
}

public class RoomSection : GridSection
{
    public BaseRoom parentRoom;

    public RoomSection(BaseRoom room, int x, int y, int z, int rot)
        : base(x, y, z, rot)
    {
        parentRoom = room;
    }
}

