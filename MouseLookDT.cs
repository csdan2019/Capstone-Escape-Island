using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookDT : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    //xRotation allows mouse to look up and down by rotate around x-axis
    float xRotation = 0f;
    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        //cursor now is locked at center of screen so it cannot go off screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime allows for the movement to be even and smooth movement over time
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
       
        xRotation -= mouseY;
        //allows player to only rotate 90degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Quaternion is rotation in Unity

        transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}
