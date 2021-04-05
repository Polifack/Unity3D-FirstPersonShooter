using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float mouseSensitivity; // This could be in a "settings" file

    Transform t;
    Camera c;

    void Start()
    {
        t = transform;
        c = GetComponentInChildren<Camera>();
    }
       
    Vector3 getMovementInput()
    {
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");

        Vector3 xMove = xVal * -transform.up;
        Vector3 zMove = zVal * transform.right;

        // Create a input vector based on movement axis
        Vector3 input = (xMove + zMove);
        return input;
    }

    Vector3 getCameraControl()
    {
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");

        Vector3 mouseInput = new Vector2(xMouse, yMouse) * mouseSensitivity;

        return mouseInput;
    }

    void doMove(Vector3 movementInput)
    {
        t.position += movementInput * Time.deltaTime * movementSpeed;
    }

    void doRotate(float mouseX)
    {
        // Create a quaternion according to our rotation needs
        // we create it from the euler angles of the player
        // we modify the rotation in z axis
        float xDeltaRot = t.rotation.eulerAngles.z - mouseX;
        Quaternion playerRotation = Quaternion.Euler(t.rotation.eulerAngles.x, t.rotation.eulerAngles.y, xDeltaRot);

        // Rotate phisically the player
        t.rotation = playerRotation;
    }

    void doRotateCamera(float mouseY)
    {
        // Create a quaternion according to our needs
        // we create it from the local euler angles of the camera
        // we modify the Y axis
        Quaternion camRotation = Quaternion.Euler(c.transform.localRotation.eulerAngles + new Vector3(0, mouseY, 0));

        // Avoid rotating 360 degrees
        if ((camRotation.y > -0.25f) || (camRotation.y < -0.75f))
        {
            return;
        }
        // Change the local rotation of the camera
        c.transform.localRotation = camRotation;
    }

    void Update()
    {
        Vector3 velocity = getMovementInput();
        doMove(velocity);

        Vector3 cameraMovement = getCameraControl();
        doRotate(cameraMovement.x);
        doRotateCamera(cameraMovement.y);
    }
}
