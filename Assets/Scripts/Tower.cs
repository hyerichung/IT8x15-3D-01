using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  [Header("Attributes")]
  private Transform target;
  public float range = 15f;
  public float fireRate = 1f;
  private float fireCoundown = 0f;

  [Header("Unity Setup Fields")]
  public Transform partToRotate;
  public string enemyTag = "Enemy";
  public float rotationSpeed = 10f;

  public GameObject bulletPrefeb;
  public Transform firePoint;

  void Start()
  {
    InvokeRepeating("UpdateTarget", 0f, 0.5f);
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

    if (nearestEnemy != null && shortestDistance < range)
    {
      target = nearestEnemy.transform;
    }
    else
    {
      target = null;
    }
  }

  void Update()
  {
    if (target == null) return;

    Vector3 dir = target.position - transform.position;
    Quaternion lookRotation = Quaternion.LookRotation(dir); // how do we need to rotate myself in order to look this direction?
    Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles; // smooth transition from one state to another

    partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // only want to rotate angleY

    if (fireCoundown <= 0f)
    {
      Shoot();
      fireCoundown = 1f / fireRate; // want to shoot with 0.5 bullet each second
    }

    fireCoundown -= Time.deltaTime; // when it reaches 0, shoot again

  }

  void Shoot()
  {
    GameObject misselGO = Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);

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
