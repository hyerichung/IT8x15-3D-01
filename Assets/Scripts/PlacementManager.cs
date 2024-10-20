using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
  [SerializeField]
  private PreviewManager preview;

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

  // last saved preview position, for checking if the gridPosition has been changed -> start from 0
  private Vector3Int lastPreviewPosition = Vector3Int.zero;

  private void Start()
  {
    StopPlacement(); // reset placement before start

    placedObjectDatabase = new();
  }

  private void Update()
  {
    if (selectedObjectIndex < 0) return;

    Vector3 mousePosition = inputManager.GetSelectedLayerPosition(); // get current mouse pointing position
    Vector3Int gridPosition = grid.WorldToCell(mousePosition); // convert world position to grid position 

    // Debug.Log(mousePosition);

    // check last preview position and check if the grid position stem from the same mouse position
    // if it's from same mouse position, no need to proceed further validity calculation
    // if it's from different mouse position, calculate validity and update preview position
    if (lastPreviewPosition == gridPosition) return;

    bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

    // convert grid position to world position -> update preview position
    preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    // save last preview position with grid position
    lastPreviewPosition = gridPosition;
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
    preview.StartShowingPreview(database.objectData[selectedObjectIndex].Prefab);

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

    // after object is placed, lastly update last preview position with gridPosition
    lastPreviewPosition = gridPosition;

  }

  private void StopPlacement()
  {
    selectedObjectIndex = -1;

    gridPlane.SetActive(false);
    preview.StopShowingPreview();

    inputManager.onClicked -= PlaceObject;
    inputManager.OnExit -= StopPlacement;

    lastPreviewPosition = Vector3Int.zero;
  }
}
