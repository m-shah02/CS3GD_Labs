using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour 
{

	void OnTriggerEnter (Collider c)
	{
		Rigidbody b = c.GetComponent<Rigidbody> ();
		ParticleSystem visuals = GetComponentInChildren<ParticleSystem>();
		StartCoroutine(KillAfterSeconds(visuals.main.duration));
		if (b != null) 
		{
			b.AddExplosionForce(500, transform.position - new Vector3(0,1,0), 100);
		}
	}	

	private IEnumerator KillAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}

	void Update () 
	{
		
	}
}
