using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeactive : ActiveLookable
{
    public float spinspeed = 10.0f;
    public AnimationCurve bounce;

    private bool looking = false;
    private bool active = false;
    private Transform origtrans;
    private float origy;

    public override void LookedAt()
    {
        looking = true;
    }

    public override void LookedAway()
    {
        looking = false;
    }

    public override void Activate()
    {
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
        looking = false;
    }

    void Start()
    {
        origtrans = gameObject.transform;
        origy = origtrans.position.y;
    }

    void Update()
    {
        if (looking)
        {
            Vector3 newtrans = transform.position;
            newtrans.y = origy + bounce.Evaluate(Time.time);
            transform.position = newtrans;
        }
        else
        {
            transform.position = origtrans.position;
        }

        if (active)
        {
            transform.Rotate(0, spinspeed, 0);
        }
        else
        {
            transform.rotation = origtrans.rotation;
        }
    }
}
