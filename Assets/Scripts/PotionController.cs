using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion
{
    private GameObject potionObject;

    public Potion()
    {

    }
    public Potion (GameObject p)
    {
        potionObject = p;
    }

    public void SetPotionObject(GameObject po)
    {
        potionObject = po;
    }

    public GameObject GetPotionObject()
    {
        return potionObject;
    }

}


public class PotionController : MonoBehaviour
{
    public GameObject playerObject;

    private playerController playerScript;
    public RoomsController roomsScript;

    private List<Potion> potionList;
    public GameObject potionObject;

    public int potionRarity;
    // Start is called before the first frame update
    public void Start()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<playerController>();
        roomsScript = GameObject.Find("codeHandler").GetComponent<RoomsController>();

        potionList = new List<Potion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPotions()
    {

        int mightSpawnPotion;

        int randX, randY;

        int whileCount = 0;
        for (int i = 0; i <roomsScript.roomsInPlay.Count; i++ )
        {
            BaseRoom br = roomsScript.roomsInPlay[i];
            whileCount = 0;
            mightSpawnPotion = Random.Range(0, potionRarity);
            if (mightSpawnPotion == 0)
            {
                do
                {
                    whileCount += 1;
                    randX = Random.Range(0, br.width);
                    randY = Random.Range(0, br.length);
                } while (br.GetRoomSectionArray()[randX, randY].hasTownsfolk == true && whileCount < 10);

                if (whileCount < 10)
                {
                br.GetRoomSectionArray()[randX, randY].hasPotion = true;
                Potion p = new Potion();
                GameObject go =  Instantiate(potionObject, br.GetRoomSectionArray()[randX, randY].position,
                                br.GetRoomSectionArray()[randX, randY].rotation);

                p.SetPotionObject(go);
                potionList.Add(p);
                }
            }
        }
    }

    public void GrabPotion()
    {
        for (int i = 0; i < potionList.Count; i++)
        {
            Potion p = potionList[i];
            Transform pt = p.GetPotionObject().transform;

            if (playerScript.GrabPotion(pt.position.x, pt.position.z, pt.position.y))
            {
                Destroy(p.GetPotionObject());
                potionList.RemoveAt(i);
            }
        }
    }
    
}
