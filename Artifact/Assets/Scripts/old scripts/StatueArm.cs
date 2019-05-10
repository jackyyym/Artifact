using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is attached to the statues arms
 * and has them point at the chrome sphere, offset
 * down to be 2 units below the sphere
 */

public class StatueArm : MonoBehaviour
{
	public GameObject sphere;

    void Update()
    {
		Vector3 temp_position = sphere.transform.position;
		temp_position.y -= 2f;
		gameObject.transform.LookAt(temp_position);
    }
}
