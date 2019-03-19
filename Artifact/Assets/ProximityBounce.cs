using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));

        if (distance <= radiusmax)
        {
            float mod = (distance - radiusmax) / (radiusmin - radiusmax);

            transform.Rotate(0, spinspeed * mod, 0);

            Vector3 newtrans = transform.position;
            newtrans.y = origy + (b.Evaluate(Time.time)) * mod * bouncemult;
            transform.position = newtrans;

            clip.volume = mod ;

            
        }
        else
        {
            clip.volume = 0;
        }

    }

    private void OnDrawGizmosSelected()
    {
       // Gizmos.DrawSphere(transform.position, radiusmax);
    }
}
