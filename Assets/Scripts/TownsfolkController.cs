using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownsfolkController : MonoBehaviour
{
    public GameObject bardObject;
    public int bardWeight;
    public GameObject chefObject;
    public int chefWeight;
    public GameObject farmerObject;
    public int farmerWeight;
    public GameObject farmer2Object;
    public int farmer2Weight;
    public GameObject soldierObject;
    public int soldierWeight;



    public int numberOfTownsfolk;
    public GridMap gridMapScript;
    private GameObject[] ragdollObjects;
    public int maxNumberRagdolls;
    private int currentRagdoll;
    public int ragdollDeathFling;
    public GameObject playerObject;
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

        GameObject newTownsfolk = bardObject;
        GridSection currentGridSection;
        int weightTotal = bardWeight + chefWeight + farmerWeight + farmer2Weight + soldierWeight;
        int weight;
        for (int i = 0; i < numberOfTownsfolk; i++)
        {
            //replace with randomisation
            weight = Random.Range(0, weightTotal);

            if (weight >= 0 && weight < bardWeight)
            {
                newTownsfolk = bardObject;
            }
            else if (weight >= bardWeight && weight < bardWeight + chefWeight)
            {
                newTownsfolk = chefObject;
            }
            else if (weight >= bardWeight+chefWeight && weight < bardWeight+chefWeight+farmerWeight)
            {
                newTownsfolk = farmerObject;
            }
            else if(weight >= bardWeight + chefWeight + farmerWeight && weight < bardWeight + chefWeight + farmerWeight + farmer2Weight)
            {
                newTownsfolk = farmer2Object;
            }
            else if (weight >= bardWeight + chefWeight + farmerWeight + farmer2Weight && weight < weightTotal)
            {
                newTownsfolk = soldierObject;
            }
            else
            {
                Debug.Log("weighting broke with value: " + weight);
            }
            
            
            //newTownsfolk = chefObject;

            do
            {
                randX = Random.Range(0, gridMapScript.outerGridSizeX);
                randY = Random.Range(0, gridMapScript.outerGridSizeY);

                currentGridSection = gridMapScript.GetGridSection(randX, randY);

            } while (currentGridSection.coords[0] == -1);

            Instantiate(newTownsfolk, currentGridSection.position, currentGridSection.rotation);
            currentGridSection.hasTownsfolk = true;
        }
    }

    public void DestroyTownsfolk(ref GameObject townsfolk)
    {
        // makes a newRagdoll object and places it in the world
        GameObject newRagdoll = Instantiate(townsfolk.GetComponent<TownsfolkEnemy>().ragdollObject,
                                            townsfolk.transform.position,
                                            townsfolk.transform.rotation);

        // value to loop through ragdoll array
        if (currentRagdoll >= maxNumberRagdolls)
        {
            currentRagdoll = 0;
        }

        // adding new ragdoll to array & and destroying last ragdoll if ragdoll number
        // exceeds max ragdolls
        if (ragdollObjects[currentRagdoll] == null)
        {
            ragdollObjects[currentRagdoll] = newRagdoll;
        }
        else
        {
            Destroy(ragdollObjects[currentRagdoll]);
            ragdollObjects[currentRagdoll] = newRagdoll;

        }

        Destroy(townsfolk);

        Transform rt = ragdollObjects[currentRagdoll].transform;

        Transform bones = rt.GetChild(0);

        Transform hips = bones.GetChild(0);

        hips.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.forward) * ragdollDeathFling;

        currentRagdoll += 1;

        // finally destroys the townsfolk that died

    }
}
