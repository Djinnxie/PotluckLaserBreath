using System.Collections;
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

    public GameObject character;
    public Animator charController;

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
    public GridMap gridMapScript;
    private int potionCount = 0;
    private bool reloading;
    private bool laserParticleStart = true;

    private float playerHealth;

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
        bool isMovingx = false;
        bool isMovingy = false;

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
                charController.SetBool("drink", true);
                ChangePotionCount(-1);
                SetLaserCharge(laserChargeMax);
                reloading = true;
            }
        }
        else
        {
            reloading = false;
            //charController.SetBool("drink", false);
        }


        if (Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            laserObject.GetComponent<ParticleSystem>().Stop();
            laserObject2.GetComponent<ParticleSystem>().Stop();
        }

        if (Input.GetKey("w") && Input.GetKey("s"))
        {
            isMovingx = false;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementForwardSpeed * 2.5f;
            isMovingx = true;
        }
        else if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementForwardSpeed;
            isMovingx = true;
        }
        else if (Input.GetKey("s"))
        {
            transform.position += transform.TransformDirection(-Vector3.forward) * Time.deltaTime * movementBackwardSpeed;
            isMovingx = true;
        }
        else
        {
            
            isMovingx = false;
        }



        if (Input.GetKey("a") && Input.GetKey("d"))
        {
            isMovingy = false;
        }
        else if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * strafeSpeed;
            isMovingy = true;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * strafeSpeed;
            isMovingy = true;
        }

        if (isMovingx || isMovingy)
        {
            charController.SetInteger("walk", 1);
        }
        else
        {
            charController.SetInteger("walk", 0);
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
        uiScript.ChangePotionValue(potionCount);
        //uiObject.GetComponentInChildren<Text>().text = potionCount.ToString();
    }

    private bool RemainingLaserCharge()
    {
        laserCharge -= laserDrain * Time.deltaTime;
        if (laserCharge > 1)
        {
            SetLaserChargeText();
            return true;
        }
        return false;
    }
    private void SetLaserCharge(float charge)
    {
        laserCharge = charge;
        SetLaserChargeText();
    }
    private void SetLaserChargeText()
    {
        uiScript.ChangeLaserCharge(laserCharge, laserChargeMax);
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public void SetHealth(float newHealth)
    {
        playerHealth = newHealth;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            PlayerIsDead();
        }
    }

    private void PlayerIsDead()
    {
        uiScript.ResetUI();
        gridMapScript.ResetGridMap();
        ResetPlayer();
    }

    public void ResetPlayer()
    {

    }
}
