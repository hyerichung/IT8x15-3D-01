using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
  public static BuildManager instance;
  public NodeUI nodeUI;

  private TowerBlueprint towerToBuild;
  private Node selectedNode;

  void Awake()
  {
    if (instance != null) return;

    instance = this;
  }

  public bool HasTowerBlueprint { get { return towerToBuild != null; } }
  public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

  public void SelectNode(Node node)
  {
    if (selectedNode == node)
    {
      DeSelectNode();
      return;
    }

    selectedNode = node;
    towerToBuild = null;

    nodeUI.SetTarget(node);
  }

  public void DeSelectNode()
  {
    selectedNode = null;
    nodeUI.Hide();
  }

  public void SelectTowerToBuild(TowerBlueprint tower)
  {
    towerToBuild = tower;

    DeSelectNode();
  }

  public TowerBlueprint GetTowerToBuild()
  {
    return towerToBuild;
  }

  public void StopBuildTowerOn(Node node)
  {
    towerToBuild = null;
  }
}
