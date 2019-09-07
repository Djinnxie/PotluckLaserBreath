using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TownsfolkEnemy : MonoBehaviour
{

    private NavMeshAgent thisAgent;
    private GameObject playerObject;
    public int aggroDistance;
    private Vector3 oldPLayerLocation;
    private Vector3 newPlayerLocation;
    // Start is called before the first frame update
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");

        newPlayerLocation = playerObject.transform.position;
        oldPLayerLocation = newPlayerLocation;

    }

    // Update is called once per frame
    void Update()
    {
        newPlayerLocation = playerObject.transform.position;

        if (DistanceToPlayer() < aggroDistance)
        {
            thisAgent.SetDestination(newPlayerLocation);
        }

        oldPLayerLocation = newPlayerLocation;
    }

    public float DistanceToPlayer()
    {
        float disX = Mathf.Abs(transform.position.x - newPlayerLocation.x);
        float disY = Mathf.Abs(transform.position.y - newPlayerLocation.y);

        return disX + disY;
    }
}
