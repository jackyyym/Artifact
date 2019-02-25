using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    public float turnspeed = 2.0f;
    private Vector2 rotation;

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
    }
}
