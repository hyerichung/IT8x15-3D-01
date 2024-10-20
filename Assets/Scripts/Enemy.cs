using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float speed = 15f;

  private Transform target;
  private int wayPointIndex = 1;

  void Start()
  {
    target = Waypoints.points[0];
  }

  void Update()
  {
    Vector3 dir = target.position - transform.position;

    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    if (Vector3.Distance(transform.position, target.position) <= 0.2f)
    {
      GetNextWaypoint();
    }
  }

  private void GetNextWaypoint()
  {
    if (wayPointIndex >= Waypoints.points.Length - 1)
    {
      Destroy(gameObject);
      return;
    }

    wayPointIndex++;
    target = Waypoints.points[wayPointIndex];

  }
}
