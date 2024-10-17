using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  [SerializeField]
  private LayerMask placementLayerMask;

  [SerializeField]
  private Camera mainCamera;

  private Vector3 finalPosition;

  public Vector3 GetSelectedLayerPosition()
  {
    Vector3 mousePosition = Input.mousePosition;

    mousePosition.z = mainCamera.nearClipPlane;

    Ray ray = mainCamera.ScreenPointToRay(mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 150, placementLayerMask))
    {
      finalPosition = hit.point;
    }

    return finalPosition;
  }
}
