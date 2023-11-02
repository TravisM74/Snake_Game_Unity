using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Current {
        get;
        private set;
    }
     public SnakeController Snake {
        get;
        private set;
    }
    public Board GameBoard {
        get;
        private set;
    }

    public UIController UI {
        get; 
        private set;
    }


    private void Start(){
        Current = this;
        
        Snake = GameObject.FindObjectOfType<SnakeController>();
        GameBoard = GameObject.FindObjectOfType<Board>();
        UI = GameObject.FindObjectOfType<UIController>();
        
        GameBoard.Setup();
        Snake.Setup();
        
        GameBoard.CreateApple();

        if(GameManager.Instance.CurrentState is LevelState currentLevel && currentLevel.Index == 1 ){
            // if we are currently in a level state and the level is the 1st, reset the score (GameManager) .
            GameManager.Instance.Reset();
        }
        // Starts listening to the event OnScoreChanged
        GameManager.Instance.ScoreChanged += OnScoreChanged;
        OnScoreChanged(GameManager.Instance.Score);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.Instance.Go(GameStateBase.Type.Options);
        }
    }

    private void OnDestroy(){
        // Stops listening to the event OnScoreChanged
        GameManager.Instance.ScoreChanged -= OnScoreChanged;
    }
    private void OnScoreChanged(int score)
    {
        UI.setScore(score);
    }
}
