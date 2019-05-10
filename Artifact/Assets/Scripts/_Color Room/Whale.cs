using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    private Color[] colors = {Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow};
    private MeshRenderer mesh;

    private float emissiontime = 5f;

    public Light light, light2;
    public Deform.SineDeformer sine;
    float sinetimer = 0.0f, maxsinetime = 4f;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        StartCoroutine(ColorCycle());
    }

    void Update()
    {
        sinetimer += Time.deltaTime;
        if (sinetimer > maxsinetime)
            sinetimer = 0.0f;
        sine.Offset = sinetimer / maxsinetime;
    }

    IEnumerator ColorCycle()
    {
        int current_color = 0, nextcolor = 1;
        float timepassed = 0;
        while(true)
        {
            
            mesh.material.SetColor("_EmissionColor", Color.LerpUnclamped(colors[current_color], colors[nextcolor], timepassed / emissiontime));
            light.color = Color.LerpUnclamped(colors[current_color], colors[nextcolor], timepassed / emissiontime);
            light2.color = Color.LerpUnclamped(colors[current_color], colors[nextcolor], timepassed / emissiontime);
            timepassed += Time.deltaTime;
            if (timepassed > emissiontime)
            {
                timepassed = 0;
                current_color++;
                nextcolor++;
                if (current_color == colors.Length) current_color = 0;
                if (nextcolor == colors.Length) nextcolor = 0;
            }
            yield return null;
        }
    }
}
