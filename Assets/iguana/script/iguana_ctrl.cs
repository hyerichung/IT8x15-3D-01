using UnityEngine;

public class iguana_ctrl : MonoBehaviour { 
	public float speed = 6.0f;

	private Transform target;
	private int wavepointIndex = 0;
	void Start()
	{
		//anim = GetComponent<Animator>();
		//controller = GetComponent<CharacterController>();
		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		Vector3 directionToWaypoint = target.position - transform.position;
		transform.forward = Vector3.RotateTowards(transform.forward, directionToWaypoint, speed * Time.deltaTime, 0.0f);
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
	}
	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void EndPath()
	{
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}
