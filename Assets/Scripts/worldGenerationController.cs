using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerationController : MonoBehaviour
{
    public GameObject corridor;
    public GameObject tJunction;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(wallPiece, new Vector3(0,0,0), Quaternion.identity);
        Instantiate(corridor, new Vector3(0, 0, 10), Quaternion.identity);
        Instantiate(tJunction, new Vector3(0, 0, 20), Quaternion.identity);
        Instantiate(corridor, new Vector3(16.25f, 0, 24.5f), Quaternion.Euler(0f, 90f, 0f));
        //wallPiece.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
