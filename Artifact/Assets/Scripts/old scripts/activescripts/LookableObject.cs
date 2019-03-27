using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableObject : MonoBehaviour
{
    public float lookduration = 3.0f, activeduration = 3.0f;
    public ActiveLookable activescript;
    private Renderer r;
    //private MeshRenderer m;
    private bool lookrunning = false;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
       // m = GetComponent<MeshRenderer>();
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
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            activescript.SetLookTime(t);
            if (t >= lookduration)
            {
                activescript.Activate();
                yield return new WaitForSeconds(activeduration);
                activescript.Deactivate();
                yield return new WaitForSeconds(1.0f);
                lookrunning = false;
                yield break;
            }
        }
        activescript.LookedAway();
        lookrunning = false;
    }

}
