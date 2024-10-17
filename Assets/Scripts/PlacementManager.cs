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
  private Grid grid;

  private void Update()
  {
    Vector3 mousePosition = inputManager.GetSelectedLayerPosition(); // get current mouse pointing position
    Vector3Int gridPosition = grid.WorldToCell(mousePosition); // convert world position to grid position 

    mouseIndicator.transform.position = mousePosition;
    gridIndicator.transform.position = grid.CellToWorld(gridPosition); // convert grid position to world position

  }
}
