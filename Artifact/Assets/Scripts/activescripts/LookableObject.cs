using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableObject : MonoBehaviour
{
    public float lookduration = 3.0f, activeduration = 3.0f;
    public ActiveLookable activescript;
    private Renderer r;
    private MeshRenderer m;
    private bool lookrunning = false;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        m = GetComponent<MeshRenderer>();
        activescript.SetLookDuration(lookduration);
        activescript.SetActiveDuration(activeduration);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, transform.position);
        if (!lookrunning && r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            StartCoroutine(Looking());
            activescript.LookedAt();
            lookrunning = true;
        }
    }

    IEnumerator Looking()
    {
        float t = 0; // time being looked at
        while (r.IsVisibleFrom(Camera.main) && !Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            yield return new WaitForSeconds(0.05f);
            t += 0.05f;
            activescript.SetLookTime(t);
            //Debug.Log("Looking at " + gameObject.name + " for " + t + " seconds.");
            //m.material.color = Color.Lerp(Color.cyan, Color.blue, (t / lookduration));
            if (t >= lookduration)
            {
               // m.material.color = Color.yellow;
                //Debug.Log(gameObject.name + " is active!");
                activescript.Activate();
                yield return new WaitForSeconds(activeduration);

               // m.material.color = Color.grey;
                activescript.Deactivate();
                yield return new WaitForSeconds(1.0f);
                lookrunning = false;
                yield break;
            }
        }
        activescript.LookedAway();
        lookrunning = false;
       // m.material.color = Color.gray;
    }

}
