using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : ActiveLookable
{
    public float spinspeed = 10.0f;
    public AnimationCurve b;
    public AnimationCurve easein;

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
        StartCoroutine(Reset());
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
            easein = AnimationCurve.EaseInOut(0, 0, lookduration, 1);
            Vector3 newtrans = transform.position;
            newtrans.y = origy + (b.Evaluate(Time.time) * easein.Evaluate(timelooked));
            Debug.Log("b curve: " + b.Evaluate(Time.time));
            Debug.Log(easein.Evaluate(timelooked));
            Debug.Log("newtrans.y :" + newtrans.y);
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

    IEnumerator Reset()
    {
        Vector3 currentpos = transform.position;
        for (float i = 0; i < 1; i += 0.1f)
        {
            Vector3.Lerp(currentpos, origtrans.position, i);
            yield return new WaitForSeconds(0.1f);
        }
        active = false;
        yield break;
    }
}
