using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private Transform target; // Target that the camera follows
  [SerializeField]
  private float minDistance = 3;
  [SerializeField]
  private float maxDistance = 30;
  [SerializeField]
  private float wheelSpeed = 30;

  private float distance;

  private void Awake()
  {
    distance = Vector3.Distance(transform.position, target.position);
  }

  private void Update()
  {
    // Adjust the distance between the target and the camera based on mouse scroll input
    distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
    // Clamp the distance to ensure it stays within min and max limits
    distance = Mathf.Clamp(distance, minDistance, maxDistance);
  }
}
