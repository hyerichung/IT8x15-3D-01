using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 6.0f; private Enemy enemy;

    private GameObject hero;
    //private GameObject spwan_enemy;
    private float distacne = 0.0f;

    private Animator anim;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];

        hero = GameObject.FindGameObjectWithTag("Hero");
        //spwan_enemy = GameObject.FindGameObjectWithTag("Enemy");

        anim = GetComponent<Animator>();
    }

    private void Update()
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

        anim = WaveSpawner.anim;

        MonsterAttack();

        //Debug.Log(Vector3.Distance(hero.transform.position, transform.position));
        //if (Vector3.Distance(hero.transform.position, transform.position) < 10.0f)
        //{
        //    MonsterAttack();
        //}
        //else
        //{
        //    anim.SetInteger("battle", 0);
        //    anim.SetInteger("moving", 1);            
        //}
    }

    void MonsterAttack()
    {        
        if (anim != null)
        {
            switch (anim.name)
            {
                case "succubus_all(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 4);
                    break;
                case "Frogman_beast_blu(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 10);
                    break;
                case "iguana_mage(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 3);
                    break;
                case "Monster_rabbit(Clone)":
                    Debug.Log("Monster Attack: " + anim.name);
                    anim.SetInteger("battle", 1);
                    int n = Random.Range(0, 2);
                    if (n == 0)
                    {
                        anim.SetInteger("moving", 3);
                    }
                    else
                    {
                        anim.SetInteger("moving", 4);
                    }
                    break;
                case "Goblins_all(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 4);
                    break;
                case "golem_lava(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 5);
                    break;
                case "ent_1(Clone)":
                    anim.SetInteger("battle", 1);
                    anim.SetInteger("moving", 5);
                    break;
            }
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
