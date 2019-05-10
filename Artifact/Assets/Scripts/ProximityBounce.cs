using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// changes volume, deform, and spinning of gameobject based on proximity

public class ProximityBounce : MonoBehaviour
{

    public AnimationCurve b;
    public float spinspeed = 10.0f, bouncemult = 1f;
    public float radiusmax, radiusmin;

    private Vector3 origtrans;
    private float origy;

    private AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        origtrans = gameObject.transform.position;
        origy = origtrans.y;
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance between player and object, ignoring the difference in y
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));

        // player is within maximum radius
        if (distance <= radiusmax)
        {
            // normalizes distance between 0 and 1
            float normalzed_distance = (distance - radiusmax) / (radiusmin - radiusmax);

            transform.Rotate(0, spinspeed * normalzed_distance, 0);

            Vector3 newtrans = transform.position;
            newtrans.y = origy + (b.Evaluate(Time.time)) * normalzed_distance * bouncemult;
            transform.position = newtrans;

            clip.volume = normalzed_distance;

            
        }
        // player is outside of maxiumum radius
        else
        {
            clip.volume = 0;
        }
    }


}
