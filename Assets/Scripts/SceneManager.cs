using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
  public static ScenesManager Instance;

  void Awake()
  {
    Instance = this;
  }

  public enum Scene
  {
    Start,
    Level1,
    Level2,
    GameResult,
  }

  public void LoadStartScene()
  {
    SceneManager.LoadScene(Scene.Start.ToString());
  }

  public void LoadLevel1Scene()
  {
    SceneManager.LoadScene(Scene.Level1.ToString());
  }

  public void LoadLevel2Scene()
  {
    SceneManager.LoadScene(Scene.Level2.ToString());
  }

  public void LoadGameResultScene()
  {
    SceneManager.LoadScene(Scene.GameResult.ToString());
  }

}
