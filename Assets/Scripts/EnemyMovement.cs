using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 6.0f;

    private GameObject hero;
    private Enemy enemy;

    //private float distacne = 0.0f;

    //private Animator anim;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];

        //hero = GameObject.FindGameObjectWithTag("Hero");
        //anim = GetComponent<Animator>();
    }

    //private void FixedUpdate()
    //{
    //    anim = WaveSpawner.anim;
    //    Debug.Log(anim.name);

    //    StartCoroutine(SetIntegers());

    //    IEnumerator SetIntegers()
    //    {
    //        yield return null;
    //        anim.SetInteger("battle", 1);
    //        anim.SetInteger("moving", 5);

    //    }
    //}

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        Vector3 directionToWaypoint = target.position - transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, directionToWaypoint, speed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
       
        //distacne = hero.transform.position.x - transform.position.x;
        
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
