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
    // Only triggers aura if the number of enemies around the tower 
    // is less than or equal to the maximum number of targets, and aura is enabled
    if (currentEnemies.Count <= Tower.numberOfTargets && targetEnabled == true)
    {
      HitTargets();

      targetEnabled = false;
    }
  }
  private IEnumerator SlowAndRestoreSpeed(Enemy enemy)
  {
    enemy.isSlowing = true;

    enemy.ApplySlow(0.5f);

    yield return new WaitForSeconds(3f);

    enemy.RestoreSpeed();

    enemy.isSlowing = false;
  }

  private void HitTargets()
  {
    foreach (Enemy currentEnemy in currentEnemies)
    {
      // If enemy no longer exists, destroys aura object and exits function
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
        StartCoroutine(SlowAndRestoreSpeed(currentEnemy));
      }
    }

    targetEnabled = true;

    currentEnemies.Clear();

    Destroy(gameObject, 2f);
  }
}
