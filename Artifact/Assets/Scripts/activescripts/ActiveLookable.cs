using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class declares the abstract functions for all activatable objects
 * 
 */
public abstract class ActiveLookable : MonoBehaviour
{
    protected float timelooked = 0;
    protected float lookduration, activeduration;

    public void SetLookTime(float t)
    {
        timelooked = t;
    }
    public void SetLookDuration(float t)
    {
        lookduration = t;
    }
    public void SetActiveDuration(float t)
    {
        activeduration = t;
    }

    public abstract void LookedAt();
    public abstract void LookedAway();

    public abstract void Activate();
    public abstract void Deactivate();
}
