using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  public static int Money;
  public int startMoney = 600;

  public static int Lives;
  public int startLives = 20;

  public static int Rounds;

  void Start()
  {
    Money = startMoney;
    Lives = startLives;

    Rounds = 0;
  }
}