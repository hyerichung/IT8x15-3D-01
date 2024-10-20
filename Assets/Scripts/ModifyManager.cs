using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyManager : MonoBehaviour
{
  [SerializeField]
  private InputManager inputManager;
  [SerializeField]
  private Grid grid; // cell

  [SerializeField]
  private LayerMask placementLayerMask;

  [SerializeField]
  private Camera mainCamera;

  private Vector3 finalInputPosition;

  public event Action onClicked, OnExit;

  public GameObject clickedObject;

  private bool isButtonClicked = false;

  // Start is called before the first frame update

  void Update()
  {

    if (Input.GetMouseButtonDown(0))
    {
      CheckGameObject();
    }
  }

  public void Test()
  {
    // Debug.Log($"{clickedObject}, 222");
    if (clickedObject != null)
    {
      Destroy(clickedObject);
      clickedObject = null;
    }
  }

  public void OnButtonClick()
  {
    // Debug.Log($"{clickedObject}, 222");
  }

  private void CheckGameObject()
  {

    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = mainCamera.nearClipPlane;

    Ray ray = mainCamera.ScreenPointToRay(mousePosition);
    RaycastHit hit;

    // Debug.Log($"{mousePosition}, {Physics.Raycast(ray, out hit)}, {hit.collider.name}");

    if (Physics.Raycast(ray, out hit))
    {
      // Debug.Log(hit.collider.name);
      clickedObject = hit.collider.gameObject;


    }
  }
}
