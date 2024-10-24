using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  public Transform target;

  [Header("General")]
  public float range = 15f;

  [Header("Use Missels (default)")]
  public float fireRate = 1f;
  private float fireCountdown = 0f;
  public GameObject misselPrefeb;

  [Header("Use Laser")]
  public bool useLaser = false;
  public GameObject laserPrefeb;

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
    // 타겟 없음
    if (target == null)
    {
      if (useLaser && Laser.instance != null)
      {
        // 타겟이 없으면 레이저 사용 중지
        Laser.instance.DestroyLaser();
      }
      return;
    };

    // 타겟 존재
    LockOnTarget();

    // 레이저
    if (useLaser)
    {
      ShootLaser();
    }
    else
    {
      // 미사일
      if (fireCountdown <= 0f)
      {
        Shoot();
        // want to shoot with 0.5 missels each second
        fireCountdown = 1f / fireRate;
      }

      // when it reaches 0, shoot again
      fireCountdown -= Time.deltaTime;
    }
  }

  void LockOnTarget()
  {
    Vector3 dir = target.position - transform.position;
    // how do we need to rotate myself in order to look this direction?
    Quaternion lookRotation = Quaternion.LookRotation(dir);
    // smooth transition from one state to another
    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
    // only want to rotate angleY
    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
  }

  void UpdateTarget()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    GameObject nearestEnemy = null;

    float shortestDistance = Mathf.Infinity;

    foreach (GameObject enemy in enemies)
    {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

      if (distanceToEnemy < shortestDistance)
      {
        shortestDistance = distanceToEnemy;
        nearestEnemy = enemy;
      }
    }

    if (nearestEnemy != null && shortestDistance <= range)
    {
      target = nearestEnemy.transform;
    }
    else
    {
      target = null;
    }
  }

  void ShootLaser()
  {
    if (target != null)
    {
      if (useLaser)
      {
        Laser.GenerateLaser(laserPrefeb, firePoint);

        Laser.instance.Seek(target);
      }
      return;
    }
  }

  void Shoot()
  {
    GameObject misselGO = Instantiate(misselPrefeb, firePoint.position, firePoint.rotation);

    Missel missel = misselGO.GetComponent<Missel>();

    if (missel != null)
    {
      missel.Seek(target);
    }

  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
