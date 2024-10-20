using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
  public Transform enemyPrefeb;
  public Transform spawnPoint;

  public float timeBetweenWaves = 5f;
  private float countdown = 2f;

  private int waveIndex = 0;

  void Update()
  {
    if (countdown <= 0f)
    {
      StartCoroutine(SpawnWave());
      countdown = timeBetweenWaves;
    }

    countdown -= Time.deltaTime;
  }

  IEnumerator SpawnWave()
  {
    for (int i = 0; i < waveIndex; i++)
    {
      SpawnEnemy();
      yield return new WaitForSeconds(2f);
    }

    waveIndex++;
  }

  private void SpawnEnemy()
  {
    if (enemyPrefeb == null) return;
    Instantiate(enemyPrefeb, spawnPoint.position, spawnPoint.rotation);
  }
}
