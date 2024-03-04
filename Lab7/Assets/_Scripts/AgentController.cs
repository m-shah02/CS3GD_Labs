using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour 
{
	public enum AgentState 
	{
		Idle = 0,
		Patrolling,
		Chasing
	}

	public AgentState state;
	public Transform[] waypoints;
	private NavMeshAgent navMeshAgent;
	private Animator animController;
	private int speedHashId;


	void Awake()
	{
		speedHashId    = Animator.StringToHash ("walkingSpeed");
		navMeshAgent   = GetComponent<NavMeshAgent>();
		animController = GetComponent<Animator>();

		if (waypoints.Length == 0) 
		{
			Debug.LogError("Error: list of waypoints is empty.");
		}
	}

	void Update()
	{
		if (state == AgentState.Idle)
			Idle ();
		else if (state == AgentState.Patrolling)
			Patrol ();
		else
			Chase ();
	}

	void Chase ()
	{
	}

	void Idle() 
	{
		animController.SetFloat (speedHashId, 0.0f);
	}

	void Patrol()
	{
		animController.SetFloat (speedHashId, 1.0f);
	}


}