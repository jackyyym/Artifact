using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLeafMat : MonoBehaviour
{

    public Material mat;
    [ContextMenu("Change")]
    void Change()
    {
        foreach (Transform x in transform)
            foreach (Transform y in x.transform)
            {
                MeshRenderer r = y.GetComponent<MeshRenderer>();
                r.material = mat;
            }
    }
}
