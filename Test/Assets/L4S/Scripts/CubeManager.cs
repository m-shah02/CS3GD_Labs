using UnityEngine;

public class CubeManager : MonoBehaviour 
{

	public GameObject cubePrefab;
	public Transform explosionForceLocation;

	float t = 1;

	void Update ()
	{
		if (CreationRequested ()) 
		{
			CreateCube ();
		}
		t -= Time.deltaTime;
		if (t < 0) 
		{
			CreateCube ();
			t += 0.5f;
		}
		if (Input.GetKeyDown (KeyCode.G)) 
		{
			Physics.gravity *= -1.0f;
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			Explode();
		}
	}

	private void Explode ()
	{
		Rigidbody[] cubes = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody cube in cubes)
			cube.AddExplosionForce (20, explosionForceLocation.position, 10, 1, ForceMode.Impulse);
		/* 
		Alternative: loop over all child objects and get the Rigidbody component
		foreach (Transform t in transform) 
		{
			t.GetComponent<Rigidbody> ().AddExplosionForce (20, ExplosionForceLocation.position, 10, 1, ForceMode.Impulse);
		}*/
	}

	private void CreateCube () 
	{
		GameObject cube = GameObject.Instantiate (cubePrefab, transform.position, transform.rotation);
		cube.transform.parent = transform;
		//Rigidbody b = cube.GetComponent<Rigidbody> ();
		//b.AddForce(transform.forward * 20, ForceMode.Impulse);
		//b.mass = Random.Range (1, 100);
		//b.drag = Random.Range (0, 10);
	}

	private bool CreationRequested()
	{
		return Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.C);
	}
}
