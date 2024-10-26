using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aura : MonoBehaviour
{
  public GameObject impactEffect;

  private bool targetEnabled = true;
  private List<Transform> targetEnemies = new List<Transform>();

  public void setTargets(List<Transform> _targetEnemies)
  {
    targetEnemies = _targetEnemies;
  }

  void Update()
  {
    // 타워 주변의 적 targetEnemies들에게 아우라 발동, 슬로우 다운 할 예정
    if (targetEnemies.Count <= Tower.numberOfTargets && targetEnabled == true)
    {
      HitTargets();
      targetEnabled = false;
    }
  }

  private void HitTargets()
  {
    foreach (Transform target in targetEnemies)
    {
      GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
      effectInstance.transform.position = target.transform.position;

      Destroy(effectInstance, 2f);
    }

    targetEnabled = true;
    // targetEnemies.Clear();

    Destroy(gameObject, 2f);
  }
}
