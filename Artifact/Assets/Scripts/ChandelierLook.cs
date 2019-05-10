using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierLook : MonoBehaviour
{
    public float maxdis = 15, mindis = 5;
    public float turnspeed = 5;

    public GameObject c_big, c_mid, c_smol;
    public GameObject aoe;
    private AudioSource clip;
    private Renderer r;
    // Start is called before the first frame update
    void Awake()
    {
        clip = GetComponent<AudioSource>();
        r = c_big.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform camtrans = Camera.main.transform;

        Vector3 dir = c_big.transform.position - camtrans.position; // target direction (from camera to object)
        float angle = Vector3.Angle(dir, camtrans.forward); // angle between camera and object
        float distance = Vector3.Distance(camtrans.position, c_big.transform.position); // distance between camera and object
    
        // use law of cosine to solve third side of isosceles  triangle, where one side is from the player camera to the object, and
        // the other side is of the same length but angled the same way as the player is looking.
        float angdis = (Mathf.Pow(distance, 2) + Mathf.Pow(distance, 2)) - (2 * distance * distance * Mathf.Cos(angle * Mathf.Deg2Rad));
        angdis = Mathf.Sqrt(angdis);

        // normalize angular distance between 0 and 1
        float normalzed_angdis = (angdis - maxdis) / (mindis - maxdis);
        // main chunk of setting spin speed and clip volume
        if (r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, c_big.transform.position))
        {
            // between min and max bounds, volume and turning speed scale between 0 and 1
            if (angdis <= maxdis && angdis > mindis)
            {
                clip.volume = normalzed_angdis;
                c_big.transform.Rotate(0, turnspeed * normalzed_angdis, 0);
                c_mid.transform.Rotate(0, -turnspeed * normalzed_angdis, 0);
                c_smol.transform.Rotate(0, turnspeed * normalzed_angdis, 0);
            }
            // within minimum boundary, spin speed and volume at maximum
            else if (angdis <= mindis)
            {
                clip.volume = 1;
                c_big.transform.Rotate(0, turnspeed, 0);
                c_mid.transform.Rotate(0, -turnspeed, 0);
                c_smol.transform.Rotate(0, turnspeed, 0);
            }
            // outside of maximum range or out of sight
            else
            {
                clip.volume = 0;
            }
        }
        else
        {
            clip.volume = 0;
        }
    }
}
