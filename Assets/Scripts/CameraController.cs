using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private Transform target; // target that camera follows
  [SerializeField]
  private float minDistance = 3; // min distance between target and camera
  [SerializeField]
  private float maxDistance = 30;
  [SerializeField]
  private float wheelSpeed = 30;

  private float distance; // distance between target and camera

  private void Awake()
  {
    // setup distance between target and camera based on initial target and camera position
    distance = Vector3.Distance(transform.position, target.position);
  }

  private void Update()
  {
    // put the limit the distance between target and camera
    distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
    distance = Mathf.Clamp(distance, minDistance, maxDistance);
  }
}
