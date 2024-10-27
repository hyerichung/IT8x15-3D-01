using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
  public Text gameResultText;

  private void Update()
  {
    gameResultText.text = "";

    if (PlayerStats.Lives == 0)
    {
      gameResultText.text = "Game Over 🥹";
    }
    else if (PlayerStats.Rounds == 7)
    {
      gameResultText.text = "Victory 🥳";
    }
  }
  public void PlayAgain()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }
  public void QuitGame()
  {
    Application.Quit();
  }

}
