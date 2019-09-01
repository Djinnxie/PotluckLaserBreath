using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerationController : MonoBehaviour
{
    public GameObject wallPiece;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(wallPiece, new Vector3(0,0,0), Quaternion.identity);
        Instantiate(wallPiece, new Vector3(0, 0, 8), Quaternion.identity);
        //wallPiece.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
