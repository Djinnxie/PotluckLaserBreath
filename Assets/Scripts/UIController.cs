using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject playerObject;

    public GameObject laserChargeBarObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLaserCharge(float laserCharge, float chargeMax)
    {
        laserCharge = (laserCharge / chargeMax) * 2;

        laserChargeBarObject.transform.localScale = new Vector3(laserCharge, 1, 1);
    }

    public void ChangePotionValue (int potionValue)
    {
        foreach (Text tx in this.GetComponentsInChildren<Text>())
        {
            if (tx.name == "PotionText")
            {
                tx.text = potionValue.ToString();
            }
        }
    }

    public void ResetUI()
    {

    }
}
