using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;

// Jacky McGrath May 2019

// This script causes objects to deform, spin, and play their assigned audio track when the player looks at them.

public class ProximityLook : MonoBehaviour
{
    public float maxdis = 15, mindis = 5;
    public float deformmax = 180f;
    public float turnspeed = 5;
    private Vector3 ymask = new Vector3(1, 0, 1);

    private Renderer r;
    private AudioSource clip;
    public TwistDeformer d;

    private float radiusmax = 50f;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform camtrans = Camera.main.transform;

        Vector3 dir = transform.position - camtrans.position; // target direction (from camera to object)
        float angle = Vector3.Angle(Vector3.Scale(dir, ymask), camtrans.forward); // angle between camera and object
        float distance = Vector3.Distance(camtrans.position, transform.position); // distance between camera and object
    
        // use law of cosine to solve third side of isosceles  triangle, where one side is from the player camera to the object, and
        // the other side is of the same length but angled the same way as the player is looking.
        float angdis = (Mathf.Pow(distance, 2) + Mathf.Pow(distance, 2)) - (2 * distance * distance * Mathf.Cos(angle * Mathf.Deg2Rad));
        angdis = Mathf.Sqrt(angdis);

        // normalize angular distance between 0 and 1
        float normalzed_angdis = (angdis - maxdis) / (mindis - maxdis);

        // main chunk of setting spin speed and clip volume
        float dis = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
        if (dis < radiusmax)
        if (r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            // between min and max bounds, volume and turning speed scale between 0 and 1
            if (angdis <= maxdis && angdis > mindis)
            {
                clip.volume = normalzed_angdis;
                transform.Rotate(0, (turnspeed * normalzed_angdis) / 2f, turnspeed * normalzed_angdis, Space.World);
                d.EndAngle = deformmax * normalzed_angdis;
            }
            // within minimum boundary, spin speed and volume at maximum
            else if (angdis <= mindis)
            {
                clip.volume = 1;
                transform.Rotate(0, turnspeed, turnspeed / 2);
                d.EndAngle = deformmax;
            }
            // outside of maximum range or out of sight
            else
            {
                clip.volume = 0;
                d.EndAngle = 0;
            }
        }
        else
        {
            clip.volume = 0;
            d.EndAngle = 0;
        }

    }
}