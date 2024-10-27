using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
  [HideInInspector]
  public GameObject tower;
  [HideInInspector]
  public TowerBlueprint towerBlueprint;
  [HideInInspector]
  public bool isUpgraded = false;

  private BuildManager buildManager;
  private Renderer renderer;

  public Color startColor;
  public Color hoverColor;
  public Color disabledColor;
  public Vector3 positionOffset;

  public Vector3 GetBuildPosition()
  {
    return transform.position + positionOffset;
  }

  void Start()
  {
    renderer = GetComponent<Renderer>();
    startColor = renderer.material.color;

    buildManager = BuildManager.instance;
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      renderer.material.color = startColor;
      buildManager.StopBuildTowerOn();
    }
  }

  void BuildTower(TowerBlueprint blueprint)
  {
    if (PlayerStats.Money < blueprint.cost) return;

    PlayerStats.Money -= blueprint.cost;

    GameObject _tower = Instantiate(blueprint.prefeb, GetBuildPosition(), Quaternion.identity);
    tower = _tower;

    towerBlueprint = blueprint;
  }

  public void UpgradeTower()
  {
    if (PlayerStats.Money < towerBlueprint.upgradeCost) return;

    PlayerStats.Money -= towerBlueprint.upgradeCost;

    Destroy(tower); // remove old tower

    // build upgraded tower
    GameObject _tower = Instantiate(towerBlueprint.upgradedPrefeb, GetBuildPosition(), Quaternion.identity);
    tower = _tower;

    isUpgraded = true;
  }

  void OnMouseEnter()
  {
    if (!buildManager.HasTowerBlueprint) return;

    if (EventSystem.current.IsPointerOverGameObject()) return;

    if (tower != null || !buildManager.HasMoney)
    {
      renderer.material.color = disabledColor;
    }
    else
    {
      renderer.material.color = hoverColor;
    }
  }

  void OnMouseDown()
  {
    if (EventSystem.current.IsPointerOverGameObject()) return;

    if (tower != null)
    {
      buildManager.SelectNode(this);
      return;
    };

    if (!buildManager.HasTowerBlueprint) return;

    BuildTower(buildManager.GetTowerToBuild());
  }

  void OnMouseExit()
  {
    renderer.material.color = startColor;
  }
}
