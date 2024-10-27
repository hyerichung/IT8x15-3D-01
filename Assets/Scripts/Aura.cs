using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Aura : MonoBehaviour
{
  public GameObject impactEffect;

  private bool targetEnabled = true;
  private List<Enemy> currentEnemies = new List<Enemy>();

  public void setTargets(List<Enemy> _currentEnemies)
  {
    currentEnemies = _currentEnemies;
  }

  void Update()
  {
    Debug.Log(currentEnemies.Count);
    // 타워 주변의 적 targetEnemies들에게 아우라 발동, 슬로우 다운 할 예정
    if (currentEnemies.Count <= Tower.numberOfTargets && targetEnabled == true)
    {
      HitTargets();

      targetEnabled = false;
    }
  }

  private IEnumerator SlowAndRestoreSpeed(Enemy enemy)
  {
    enemy.isSlowing = true;
    // 속도를 절반으로 줄임
    enemy.ApplySlow(0.5f);

    // 3초 대기
    yield return new WaitForSeconds(3f);

    // 속도를 원래 값으로 복원
    enemy.RestoreSpeed();
    enemy.isSlowing = false;  // Slow 상태 해제
  }

  private void HitTargets()
  {
    foreach (Enemy currentEnemy in currentEnemies)
    {
      if (currentEnemy == null)
      {
        Destroy(gameObject);
        return;
      }

      GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
      effectInstance.transform.position = currentEnemy.transform.position;

      Destroy(effectInstance, 2f);

      if (!currentEnemy.isSlowing)
      {
        // 각 currentEnemy에 대해 Slow를 적용한 후 3초간 기다렸다가 속도 복원
        StartCoroutine(SlowAndRestoreSpeed(currentEnemy));
      }
    }

    targetEnabled = true;
    currentEnemies.Clear();

    Destroy(gameObject, 2f);
  }
}
