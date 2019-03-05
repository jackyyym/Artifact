using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTest : MonoBehaviour
{
    public float lookduration = 3.0f, activeduration = 3.0f;
    private Renderer r;
    private MeshRenderer m;
    private bool lookrunning = false;

    private Color lerpedColor;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        m = GetComponent<MeshRenderer>();
        lerpedColor = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, transform.position);
        if (!lookrunning && r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            StartCoroutine(Looking());
            lookrunning = true;
        }
    }

    IEnumerator Looking()
    {
        float t = 0;
        while (r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            yield return new WaitForSeconds(0.05f);
            t += 0.1f;
            Debug.Log("Looking at " + gameObject + " for " + t + " seconds.");
            m.material.color = Color.Lerp(Color.cyan, Color.blue, (t / lookduration));
            if (t >= lookduration)
            {
                m.material.color = Color.yellow;
                Debug.Log(gameObject + " is active!");
                yield return new WaitForSeconds(activeduration);
                m.material.color = Color.grey;
                lookrunning = false;
                yield break;
            }
        }
        lookrunning = false;
        m.material.color = Color.gray;
    }
}
