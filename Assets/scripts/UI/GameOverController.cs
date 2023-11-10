using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
  public TMP_Text newHighScoreText;

  private void Start(){
    if (GameManager.Instance.IsNewHighScore){
      newHighScoreText.text = $"New high score {GameManager.Instance.Score}!";
    } else {
      newHighScoreText.text = $"Your score was {GameManager.Instance.Score}.";
    }
    
  }
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
