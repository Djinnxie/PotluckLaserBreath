using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public GameObject playerObject;

    private playerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerObject.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.GrabPotion(transform.position.x,transform.position.y,transform.position.z))
        {
            Destroy(this.gameObject);
        }
    }
}
