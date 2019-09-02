using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPiece : MonoBehaviour
{
    public GameObject roomType;
    public Vector3 position;
    public Quaternion rotation;
    public int[] coords;
    public int rot;
    public int roomSize = 7 * 2;
    public RoomPiece(GameObject type, int x, int y, int z, int rot)
    {
        // figure out size dynamically?
        roomType = type;
        position = new Vector3(x*roomSize, z*roomSize, y*roomSize);
        coords = new int[3]{x, y, z};
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
}

public class worldGenerationController : MonoBehaviour
{
    public GameObject corridor;
    public GameObject tJunction;
    private int roomSize = 14;

    // Start is called before the first frame update
    void Start()
    {
        RoomPiece[,] grid = new RoomPiece[5,5];
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                grid[x,y] = new RoomPiece(corridor, x, y, 0, Random.Range(0,5));
                Instantiate(grid[x, y].roomType, grid[x, y].position, grid[x, y].rotation);
            }
        }

        /*
        //Instantiate(wallPiece, new Vector3(0,0,0), Quaternion.identity);
        Instantiate(corridor, new Vector3(0, 0, 10), Quaternion.Euler(-89.98f, 0f, 0f));
        Instantiate(corridor, new Vector3(0, 0, 10+roomSize), Quaternion.Euler(-89.98f, 0f, 0f));
        Instantiate(corridor, new Vector3(0, 0, 10+roomSize*2), Quaternion.Euler(-89.98f, 0f, 0f));
        Instantiate(tJunction, new Vector3(0, 0, 10 + roomSize * 3), Quaternion.Euler(-89.98f, 90f, 0f));
        Instantiate(corridor, new Vector3(roomSize, 0, 10 + roomSize * 3), Quaternion.Euler(-89.98f, 90f, 0f));
        Instantiate(corridor, new Vector3(-roomSize, 0, 10 + roomSize * 3), Quaternion.Euler(-89.98f, -90f, 0f));
        //Instantiate(tJunction, new Vector3(0, 0, 20), Quaternion.identity);
        //Instantiate(corridor, new Vector3(16.25f, 0, 24.5f), Quaternion.Euler(0f, 90f, 0f));
        //wallPiece.transform.position = new Vector3(0, 0, 0);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
