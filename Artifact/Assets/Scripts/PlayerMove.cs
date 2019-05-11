using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Jacky McGrath May 2019
//
// manager script for steamvr actions and player movement

public class PlayerMove : MonoBehaviour
{
    public SteamVR_Action_Boolean turnLeft, turnRight;

    public SteamVR_Action_Vector2 move;

    public SteamVR_Action_Boolean triggerAction;

    public SteamVR_Action_Vibration haptic;

    public SteamVR_Behaviour_Pose controller_left, controller_right;

    public GameObject CameraRig;

    public float movespeed = 0.5f;

    private Rigidbody r;

    private Vector3 v_move;

    void Awake()
    {
        r = CameraRig.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (turnLeft.stateDown)
            CameraRig.transform.Rotate(0, -15f, 0);
        if (turnRight.stateDown)
            CameraRig.transform.Rotate(0, 15f, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        Vector2 m = move.axis;

        v_move = m.y * Camera.main.transform.forward + m.x * Camera.main.transform.right;
        v_move.y = 0.0f;
        r.position += v_move * movespeed * Time.deltaTime;
    }
}
