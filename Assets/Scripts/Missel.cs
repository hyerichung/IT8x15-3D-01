using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missel : MonoBehaviour
{
  public GameObject impactEffect;

  public Enemy enemy;

  public float speed = 70f;

  public void Seek(Enemy _enemy)
  {
    enemy = _enemy;

    Debug.Log(enemy.name);
  }

  void Update()
  {
    if (enemy == null)
    {
      Destroy(gameObject);
      return;
    }

    Vector3 dir = enemy.transform.position - transform.position;

    float distanceThisFrame = speed * Time.deltaTime;

    if (dir.magnitude <= distanceThisFrame)
    { // already hit the object, overshoot
      HitTarget();
      return;
    }

    transform.Translate(dir.normalized * distanceThisFrame, Space.World);
  }

  private void HitTarget()
  {
    GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);

    enemy.TakeDamage(1000);

    Destroy(effectInstance, 2f);

    Destroy(gameObject);
  }
}
