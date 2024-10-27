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

  public bool isSlowing = false;  // Slow 상태를 추적하는 변수

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

    //healthBar.fillAmount = health / startHealth;

    if (health <= 0 && !isDead)
    {
      Die();
    }
  }

  public void ApplySlow(float factor)
  {
    speed = startSpeed * factor;
    Debug.Log($"{factor}, {startSpeed * factor}, {speed}");
  }

  public void RestoreSpeed()
  {
    speed = startSpeed;
  }

  void Die()
  {
    isDead = true;

    PlayerStats.Money += worth;

    // TODO:
    // GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
    // Destroy(effect, 5f);

    WaveSpawner.EnemiesAlive--;

    Destroy(gameObject);

    if (WaveSpawner.EnemiesAlive == 0 && PlayerStats.Rounds == 7)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }
}
