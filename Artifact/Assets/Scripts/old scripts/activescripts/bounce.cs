using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : ActiveLookable
{
    public float spinspeed = 10.0f;
    public AnimationCurve b;

    private bool looking = false;
    private bool active = false;
    private Transform origtrans;
    private float origy;

    public override void LookedAt()
    {
        if (!active)
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
            transform.Rotate(0, spinspeed * (timelooked / lookduration), 0);

            Vector3 newtrans = transform.position;
            newtrans.y = origy + (b.Evaluate(Time.time)) * (timelooked / lookduration);
            transform.position = newtrans;
        }
        else
        {
            transform.rotation = origtrans.rotation;
            transform.position = origtrans.position;

        }
    }

}
