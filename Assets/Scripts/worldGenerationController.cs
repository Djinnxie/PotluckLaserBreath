using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerationController : MonoBehaviour
{
    public GameObject wallPiece;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(wallPiece);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
