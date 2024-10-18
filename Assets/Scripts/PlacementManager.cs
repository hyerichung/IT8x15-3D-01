using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
  [SerializeField]
  private GameObject mouseIndicator; // current mouse position indicator

  [SerializeField]
  private GameObject gridIndicator; // grid indicator based on current mouse indicator

  [SerializeField]
  private InputManager inputManager;
  [SerializeField]
  private Grid grid; // cell
  [SerializeField]
  private GameObject gridPlane; // bunch of cells

  [SerializeField]
  private ObjectDatabaseScriptable database;
  private int selectedObjectIndex = -1;

  private void Start()
  {
    StopPlacement(); // reset placement before start
  }

  private void Update()
  {
    if (selectedObjectIndex < 0) return;

    Vector3 mousePosition = inputManager.GetSelectedLayerPosition(); // get current mouse pointing position
    Vector3Int gridPosition = grid.WorldToCell(mousePosition); // convert world position to grid position 

    mouseIndicator.transform.position = mousePosition;
    gridIndicator.transform.position = grid.CellToWorld(gridPosition); // convert grid position to world position

  }

  public void StartPlacement(int Id)
  {
    StopPlacement(); // reset placement before start

    selectedObjectIndex = database.objectData.FindIndex(obj => obj.Id == Id); // find object in the database

    if (selectedObjectIndex < 0)
    {
      return;
    }

    gridPlane.SetActive(true);
    gridIndicator.SetActive(true);

    inputManager.onClicked += PlaceObject;
    inputManager.OnExit += StopPlacement;
  }

  private void PlaceObject()
  {
    // if the mouse pointer is over the current object's UI, do not place
    if (inputManager.IsMousePointerOverUI())
    {
      return;
    }

    Vector3 mousePosition = inputManager.GetSelectedLayerPosition();
    Vector3Int gridPosition = grid.WorldToCell(mousePosition);

    GameObject newGameObject = Instantiate(database.objectData[selectedObjectIndex].Prefab);
    newGameObject.transform.position = grid.CellToWorld(gridPosition);

  }

  private void StopPlacement()
  {
    selectedObjectIndex = -1;

    gridPlane.SetActive(false);
    gridIndicator.SetActive(false);

    inputManager.onClicked -= PlaceObject;
    inputManager.OnExit -= StopPlacement;
  }
}
