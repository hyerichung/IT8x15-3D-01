using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjectDatabase
{
  // [x.x.x] :
  // this.OccupiedPositions = [[x.x.x], [x.x.x]]
  // Id = x;
  // PlacedObjectIndex = x;
  Dictionary<Vector3Int, PlacedObjectData> placedObjects = new();

  public void AddObject(Vector3Int gridPosition, Vector2Int objectSize, int id, int placedObjectIndex)
  {
    List<Vector3Int> positionToOccupy = CalculateOccupiedPositions(gridPosition, objectSize);

    PlacedObjectData newPlacedObjectData = new PlacedObjectData(positionToOccupy, id, placedObjectIndex);

    foreach (var position in positionToOccupy)
    {
      if (placedObjects.ContainsKey(position))
      {
        throw new System.Exception($"grid position is already occupied {position}");
      }

      placedObjects[position] = newPlacedObjectData;
    }
  }

  public bool IsObjectPlaceable(Vector3Int gridPosition, Vector2Int objectSize)
  {
    List<Vector3Int> positionToOccupy = CalculateOccupiedPositions(gridPosition, objectSize);

    foreach (var position in positionToOccupy)
    {
      if (placedObjects.ContainsKey(position))
      {
        return false;
      }
    }
    return true;
  }

  /*
  * Calculate position to be occupied based on selected gridPosition (Vector3) and objectSize (Vector2)
  */
  private List<Vector3Int> CalculateOccupiedPositions(Vector3Int gridPosition, Vector2Int objectSize)
  {
    List<Vector3Int> calculatedPosition = new();

    for (int x = 0; x < objectSize.x; x++)
    {
      for (int y = 0; y < objectSize.y; y++)
      {
        calculatedPosition.Add(gridPosition + new Vector3Int(x, 0, y));
      }
    }

    return calculatedPosition;
  }

  public void RemoveObject(Vector3Int gridPosition)
  {
    foreach (var position in placedObjects[gridPosition].OccupiedPositions)
    {
      placedObjects.Remove(position);
    }
  }


  public int GetPlacedObjectIndex(Vector3Int gridPosition)
  {
    if (placedObjects.ContainsKey(gridPosition) == false)
    {

      return -1;
    }

    return placedObjects[gridPosition].PlacedObjectIndex;
  }
}

public class PlacedObjectData
{
  public List<Vector3Int> OccupiedPositions;

  public int Id { get; private set; }

  public int PlacedObjectIndex { get; private set; }

  public PlacedObjectData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
  {
    this.OccupiedPositions = occupiedPositions;
    Id = id;
    PlacedObjectIndex = placedObjectIndex;
  }
}
