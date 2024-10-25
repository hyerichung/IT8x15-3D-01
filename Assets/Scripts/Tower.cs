using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  public Transform target;
  public List<Transform> targets;

  [Header("General")]
  public float range = 15f;

  [Header("Use Missels (default)")]
  public float fireRate = 1f;
  private float fireCountdown = 0f;
  public GameObject misselPrefeb;

  [Header("Use Laser")]
  public bool useLaser = false;
  public GameObject laserPrefeb;

  [Header("Use Aura")]
  public bool useAura = false;
  public float auraRate = 0.5f;
  private float auraCountdown = 0f;
  public GameObject auraPrefeb;
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
    // 아우라
    else if (useAura)
    {
      // numberOfTargets 넘으면 타겟들 초기화
      if (targets.Count > numberOfTargets)
      {
        targets.Clear();
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
      // 미사일
      if (fireCountdown <= 0f)
      {
        ShootMissel();
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

      targets.Add(target);
    }
    else
    {
      target = null;
    }
  }

  void ShootLaser()
  {
    // GameObject laserGO = Instantiate(laserPrefeb, firePoint.position, firePoint.rotation);

    // Laser laser = laserGO.GetComponent<Laser>();

    // if (laser != null)
    // {
    //   laser.Seek(target);
    // }

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

  void ShootMissel()
  {
    GameObject misselGO = Instantiate(misselPrefeb, firePoint.position, firePoint.rotation);

    Missel missel = misselGO.GetComponent<Missel>();

    if (missel != null)
    {
      missel.Seek(target);
    }
  }

  void ShootAura()
  {
    GameObject auraGO = Instantiate(auraPrefeb, transform.position + auraOffset, Quaternion.Euler(-90, 0, 0));

    Aura aura = auraGO.GetComponent<Aura>();

    if (aura != null)
    {
      aura.setTargets(targets);
    }
  }


  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
