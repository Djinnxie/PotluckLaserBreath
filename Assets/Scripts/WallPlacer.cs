using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{ 
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomLeft;
    public GameObject bottomRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveWall(int direction)
    {
        switch (direction)
        {
            case 0:
                topLeft.SetActive(false);
                break;
            case 1:
                topRight.SetActive(false);
                break;
            case 2:
                bottomLeft.SetActive(false);
                break;
            case 3:
                bottomRight.SetActive(false);
                break;
        }
    }
}
