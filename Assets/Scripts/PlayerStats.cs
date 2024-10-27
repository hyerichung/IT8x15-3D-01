using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
  public static int Money;
  public int startMoney = 600;

  public static int Lives;
  public int startLives = 20;

  public static int Rounds;

  public Text livesCountdownText;

  void Start()
  {
    Money = startMoney;
    Lives = startLives;

    Rounds = 0;
  }

    private void Update()
    {
        Debug.Log("PlayerStats");
        livesCountdownText.text = "Life " + Lives.ToString() + "/20";
    }
}
