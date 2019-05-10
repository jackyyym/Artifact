using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script is placed on the chrome sphere in the red room, and controls the
 * movement and material changing of the sphere and the pedestals.
 * 
 */
public class RedRoomController : MonoBehaviour
{

    public AnimationCurve ballcurve;
    public Material[] mats = new Material[5]; // 0-3 = red1 - red4, 4 = black
    public MeshRenderer[] toggle_light = new MeshRenderer[4]; // plates that change color when toggled
    private bool[] toggle_bool = new bool[4];
    private MeshRenderer mesh;
    private ReflectionProbe probe;
    private Vector3 startpos;
    private AudioSource chime;

    private float heightmod = 7f;
    private float movetime = 2f;
    private float rotatespeed = 0f;
    private float rotatemod = 3f;

    public Material[] temp_mats = new Material[5]; // array of materials used to sed materials on chrome sphere

    void Awake()
    {
        startpos = transform.position;
        chime = GetComponent<AudioSource>();
        mesh = gameObject.GetComponent<MeshRenderer>();
        probe = GetComponent<ReflectionProbe>();
        probe.RenderProbe();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotatespeed / 2, rotatespeed) * Time.deltaTime, Space.Self);
    }

    public void ToggleOn(ToggleScript s)
    {
        int i = s.object_num;
        toggle_bool[i] = true;
        toggle_light[i].material = mats[i];
        temp_mats[i + 1] = mats[i];
        mesh.materials = temp_mats;

        chime.pitch = 1f;
        chime.Play();

        float newheight = 0;
        rotatespeed = 0;
        foreach (bool b in toggle_bool)
            if (b)
            {
                newheight += heightmod;
                rotatespeed += rotatemod;
            }
        StopAllCoroutines();
        StartCoroutine(MoveSphere(newheight));
    }

    public void ToggleOff(ToggleScript s)
    {
        int i = s.object_num;
        toggle_bool[i] = false;
        toggle_light[i].material = mats[4];
        temp_mats[i + 1] = mats[4];
        mesh.materials = temp_mats;

        chime.pitch = 0.80f;
        chime.Play();

        float newheight = 0;
        rotatespeed = 0f;
        foreach (bool b in toggle_bool)
            if (b)
            {
                newheight += heightmod;
                rotatespeed += rotatemod;
            }
        StopAllCoroutines();
        StartCoroutine(MoveSphere(newheight));
    }

    // moves sphere to startpos.y + newheight
    IEnumerator MoveSphere(float newheight)
    {
        Vector3 newpos = startpos; // calculates newpos from startpos
        newpos.y += newheight;
        Vector3 beginpos = transform.position; // current position of sphere for Lerps
        float timepassed = 0.0f;
        int rendercount = 0;
        while (timepassed < movetime)
        {
            timepassed += Time.deltaTime;
            transform.position = Vector3.Lerp(beginpos, newpos, ballcurve.Evaluate(timepassed / movetime));
            if (rendercount++ == 2)
            {
                //probe.RenderProbe();
                rendercount = 0;
            }
            probe.RenderProbe();
            yield return null;
        }
        probe.RenderProbe();
        yield break;
    }
}
