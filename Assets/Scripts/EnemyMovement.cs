using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
  private Transform target;
  private int wavepointIndex = 0;
  public float speed = 6.0f;
  private Enemy enemy;

  private Transform hero;
  private float distance = 0.0f;

  private Animator anim;

  void Start()
  {
    enemy = GetComponent<Enemy>();
    target = Waypoints.points[0];

    hero = GameObject.FindGameObjectWithTag("Hero").transform;
    anim = GetComponent<Animator>();

    enemy.speed = enemy.startSpeed;
  }

  private void Update()
  {
    speed = enemy.speed;
    anim = WaveSpawner.anim;

    Vector3 dir = target.position - transform.position;
    distance = Vector3.Distance(transform.position, hero.transform.position);

    if (distance <= 5.5f)
    {
      transform.Translate(0.001f, 0.001f, 0.001f);
      MonsterAttack();
    }
    else
    {
      MonsterNormalMovement(dir);
    }

    //if (Vector3.Distance(transform.position, target.position) <= 0.4f)
    //{
    //    GetNextWaypoint();
    //}
  }

  void MonsterNormalMovement(Vector3 dir)
  {
    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    transform.LookAt(target);

    if (Vector3.Distance(transform.position, target.position) <= 0.4f)
    {
      GetNextWaypoint();
    }

  }

  void MonsterAttack()
  {
    if (anim != null)
    {
      switch (anim.name)
      {
        case "succubus_warrior(Clone)":
          anim.SetInteger("battle", 1);
          anim.SetInteger("moving", 4);
          break;
        case "Frogman_beast_blu(Clone)":
          anim.SetInteger("battle", 1);
          anim.SetInteger("moving", 10);
          break;
        case "iguana_warrior(Clone)":
          anim.SetInteger("battle", 1);
          anim.SetInteger("moving", 3);
          break;
        case "Monster_rabbit(Clone)":
          anim.SetInteger("battle", 1);
          anim.SetInteger("moving", 3);
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

    if (PlayerStats.Lives == 0)
    {
      SceneManager.LoadScene("GameResult");
    }
    if (WaveSpawner.EnemiesAlive == 0 && PlayerStats.Rounds == 7)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }
}
