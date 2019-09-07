using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownsfolkController : MonoBehaviour
{
    public GameObject bardObject;
    public GameObject chefObject;
    public GameObject farmerObject;

    public int numberOfTownsfolk;
    public GridMap gridMapScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonTownsfolk()
    {
        int randX, randY;

        GameObject newTownsfolk;
        GridSection currentGridSection;
        for (int i = 0; i < numberOfTownsfolk; i++)
        {
            //replace with randomisation
            newTownsfolk = chefObject;

            do
            {
                randX = Random.Range(0, gridMapScript.outerGridSizeX);
                randY = Random.Range(0, gridMapScript.outerGridSizeY);

                currentGridSection = gridMapScript.GetGridSection(randX, randY);

            } while (currentGridSection.coords[0] == -1);

            Instantiate(newTownsfolk, currentGridSection.position, currentGridSection.rotation);
        }
    }
}
