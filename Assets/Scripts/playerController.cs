using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // define this in unity engine
    public float movementForwardSpeed;
    public float movementBackwardSpeed;
    public float strafeSpeed;
    public float rotationSpeed;

    public Transform cameraTransform;
    public float cameraSpeed;

    public float lookUpMax;
    public float lookDownMax;

    public float crouchDistance;
    private bool crouching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {

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
}
