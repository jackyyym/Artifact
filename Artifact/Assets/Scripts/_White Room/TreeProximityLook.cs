using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jacky McGrath May 2019

// spins leaves based on looking at tree
public class TreeProximityLook : MonoBehaviour
{
    public float maxdis = 8, mindis = 1;
    public float turnspeed = 50f;
    private Vector3 ymask = new Vector3(1, 0, 1);

   private Renderer r;
   // private AudioSource clip;

    private Vector3 v3_deltatime;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
       // clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform camtrans = Camera.main.transform;
        v3_deltatime = new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

        Vector3 dir = transform.position - camtrans.position; // target direction (from camera to tree)
        float angle = Vector3.Angle(Vector3.Scale(dir, ymask), camtrans.forward); // angle between camera and tree
        float distance = Vector3.Distance(camtrans.position, transform.position); // distance between camera and tree

        // use law of cosine to solve third side of isosceles  triangle, where one side is from the player camera to the tree, and
        // the other side is of the same length but angled the same way as the player is looking.
        float angdis = (Mathf.Pow(distance, 2) + Mathf.Pow(distance, 2)) - (2 * distance * distance * Mathf.Cos(angle * Mathf.Deg2Rad));
        angdis = Mathf.Sqrt(angdis);

        // normalize angular distance between 0 and 1
        float normalzed_angdis = (angdis - maxdis) / (mindis - maxdis);

        // main chunk of setting spin speed and clip volume
        if (r.IsVisibleFrom(Camera.main))
        {
            Debug.Log(angdis);
            // between min and max bounds, volume and turning speed scale between 0 and 1
            if (angdis <= maxdis && angdis > mindis) 
            {
               // clip.volume = normalzed_angdis;
                foreach (Transform x in transform)
                    foreach (Transform y in x.transform)
                    {
                        Vector3 r = new Vector3(0, turnspeed * normalzed_angdis, 0);
                        r.Scale(v3_deltatime);
                        y.transform.Rotate(r);
                    }
            }
            // within minimum boundary, spin speed and volume at maximum
            else if (angdis <= mindis) 
            {
               // clip.volume = 1;
                foreach (Transform x in transform)
                    foreach (Transform y in x.transform)
                    {
                        Vector3 r = new Vector3(0, turnspeed, turnspeed / 2);
                        r.Scale(v3_deltatime);
                        y.transform.Rotate(r);
                    }
            }
            // outside of maximum range or out of sight
            else
            {
                //clip.volume = 0;
            }
        }
        else
        {
            //clip.volume = 0;
        }

    }
}