using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamController : MonoBehaviour
{
    public Transform portal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.InverseTransformVector(Camera.main.transform.position);
        transform.localPosition = new Vector3(-diff.x, transform.localPosition.y, -diff.z);
        float angle = Vector3.SignedAngle(-portal.transform.forward, Camera.main.transform.forward, Vector3.up);
        transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
