using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missel : MonoBehaviour
{
  private Transform target;
  public GameObject impactEffect;

  public Enemy enemy;

  public float speed = 70f;

  public void Seek(Transform _target, Enemy _enemy)
  {
    target = _target;
    enemy = _enemy;

    Debug.Log(enemy.name);
  }

  void Update()
  {
    if (target == null)
    {
      Destroy(gameObject);
      return;
    }

    Vector3 dir = target.position - transform.position;

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
