using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    public float radiusmin = 10f, radiusmax = 50f;
    private AudioSource clip;
    public GameObject inner_ring, middle_ring, outer_ring; 

    public float spinspeed = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance between player and object, ignoring the difference in y
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));

        // player is within maximum radius
        if (distance <= radiusmax && distance > radiusmin)
        {
            // normalizes distance between 0 and 1
            float normalzed_distance = (distance - radiusmax) / (radiusmin - radiusmax);

            inner_ring.transform.Rotate(spinspeed * normalzed_distance, 0, 0, Space.Self);
            middle_ring.transform.Rotate(0, spinspeed * normalzed_distance, 0, Space.World);
            outer_ring.transform.Rotate(spinspeed * normalzed_distance, 0, 0, Space.Self);

            clip.volume = normalzed_distance;

        }
        else if (distance <= radiusmin)
        {
            inner_ring.transform.Rotate(spinspeed, 0, 0, Space.Self);
            middle_ring.transform.Rotate(0, spinspeed, 0, Space.World);
            outer_ring.transform.Rotate(spinspeed, 0, 0, Space.Self);
            clip.volume = 1;
        }
        // player is outside of maxiumum radius
        else
        {
            clip.volume = 0;
        }
    }
}