using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public GameObject roomType;
    public int width;
    public int length;
    private RoomSection[] roomParts;
    

    public RoomObject(GameObject type, int w, int l)
    {
        roomType = type;
        width = w;
        length = l;
        roomParts = new RoomSection[(width*length)];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRoomSection(RoomSection section)
    {
        //adds new roomSection to roomParts if it's not already existing in the array
        if(!CompareRoomSections(section))
        {
            roomParts[section.roomPos] = section;
        }
    }

    private bool CompareRoomSections(RoomSection sectionToCompare)
    {
        // checks if new roomSection is already in the roomParts array
        foreach (RoomSection rs in roomParts)
        {
            if (rs.coords[0] == sectionToCompare.coords[0] &&
                rs.coords[1] == sectionToCompare.coords[1] &&
                rs.coords[2] == sectionToCompare.coords[2])
            {
                return true;
            }
        }

        return false;
    }
}
