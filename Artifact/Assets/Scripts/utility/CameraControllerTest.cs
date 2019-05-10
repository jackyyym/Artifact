using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script used for 2d testing and development

public class CameraControllerTest : MonoBehaviour
{
    public float turnspeed = 200f, movespeed = 100f;
    private Vector2 rotation;
    private Vector3 movement;

	private bool cursorlocked = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		else if (Input.GetKeyDown(KeyCode.C))
		{
			if (cursorlocked) Cursor.lockState = CursorLockMode.None;
			else Cursor.lockState = CursorLockMode.Locked;

			Cursor.visible = !Cursor.visible;
			cursorlocked = !cursorlocked;
		}
        rotation.y += Input.GetAxis("Mouse X") * turnspeed;
        rotation.x += Input.GetAxis("Mouse Y") * -turnspeed;
        transform.eulerAngles = rotation;

        Vector3 inputmove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * movespeed * Time.deltaTime;
        Vector3 movement = transform.TransformVector(inputmove);
        if (Input.GetKey(KeyCode.Space))
            movement.y = movespeed * 0.25f * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftShift))
            movement.y = -movespeed * 0.25f * Time.deltaTime;
        else
            movement.y = 0.0f;
        transform.position += movement;
    }

}
