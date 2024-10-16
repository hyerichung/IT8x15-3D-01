using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  public static float hp;
  [SerializeField]
  public static float life;
  [SerializeField]
  public static float cash;

  [SerializeField]
  public static float currentLevel;
  [SerializeField]
  public static float currentWave;

  public static bool isAlive;

  static float MAX_WAVES = 5;
  static float MAX_LEVEL = 2;

  void Awake()
  {
    hp = 300f;
    life = 20f;
    cash = 600f;
    currentLevel = 1;
    currentWave = 1;
    isAlive = true;
  }

  void Start()
  {
    if (!isAlive) return;
  }

  public void GameOver()
  {
    isAlive = false;
  }

  public void Restart()
  {
    hp = 300f;
    life = 20f;
    cash = 600f;
    currentLevel = 1;
    currentWave = 1;
    isAlive = true;
  }

  public void IncreaseHp(float health)
  {
    float addedHp = hp += health;

    if (addedHp >= 100f)
    {
      hp = 100f;
    }
    else
    {
      hp = addedHp;
    };
  }

  public void DecreaseHp(float health)
  {
    float decreasedHp = hp -= health;

    if (decreasedHp <= 0)
    {
      hp = 0;
    }
    else
    {
      hp = decreasedHp;
    }
  }

  public void DecreaseLife()
  {
    life--;
  }

  public void IncreaseCash(float money)
  {
    cash += money;
  }

  public void DecreaseCash(float money)
  {
    float decreasedCash = cash -= money;

    if (decreasedCash <= 0)
    {
      cash = 0;
    }
    else
    {

      cash -= decreasedCash;
    }
  }

  public void IncreaseLevel(float lv)
  {
    float increasedLevel = currentLevel += lv;

    if (increasedLevel > MAX_LEVEL) return;

    currentLevel = increasedLevel;
  }

  public void IncreaseWave(float wv)
  {
    float increasedWave = currentWave += wv;

    if (increasedWave > MAX_WAVES) return;

    currentWave = increasedWave;
  }

}
