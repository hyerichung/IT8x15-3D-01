using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
  [SerializeField]
  private LayerMask placementLayerMask;

  [SerializeField]
  private Camera mainCamera;

  private Vector3 finalInputPosition;

  public event Action onClicked, OnExit;

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      onClicked?.Invoke();
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      OnExit?.Invoke();
    }
  }

  public bool IsMousePointerOverUI() => EventSystem.current.IsPointerOverGameObject();

  public Vector3 GetSelectedLayerPosition()
  {
    Vector3 mousePosition = Input.mousePosition;

    // only objects located at a distance of nearClipPlane or more from the camera will be rendered
    mousePosition.z = mainCamera.nearClipPlane;

    Ray ray = mainCamera.ScreenPointToRay(mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 150, placementLayerMask))
    {
      GameObject clickedObject = hit.collider.gameObject;

      finalInputPosition = hit.point;
    }

    return finalInputPosition;
  }
}
