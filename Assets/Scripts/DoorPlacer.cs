using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlacer : MonoBehaviour
{
    public GameObject firstDoor;
    public GameObject secondDoor;
    public GameObject bothDoor;
    public GameObject noDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDoor(bool firstDoorBool, bool secondDoorBool)
    {
        //if doors on both sides of the wall
        if (firstDoorBool == true && secondDoorBool == true)
        {
            bothDoor.SetActive(true);
        }

        //if door on the true left side for corridor or true left side of corner 
        else if (firstDoorBool == true)
        {
            firstDoor.SetActive(true);
        }
        //if the door is on the true right side for corridor or true forward side of corner
        else if (secondDoorBool == true)
        {
            secondDoor.SetActive(true);
        }

        // if there are no rooms attached to this hall section
        else
        {
            noDoor.SetActive(true);
        }
    }
}
