using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{

	public AudioClip painSound;
	new private AudioSource audio;

	void Awake ()
	{
		audio = GetComponent<AudioSource>();
	}

	public void TakeDamage ()
	{
		audio.PlayOneShot(painSound);
	}
}
