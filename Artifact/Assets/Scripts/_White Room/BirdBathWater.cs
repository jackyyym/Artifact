using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
        if (inside && !deforming && player.triggerAction.stateDown)
        {
            if (!active)
                StartCoroutine(ToggleOn());
            else
                StartCoroutine(ToggleOff());
        }

        ripple.Offset = offsetcurve.Evaluate(Time.time);
        
    }

    IEnumerator ToggleOn()
    {
        deforming = true;
        float timepassed = 0;
        Vector3 startpos = transform.position;
        Vector3 endpos = transform.position;
        endpos.y += heightchange;
        while (timepassed < deformtime)
        {
            timepassed += Time.deltaTime;
            ripple.Amplitude = (timepassed / deformtime) * max_amp;
            track.volume = timepassed / deformtime;
            transform.position = Vector3.Lerp(startpos, endpos, smoothcurve.Evaluate(timepassed / deformtime));
            yield return null;
        }
        ripple.Amplitude = max_amp;
        track.volume = 1;
        deforming = false;
        active = true;
        transform.position = endpos;   
        yield break;
    }

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
