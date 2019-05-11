using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Jacky McGrath May 2019

// controller for the knobs of the mixing table
public class KnobController : MonoBehaviour
{
    public float currentlevel = 0.0f;
    private GameObject currentcontroller = null;
    private bool held = false;

    private Vector3 oldright = new Vector3();

    public PlayerMove player;

    private Material oldmat;
    public Material newmat;
    private MeshRenderer mesh;

    public GameObject arm;
    private AudioSource clip;

    public float radiusmax;
    private float numdamp = (1 / 180f); // dampens the rotation to convert from euler degrees to normalized 0 to 1
                                        // turning 180 degrees can change the normalized value from 0 to 1

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        oldmat = mesh.material;
        clip = arm.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentcontroller && player.triggerAction.stateDown)
        {
            oldright = currentcontroller.transform.right;
            held = true;
        }
        else if (currentcontroller && player.triggerAction.stateUp)
        {
            currentcontroller = null;
            mesh.material = oldmat;
            held = false;
        }
        
        // while held, rotate the knob based on controller rotation and use that to calculate new level
        if (held)
        {
            float angle = Vector3.Angle(oldright, currentcontroller.transform.right);
            Vector3 cross = Vector3.Cross(oldright, currentcontroller.transform.right);
            if (cross.y < 0)
                angle = -angle;
            transform.Rotate(0, angle, 0); // rotate knob by amount controller was turned

            currentlevel += angle * numdamp;
            currentlevel = Mathf.Clamp(currentlevel, 0.0f, 1.0f); // level must be clamped between 0 and 1
            float xrot = ((1 - currentlevel) * 90) - 50;
            arm.transform.localRotation = Quaternion.Euler(xrot, 0, 0); // rotate the arms based on rotation of the knob, clamped

            clip.volume = currentlevel;
            
            oldright = currentcontroller.transform.right;
        }
        // stops playing when the player is too far
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
        if (distance > radiusmax)
        clip.volume = 0;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            player.haptic.Execute(0, 0.1f, 50, 1, other.GetComponent<SteamVR_Behaviour_Pose>().inputSource);
            currentcontroller = other.gameObject;
            mesh.material = newmat;
        }
    }

    private  void OnTriggerExit(Collider other)
    {
        if (other.tag == "Controller" && !held)
        {
            currentcontroller = null;
            mesh.material = oldmat;
        }
    }
}
