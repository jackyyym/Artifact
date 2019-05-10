using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script is placed on each of the pedestals toggled by player, which
 * then sends a message to the sphere controller to toggle this pedestal 
 * on or off
 *
 */
public class ToggleScript : MonoBehaviour
{
	public int object_num;
	public RedRoomController controller;
	private AudioSource a;
	private bool toggled = false, moving = false;
	private float maxdis = 50f;
	private float presstime = 1f;

	private void Start()
	{
		a = gameObject.GetComponent<AudioSource>();
		a.volume = 0;
	}

	private void Update()
	{
		if (toggled && !moving)
		{
			float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
			if (distance <= maxdis)
				a.volume = 1;
			else
				a.volume = 0;
		}
		else if (!moving)
			a.volume = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		if (!toggled && !moving)
		{
			controller.ToggleOn(this);
			moving = true;
			toggled = true;
			StartCoroutine(Toggle());
		}
		else if (toggled && !moving)
		{
			controller.ToggleOff(this);
			moving = true;
			toggled = false;
			StartCoroutine(Toggle());
		}
	}

	// this script moves the pedestal up/down, as well as fades the audio
	// in and out. the pedestal cannot be toggled until it is done moving.
	IEnumerator Toggle()
	{
		float timepassed = 0f;
		Vector3 start = transform.parent.transform.position;
		Vector3 finish = transform.parent.transform.position;
		if (!toggled)
			finish.y += 0.5f;
		else
			finish.y -= 0.5f;
		while (timepassed < presstime)
		{
			transform.parent.transform.position = Vector3.Lerp(start, finish, timepassed / presstime);
			if (!toggled)
				a.volume = 1 - (timepassed / presstime);
			else
				a.volume = timepassed / presstime;
			timepassed += Time.deltaTime;
			yield return null;
		}
		moving = false;
		yield break;
	}
}
