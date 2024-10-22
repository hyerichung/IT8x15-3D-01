using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
  public TowerBlueprint blueTower;

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
    //  buildManager.SelectTowerToBuild(blueTower);
  }
}
