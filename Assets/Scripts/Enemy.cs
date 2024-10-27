using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
  public float startSpeed = 10f;

  [HideInInspector]
  public float speed;
  public int EnemyCount;

  public float startHealth = 100;
  private float health;

  public int worth = 50;

  public bool isSlowing = false;

  public GameObject deathEffect;

  [Header("Unity Stuff")]
  public Image healthBar;

  private bool isDead = false;

  void Start()
  {
    speed = startSpeed;
    health = startHealth;
  }

  public void TakeDamage(float amount)
  {
    health -= amount;

    if (health <= 0 && !isDead)
    {
      Die();
    }
  }

  public void ApplySlow(float factor)
  {
    speed = startSpeed * factor;
  }

  public void RestoreSpeed()
  {
    speed = startSpeed;
  }

  void Die()
  {
    isDead = true;

    PlayerStats.Money += worth;

    WaveSpawner.EnemiesAlive--;

    Destroy(gameObject);

    if (WaveSpawner.EnemiesAlive == 0 && PlayerStats.Rounds == 7)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }
}
