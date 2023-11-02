using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
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
