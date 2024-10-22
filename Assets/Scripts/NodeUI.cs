using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
  public GameObject ui;
  public Text upgradeCost;
  public Button upgradeButton;
  public Vector3 towerOffset;

  private Node target;

  public void SetTarget(Node _target)
  {
    target = _target;
    transform.position = target.GetBuildPosition() + towerOffset;

    if (!target.isUpgraded)
    {
      upgradeCost.text = "$" + target.towerBlueprint.upgradeCost.ToString("F0");
      upgradeButton.interactable = true;
    }
    else
    {
      upgradeCost.text = "DONE";
      upgradeButton.interactable = false;
    }

    ui.SetActive(true);
  }

  public void Hide()
  {
    ui.SetActive(false);
  }

  public void Upgrade()
  {
    target.UpgradeTower();
    BuildManager.instance.DeSelectNode();
  }
}
