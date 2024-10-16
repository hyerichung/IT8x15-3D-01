using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private float rotateSpeedX = 3;
  private float rotateSpeedY = 5;

  private float limitMinX = -80;
  private float limitMaxX = 50;

  private float eulerAngleY;
  private float eulerAngleX;

  public void RotateTo(float mouseX, float mouseY)
  {
    // mouse left/right moving -> AngleY should be changed for angle change
    eulerAngleY += mouseX * rotateSpeedX;
    // mouse up/down moving -> AngleX should be changed for angle change
    // camera downwards -> ++ but mouse downwards -> -- -> its opposite, so use -
    eulerAngleX -= mouseY * rotateSpeedY;
    // put the limitation angle for angleX as we don't want 
    eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
    // use angles for quaternion 
    transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
  }

  private float ClampAngle(float angle, float min, float max)
  {
    if (angle > 360)
    {
      angle -= 360;
    }
    else if (angle < -360)
    {
      angle += 360;
    }

    // keep angle range min <= angle <= max 
    return Mathf.Clamp(angle, min, max);
  }
}
