using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Jacky McGrath May 2019

// This script controls toggleing the bird baths on and off
public class BirdBathWater : MonoBehaviour
{
    private bool inside = false;    // true while a hand is in the water
    private bool deforming = false; // true while water is currently deforming
    private bool active = false;

    private Deform.RippleDeformer ripple;

    private float deformtime = 1f;
    private float max_amp = .4f;
    private float heightchange = .2f;
    public float radiusmax = 40f;
    private AudioSource track;

    public PlayerMove player;

    public AnimationCurve offsetcurve;
    public AnimationCurve smoothcurve;

    public Material water_highlight;
    private Material water_normal;
    private MeshRenderer meshrenderer;

    private void Awake()
    {
        ripple = GetComponentInChildren<Deform.RippleDeformer>();
        track = GetComponent<AudioSource>();
        meshrenderer = GetComponent<MeshRenderer>();
        water_normal = meshrenderer.material;
    }

    private void FixedUpdate()
    {
        // player pulls trigger while controller is inside of the water
        if (inside && !deforming && player.triggerAction.stateDown)
        {
            if (!active)
                StartCoroutine(ToggleOn());
            else
                StartCoroutine(ToggleOff());
        }
        // stops playing when the player is too far
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
        if (distance > radiusmax)
            track.volume = 0;
        ripple.Offset = offsetcurve.Evaluate(Time.time);
        
    }
    
    // called when toggling bird bath on
    IEnumerator ToggleOn()
    {
        deforming = true;
        float timepassed = 0;
        Vector3 startpos = transform.position;
        Vector3 endpos = transform.position;
        // endpos is the elevated height that the bird bath water will arrive at
        endpos.y += heightchange;
        while (timepassed < deformtime)
        {
            timepassed += Time.deltaTime;
            ripple.Amplitude = (timepassed / deformtime) * max_amp; // interpolates the amplitude of the deformation
            track.volume = timepassed / deformtime; // interpolates track volume
            transform.position = Vector3.Lerp(startpos, endpos, smoothcurve.Evaluate(timepassed / deformtime)); // interpolates position
            yield return null;
        }
        // finishes transitioning between states
        ripple.Amplitude = max_amp;
        track.volume = 1;
        deforming = false;
        active = true;
        transform.position = endpos;
        yield break;
    }
    
    // called when toggling bird bath off, inverse of ToggleOn()
    IEnumerator ToggleOff()
    {
        deforming = true;
        float timepassed = 0;
        Vector3 startpos = transform.position;
        Vector3 endpos = transform.position;
        endpos.y -= heightchange;
        while (timepassed < deformtime)
        {
            timepassed += Time.deltaTime;
            ripple.Amplitude = (1f - (timepassed / deformtime)) * max_amp;
            track.volume = 1f - (timepassed / deformtime);
            transform.position = Vector3.Lerp(startpos, endpos, smoothcurve.Evaluate(timepassed / deformtime));
            yield return null;
        }
        ripple.Amplitude = 0;
        track.volume = 0;
        deforming = false;
        active = false;
        transform.position = endpos;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        inside = true;
        meshrenderer.material = water_highlight;
        player.haptic.Execute(0, 0.1f, 50, 1, other.GetComponent<SteamVR_Behaviour_Pose>().inputSource);
        // if (!active && !deforming)
        //     ripple.Amplitude = 0.03f;
    }

    private void OnTriggerExit(Collider other)
    {
        inside = false;
        meshrenderer.material = water_normal;
        // if (!active && !deforming)
        //     ripple.Amplitude = 0;
    } 
}
