using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{

  private const string HighScoreText = "High Score: {0}";
  private void Start(){
    int highscore = PlayerPrefs.GetInt(Config.HighScoreKey, defaultValue: 0);
    if (HighScoreText != null){
      highScoreText.text = string.Format(HighScoreText, highscore);
    }

  }
  public TMP_Text highScoreText;
  public void StartGame(){
    Debug.Log("Start the game");
    GameManager.Instance.Go(GameStateBase.Type.Level);


  }
  public void ToOptions(){
    Debug.Log ("Goto Options");
    GameManager.Instance.Go(GameStateBase.Type.Options);

  }
  public void Quit(){
    Application.Quit();

  }
}
