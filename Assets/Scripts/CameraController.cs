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
  [SerializeField]
  private float rotateSpeedX = 500; // rotate speed for camera Y angle
  [SerializeField]
  private float rotateSpeedY = 250; // rotate speed for camera X angle

  private float minLimitY = 5; // rotatation min limit camera X angle
  private float maxLimitY = 50; // rotatation max limit camera X angle

  private float distance; // distance between target and camera

  private float eulerAngleY; // mouse movement direction
  private float eulerAngleX;


  private void Awake()
  {
    // setup distance between target and camera based on initial target and camera position
    distance = Vector3.Distance(transform.position, target.position);
    // setup initial camera rotation value
    Vector3 angles = transform.eulerAngles;
    eulerAngleX = angles.x;
    eulerAngleY = angles.y;
  }

  private void Update()
  {
    if (target == null) return;

    // only the right mouse is pressing
    if (Input.GetMouseButton(1))
    {
      // mouse left/right moving -> AngleY should be changed for angle change
      eulerAngleX += Input.GetAxis("Mouse X") * rotateSpeedX * Time.deltaTime;
      // mouse up/down moving -> AngleX should be changed for angle change
      // camera downwards -> ++ but mouse downwards -> -- -> its opposite, so use -
      eulerAngleY -= Input.GetAxis("Mouse Y") * rotateSpeedY * Time.deltaTime;

      // put the limitation angle for angleX as we don't want 
      eulerAngleY = ClampAngle(eulerAngleY, minLimitY, maxLimitY);
      // use angles for quaternion 
      transform.rotation = Quaternion.Euler(eulerAngleY, eulerAngleX, 0);
    }

    // put the limit the distance between target and camera
    distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
    distance = Mathf.Clamp(distance, minDistance, maxDistance);
  }

  private void LateUpdate()
  {
    if (target == null) return;

    // camera will follow the target position with distance differences
    transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
  }


  private float ClampAngle(float angle, float min, float max)
  {
    if (angle < -360) angle += 360;
    if (angle > 360) angle -= 360;

    // keep angle range min <= angle <= max 
    return Mathf.Clamp(angle, min, max);
  }
}
