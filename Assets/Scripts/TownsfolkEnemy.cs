using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TownsfolkEnemy : MonoBehaviour
{

    private NavMeshAgent thisAgent;
    private GameObject playerObject;
    private GameObject laserObject;
    public int aggroDistance;
    private Vector3 oldPlayerLocation;
    private Vector3 newPlayerLocation;
    public GameObject ragdollObject;
    private TownsfolkController townsfolkControllerScript;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        laserObject = GameObject.Find("LaserLine");
        townsfolkControllerScript = GameObject.Find("codeHandler").GetComponent<TownsfolkController>();

        newPlayerLocation = playerObject.transform.position;
        oldPlayerLocation = newPlayerLocation;

    }

    // Update is called once per frame
    void Update()
    {
        newPlayerLocation = playerObject.transform.position;

        if(oldPlayerLocation.x == newPlayerLocation.x && oldPlayerLocation.z == newPlayerLocation.z)
        {

        }
        else if (DistanceToPlayer() < aggroDistance)
        {
            thisAgent.SetDestination(newPlayerLocation);
        }

        oldPlayerLocation = newPlayerLocation;

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "laser")
        {
            GameObject thisObject = this.gameObject;
            townsfolkControllerScript.DestroyTownsfolk(ref thisObject);
            Debug.Log(":(");
        }
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("gets to triggerStay");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("collided with Player");
            Debug.Log("damage: " + damage * Time.deltaTime);
            playerObject.GetComponent<playerController>().TakeDamage(damage * Time.deltaTime);
        }
    }

    public float DistanceToPlayer()
    {
        float disX = Mathf.Abs(transform.position.x - newPlayerLocation.x);
        float disY = Mathf.Abs(transform.position.y - newPlayerLocation.y);

        return disX + disY;
    }
}
