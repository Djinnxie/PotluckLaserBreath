using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laserStart;
    public GameObject laserEnd;
    public LineRenderer laser;
    public float maxLength;
    public bool hasLaserFired;
    BoxCollider laserObject;
    // Start is called before the first frame update

    void Start()
    {
        //laser.SetPosition(1, laserStart.GetComponent<Transform>().position+new Vector3(0f,0f, maxLength));
        laser.SetPosition(1, new Vector3(0f, 0f, 0f));
        laserObject = GameObject.FindWithTag("laser").GetComponent<BoxCollider>();
        laserObject.enabled = false;
        hasLaserFired = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void FireLaser(Quaternion rotation)
    {
        if (!hasLaserFired)
        {
            laserObject.enabled = true;
            hasLaserFired = true;
        }

        laser.transform.rotation = rotation;
        RaycastHit hit;
        if (Physics.Raycast(laser.transform.position, transform.TransformDirection(new Vector3(0f, 0f, maxLength)), out hit, maxLength))
        {
            laserObject.size = new Vector3(0.5f,0.5f, hit.distance);
            laserObject.center = new Vector3(laserObject.center.x, laserObject.center.y, hit.distance / 2);
            laser.SetPosition(1, new Vector3(0f, 0f, hit.distance));
        }
    }

    public void HaltLaser()
    {
        laserObject.enabled = false;
        hasLaserFired = false;
        laser.SetPosition(1, new Vector3(0f, 0f, 0f));
    }

}
