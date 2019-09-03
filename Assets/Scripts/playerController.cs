﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // define this in unity engine
    public float movementForwardSpeed = 5f;
    public float movementBackwardSpeed = 2f;
    public float strafeSpeed = 4f;
    public float rotationSpeed = 2f;

    public GameObject laserObject;
    public GameObject laserObject2;

    public Transform laserTransform;
    public Transform laserTransform2;

    private float laserCharge;
    public float laserChargeMax;
    public float laserDrain;

    public Transform cameraTransform ;
    public float cameraSpeed = 1f;

    public float lookUpMax = 90f;
    public float lookDownMax = 90f;

    public float crouchDistance = 1.2f;
    private bool crouching = false;

    public float potionGrabDistance;
    public int startingPotionCount;
    public GameObject uiObject;
    private UIController uiScript;
    private int potionCount = 0;
    private bool reloading;
    private bool laserParticleStart = true;

    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiObject.GetComponent<UIController>();
        ChangePotionCount(startingPotionCount);
        SetLaserCharge(laserChargeMax);
        if (uiScript == null)
        {
            print("failed to load script");
        }
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && RemainingLaserCharge())
        {
            if (laserParticleStart)
            {
                laserObject.GetComponent<ParticleSystem>().Play();
                laserObject2.GetComponent<ParticleSystem>().Play();
                laserParticleStart = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            laserParticleStart = true;
        }
        else if (laserCharge < 1)
        {
            laserObject.GetComponent<ParticleSystem>().Stop();
            laserObject2.GetComponent<ParticleSystem>().Stop();
        }

        if (Input.GetKeyDown("r") && !reloading)
        {
            if (potionCount > 0)
            {
                ChangePotionCount(-1);
                SetLaserCharge(laserChargeMax);
                reloading = true;
            }
        }
        else
        {
            reloading = false;
        }


        if (Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            laserObject.GetComponent<ParticleSystem>().Stop();
            laserObject2.GetComponent<ParticleSystem>().Stop();
        }

        if (Input.GetKey("w") && Input.GetKey("s"))
        {

        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementForwardSpeed * 2.5f;
        }
        else if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementForwardSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position += transform.TransformDirection(-Vector3.forward) * Time.deltaTime * movementBackwardSpeed;
        }



        if (Input.GetKey("a") && Input.GetKey("d"))
        {

        }
        else if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * strafeSpeed;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * strafeSpeed;
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {

            // get left to right mouse movement
            float yRot = Input.GetAxis("Mouse X") * rotationSpeed;

            // get up and down mouse movement
            float xRot = Input.GetAxis("Mouse Y") * cameraSpeed;

            // rotate the player model left and right
            transform.localRotation *= Quaternion.Euler(0f, yRot, 0f);

            // rotates the camera up and down
            laserTransform.localRotation *= Quaternion.Euler(-xRot, 0f, 0f);
            laserTransform2.localRotation *= Quaternion.Euler(-xRot, 0f, 0f);
            cameraTransform.localRotation *= Quaternion.Euler(-xRot, 0f, 0f);

            // clamps the max up and down view
            cameraTransform.localRotation = ClampRotationAroundXAxis(cameraTransform.localRotation);
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey("c")) && crouching == false)
        {
            cameraTransform.position += new Vector3(0, crouchDistance * -1, 0);

            crouching = true;
        }
        else if ((Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp("c")) && crouching == true)
        {
            cameraTransform.position += new Vector3(0, crouchDistance, 0);
            crouching = false;
        }

    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, lookDownMax * -1, lookUpMax);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }

    public bool GrabPotion(float x, float y, float z)
    {
        if (Input.GetKey("e"))
        {
            float distanceFromPotion;

            distanceFromPotion = Mathf.Abs(x - transform.position.x) 
                                + Mathf.Abs(y - transform.position.y) 
                                + Mathf.Abs(z - transform.position.z);

            if (distanceFromPotion < potionGrabDistance)
            {
                ChangePotionCount(1);
                return true;
            }

        }
        return false;
    }

    private void ChangePotionCount(int countModifier)
    {
        potionCount += countModifier;
        uiScript.ChangeText("PotionText", potionCount.ToString());
        //uiObject.GetComponentInChildren<Text>().text = potionCount.ToString();
    }

    private bool RemainingLaserCharge()
    {
        laserCharge -= laserDrain * Time.deltaTime;
        if (laserCharge > 1)
        {
            uiScript.ChangeText("LaserText", laserCharge.ToString());
            return true;
        }
        return false;
    }
    private void SetLaserCharge(float charge)
    {
        laserCharge = charge;
        uiScript.ChangeText("LaserText", laserCharge.ToString());
    }
}
