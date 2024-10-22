using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
  public TowerBlueprint blueTower;
  public TowerBlueprint redTower;
  public TowerBlueprint greenTower;

  BuildManager buildManager;

  void Start()
  {
    buildManager = BuildManager.instance;
  }

  public void SelectBlueTower()
  {
    buildManager.SelectTowerToBuild(blueTower);
  }

  public void SelectRedTower()
  {
    buildManager.SelectTowerToBuild(redTower);
  }
}
