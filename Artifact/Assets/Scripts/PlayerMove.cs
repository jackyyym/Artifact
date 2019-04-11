using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMove : MonoBehaviour
{
    public SteamVR_Action_Boolean turnLeft, turnRight;

    public SteamVR_Action_Vector2 move;

    public GameObject CameraRig;

    public float movespeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (turnLeft.stateDown)
        {
            CameraRig.transform.Rotate(0, -15f, 0);
        }
        if (turnRight.stateDown)
        {
            CameraRig.transform.Rotate(0, 15f, 0);
        }
        Vector2 m = move.axis;
        CameraRig.transform.position += new Vector3(m.x, 0, m.y) * movespeed;
    }
}
