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
  }

  void Update()
  {
    // If there is no target, destroy the missile
    if (enemy == null)
    {
      Destroy(gameObject);
      return;
    }

    // Calculate the direction towards the enemy
    Vector3 dir = enemy.transform.position - transform.position;

    // Calculate the distance the missile should move this frame
    float distanceThisFrame = speed * Time.deltaTime;

    // If the missile has reached or overshot the target, trigger impact
    if (dir.magnitude <= distanceThisFrame)
    {
      HitTarget();
      return;
    }

    // Move the missile towards the enemy
    transform.Translate(dir.normalized * distanceThisFrame, Space.World);
  }

  private void HitTarget()
  {
    GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);

    enemy.TakeDamage(30);

    Destroy(effectInstance, 2f);
    Destroy(gameObject);
  }
}
