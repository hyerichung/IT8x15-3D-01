using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  public Transform target;
  public Enemy currentEnemy;
  public List<Enemy> currentEnemies;

  [Header("General")]
  public float range = 15f;

  [Header("Use Missiles (default)")]
  public float fireRate = 1f;
  private float fireCountdown = 0f;
  public GameObject missilePrefab;

  [Header("Use Laser")]
  public bool useLaser = false;
  public LineRenderer lineRenderer;

  [Header("Use Aura")]
  public bool useAura = false;
  public float auraRate = 0.5f;
  private float auraCountdown = 0f;
  public GameObject auraPrefab;
  public Vector3 auraOffset;
  public static float numberOfTargets = 5;

  [Header("Unity Setup Fields")]
  public Transform partToRotate;
  public string enemyTag = "Enemy";
  public float rotationSpeed = 10f;

  public Transform firePoint;

  void Start()
  {
    InvokeRepeating("UpdateTarget", 0f, 0.5f);
  }

  void Update()
  {
    if (target == null)
    {
      if (useLaser && lineRenderer.enabled)
      {
        lineRenderer.enabled = false;
      }
      return;
    }

    LockOnTarget();

    if (useLaser)
    {
      ShootLaser();
    }
    else if (useAura)
    {
      // Clear target list if it exceeds the maximum number of targets
      if (currentEnemies.Count > numberOfTargets)
      {
        currentEnemies.Clear();
      }

      if (auraCountdown <= 0f)
      {
        ShootAura();
        auraCountdown = 1f / auraRate;
      }

      auraCountdown -= Time.deltaTime;
    }
    else
    {
      if (fireCountdown <= 0f)
      {
        ShootMissile();
        fireCountdown = 1f / fireRate;
      }

      // Decrease the countdown until it reaches 0 to shoot again
      fireCountdown -= Time.deltaTime;
    }
  }

  void LockOnTarget()
  {
    // Calculate the direction to the target
    Vector3 dir = target.position - transform.position;
    // Determine the rotation needed to look at the target
    Quaternion lookRotation = Quaternion.LookRotation(dir);
    // Smoothly rotate to face the target
    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
    // Rotate only on the Y-axis
    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
  }

  void UpdateTarget()
  {
    // Find all game objects tagged as enemies
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    GameObject nearestEnemy = null;

    float shortestDistance = Mathf.Infinity;

    // Loop through each enemy to find the closest one
    foreach (GameObject enemy in enemies)
    {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

      if (distanceToEnemy < shortestDistance)
      {
        shortestDistance = distanceToEnemy;
        nearestEnemy = enemy;
        currentEnemy = enemy.GetComponent<Enemy>();
      }
    }

    // Set target if a nearby enemy is within range
    if (nearestEnemy != null && shortestDistance <= range)
    {
      target = nearestEnemy.transform;
      currentEnemies.Add(currentEnemy);
    }
    else
    {
      target = null;
      currentEnemy = null;
      currentEnemies.Clear();
    }
  }

  void ShootLaser()
  {
    if (!lineRenderer.enabled)
    {
      lineRenderer.enabled = true;
    }

    // Set the start and end points of the laser
    lineRenderer.SetPosition(0, firePoint.position);
    lineRenderer.SetPosition(1, target.position + new Vector3(0, target.position.y, 0));

    currentEnemy.TakeDamage(0.25f);
  }

  void ShootMissile()
  {
    GameObject missileGO = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);

    Missel missile = missileGO.GetComponent<Missel>();

    if (missile != null)
    {
      missile.Seek(currentEnemy);
    }
  }

  void ShootAura()
  {
    GameObject auraGO = Instantiate(auraPrefab, transform.position + auraOffset, Quaternion.Euler(-90, 0, 0));

    Aura aura = auraGO.GetComponent<Aura>();

    if (aura != null && currentEnemies.Count <= numberOfTargets)
    {
      aura.setTargets(currentEnemies);
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
