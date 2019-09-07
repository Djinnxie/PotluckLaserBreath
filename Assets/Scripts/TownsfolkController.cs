﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownsfolkController : MonoBehaviour
{
    public GameObject bardObject;
    public GameObject chefObject;
    public GameObject farmerObject;

    public int numberOfTownsfolk;
    public GridMap gridMapScript;
    private GameObject[] ragdollObjects;
    public int maxNumberRagdolls;
    private int currentRagdoll;
    // Start is called before the first frame update
    void Start()
    {
        ragdollObjects = new GameObject[maxNumberRagdolls];
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

    public void DestroyTownsfolk(GameObject townsfolk)
    {
        // makes a newRagdoll object and places it in the world
        GameObject newRagdoll = Instantiate(townsfolk.GetComponent<TownsfolkEnemy>().ragdollObject,
                                            townsfolk.transform.position,
                                            townsfolk.transform.rotation);

        // value to loop through ragdoll array
        currentRagdoll += 1;

        // adding new ragdoll to array & and destroying last ragdoll if ragdoll number
        // exceeds max ragdolls
        if (currentRagdoll >= maxNumberRagdolls)
        {
            if (ragdollObjects[currentRagdoll] == null)
            {
                ragdollObjects[currentRagdoll] = newRagdoll;
            }
            else
            {
                Destroy(ragdollObjects[currentRagdoll]);
                ragdollObjects[currentRagdoll] = newRagdoll;
            }
        }

        // finally destroys the townsfolk that died
        Destroy(townsfolk);
    }
}