using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
  public void Restart(){
    Debug.Log("Restart the game");
    GameManager.Instance.Go(GameStateBase.Type.Level);

  }
  public void BackToMenu(){
    Debug.Log("Back to the main menu");
    GameManager.Instance.Go(GameStateBase.Type.Menu);

  }

  public void Quit(){
    Application.Quit();
  }
}
