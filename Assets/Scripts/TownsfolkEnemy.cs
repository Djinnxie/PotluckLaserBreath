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
    // Start is called before the first frame update
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        laserObject = GameObject.Find("LaserLine");

        newPlayerLocation = playerObject.transform.position;
        oldPlayerLocation = newPlayerLocation;

    }

    // Update is called once per frame
    void Update()
    {
        newPlayerLocation = playerObject.transform.position;

        if (DistanceToPlayer() < aggroDistance)
        {
            thisAgent.SetDestination(newPlayerLocation);
        }

        oldPlayerLocation = newPlayerLocation;

    }

    private void OnTriggerEnter(Collider laserFrien)
    {
        if (laserFrien.gameObject.tag == "laser")
        {
            Destroy(gameObject);
            Debug.Log(":(");
        }
    }

    public float DistanceToPlayer()
    {
        float disX = Mathf.Abs(transform.position.x - newPlayerLocation.x);
        float disY = Mathf.Abs(transform.position.y - newPlayerLocation.y);

        return disX + disY;
    }
}
