using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller for the sphere, gathers the current levels of all four knobs and uses
// that to calculate height and rotation
public class SphereController : MonoBehaviour
{

    public KnobController[] knobs;
    public float heightmod = 2f, rotatemod = 0.5f;

    float currentheight = 0, currentrot = 0;

    private Vector3 startpos, newpos;

    private MeshRenderer mesh;

    private Color oldcolor;
    private Color newcolor = Color.red;

    public Material[] temp_mats = new Material[5]; // array of materials used to sed materials on chrome sphere

    void Awake()
    {
        startpos = transform.position;
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        oldcolor = mesh.materials[1].color;
        temp_mats = mesh.materials;
    }

    // Update is called once per frame
    void Update()
    {
        currentheight = 0;
        currentrot = 0;
        for (int i = 0; i < knobs.Length; i++)
        {
            currentheight += heightmod * knobs[i].currentlevel;
            currentrot += rotatemod * knobs[i].currentlevel;
            temp_mats[i + 1].color = Color.Lerp(oldcolor, newcolor, knobs[i].currentlevel);
            temp_mats[i + 1].SetColor("_EmissionColor", temp_mats[i + 1].color = Color.Lerp(oldcolor, newcolor, knobs[i].currentlevel));
        }
        newpos = startpos;
        newpos.y += currentheight;
        transform.position = newpos;
        transform.Rotate(new Vector3(0, currentrot / 2, currentrot) * Time.deltaTime, Space.Self);
        mesh.materials = temp_mats;
    }
}
