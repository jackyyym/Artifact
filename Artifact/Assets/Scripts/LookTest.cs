using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTest : MonoBehaviour
{
    private Renderer r;
    private MeshRenderer m;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        m = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (r.IsVisibleFrom(Camera.main) && !Physics.Linecast(transform.position, Camera.main.transform.position))
        {
            m.material.color = Color.yellow;
        }
        else
            m.material.color = Color.grey;
    }
}
