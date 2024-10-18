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
  private Renderer gridIndicatorPreview;

  [SerializeField]
  private Grid grid; // cell
  [SerializeField]
  private GameObject gridPlane; // bunch of cells

  [SerializeField]
  private InputManager inputManager;

  [SerializeField]
  private ObjectDatabaseScriptable database; // placeable object prefeb database
  private int selectedObjectIndex = -1;

  // Dictionary of placed GameObject
  private PlacedObjectDatabase placedObjectDatabase;
  // List of placed GameObject
  private List<GameObject> placedObjectList = new();

  private void Start()
  {
    StopPlacement(); // reset placement before start

    placedObjectDatabase = new();
    gridIndicatorPreview = gridIndicator.GetComponentInChildren<Renderer>();
  }

  private void Update()
  {
    if (selectedObjectIndex < 0) return;

    Vector3 mousePosition = inputManager.GetSelectedLayerPosition(); // get current mouse pointing position
    Vector3Int gridPosition = grid.WorldToCell(mousePosition); // convert world position to grid position 

    bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

    gridIndicatorPreview.material.color = placementValidity ? Color.white : Color.red;

    mouseIndicator.transform.position = mousePosition;
    gridIndicator.transform.position = grid.CellToWorld(gridPosition); // convert grid position to world position
  }

  private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
  {
    Vector2Int selectedObjectSize = database.objectData[selectedObjectIndex].Size;

    return placedObjectDatabase.IsObjectPlaceable(gridPosition, selectedObjectSize);
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

    bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
    if (placementValidity == false) return;

    GameObject newGameObject = Instantiate(database.objectData[selectedObjectIndex].Prefab);
    newGameObject.transform.position = grid.CellToWorld(gridPosition);

    // store newGameObject to placedObjectList
    placedObjectList.Add(newGameObject);

    // store newGameObject to placedObjectDatabase
    placedObjectDatabase.AddObject(gridPosition, database.objectData[selectedObjectIndex].Size, database.objectData[selectedObjectIndex].Id, placedObjectList.Count - 1);

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
