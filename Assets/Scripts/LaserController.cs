using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laserStart;
    public GameObject laserEnd;
    public LineRenderer laser;
    public float maxLength;
    // Start is called before the first frame update
    void Start()
    {
        laser.SetPosition(1, laserStart.GetComponent<Transform>().position+new Vector3(0f,0f, maxLength));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(laserStart.GetComponent<Transform>().position, transform.TransformDirection(new Vector3(0f, 0f, maxLength)), out hit, maxLength))
        {
            //Debug.Log(hit.distance);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            laser.SetPosition(1, new Vector3(0f, 0f, hit.distance));
        }

    }
}
