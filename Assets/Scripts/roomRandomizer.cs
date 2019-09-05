using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Interiors
{
    public GameObject interior;
    
    public Interiors()
    {
        
    }

}
    public class roomRandomizer : MonoBehaviour
{
    //public int number;
    public List<Interiors> roomTypes = new List<Interiors>();

    
    // Start is called before the first frame update
    void Start() {
        roomTypes[Random.Range(0, roomTypes.Count)].interior.SetActive(true);
        /*
        foreach (Interiors room in roomTypes)
        {
            Debug.Log(room.interior);
        }
        */
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
