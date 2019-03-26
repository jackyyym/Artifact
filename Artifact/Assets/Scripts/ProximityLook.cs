using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityLook : MonoBehaviour
{
    public float maxdis = 15, mindis = 5;
    public float turnspeed;
    private Vector3 ymask = new Vector3(1, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform camtrans = Camera.main.transform;

        Vector3 dir = transform.position - camtrans.position;
        float angle = Vector3.Angle(Vector3.Scale(dir, ymask), camtrans.forward);
        float distance = Vector3.Distance(camtrans.position, transform.position);

        float angdis = (Mathf.Pow(distance, 2) + Mathf.Pow(distance, 2)) - (2 * distance * distance * Mathf.Cos(angle * Mathf.Deg2Rad));
        angdis = Mathf.Sqrt(angdis);

        if (angdis <= maxdis && angdis > mindis)
        {
            transform.Rotate(0, turnspeed / 2, 0);
        }
        else if (angdis <= mindis)
        {
            transform.Rotate(0, turnspeed, turnspeed / 2);
        }

        
    }
}
