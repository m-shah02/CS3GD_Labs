using UnityEngine;

public class CameraController : MonoBehaviour 
{
	// the target that the camera will follow
	public Transform target;

	// the distance the camera will stay from the target
	public float distanceFromTarget = 5;

	// boolean to toggle following on and off
	public bool follow = true;

	// the displacement of the camera on the Y axis
	public float displacementY = 1.5f;

	// easing variable
	public float easing = 0.1f;

	void Update () 
	{
		if (follow) 
		{
			Vector3 newPos = target.position - (Vector3.forward * distanceFromTarget);
			
			newPos.y = target.position.y + displacementY;

			transform.position += (newPos - transform.position) * easing; 

			transform.LookAt(target);
		}
		else
		{
			transform.LookAt(target);
		}
	}
}