using UnityEngine;

public class AgentAttacking : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerHealth>().TakeDamage();
		}
	}
}
