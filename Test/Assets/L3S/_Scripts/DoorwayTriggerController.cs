using UnityEngine;

public class DoorwayTriggerController : MonoBehaviour 
{

	private AudioSource noise;

	void Awake ()
	{
		noise = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider c)
	{
		if (c.tag == "Player")
			noise.Play ();
	}
}
