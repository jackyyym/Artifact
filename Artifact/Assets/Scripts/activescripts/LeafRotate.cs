using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafRotate : ActiveLookable
{
    public float spinspeed = 10.0f;

    private bool looking = false;
    private bool active = false;

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
    }

    void Update()
    {
        if (looking)
        {
            foreach (Transform x in transform)
                foreach(Transform y in transform)
                    y.transform.Rotate(0, spinspeed, 0);
        }
        else if (active)
            foreach (Transform x in transform)
                foreach (Transform y in transform)
                    y.transform.Rotate(0, spinspeed * 2, 0);
    }

}
