using UnityEngine;

public class SphereController : MonoBehaviour, InteractiveObjectBase 
{
	public GameObject explosion;

	public void OnInteraction ()
	{
		GameObject.Instantiate (explosion, transform.position, transform.rotation);
	}
}
