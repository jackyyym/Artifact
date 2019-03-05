using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    public float turnspeed = 2.0f, movespeed = 1.0f;
    private Vector2 rotation;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X") * turnspeed;
        rotation.x += Input.GetAxis("Mouse Y") * -turnspeed;
        transform.eulerAngles = rotation;

        Vector3 inputmove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * movespeed;
        Vector3 movement = transform.TransformVector(inputmove);
        movement.y = 0.0f;
        transform.position += movement;
    }
}
