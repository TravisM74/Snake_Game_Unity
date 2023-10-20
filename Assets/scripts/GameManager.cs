using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Statics
    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                GameManager prefab = Resources.Load<GameManager>("GameManager");
                if (prefab == null) {
                    Debug.LogError("Cant find a prefab for singlton GameManager from resources");
                    return null;
                }
                instance = Instantiate(prefab);
            }
            return instance;
        }
    }
    #endregion

    private int score;

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

    public int Score {
        get { return score; }
        private set {
            score = value;
            UI.setScore(score);
        }
    }

    private void Awake() {
       if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
       // TODO: add any additional initalisation here.
       DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        Snake = GameObject.FindObjectOfType<SnakeController>();
        GameBoard = GameObject.FindObjectOfType<Board>();
        UI = GameObject.FindObjectOfType<UIController>();
        
        GameBoard.Setup();
        Snake.Setup();
        
        GameBoard.CreateApple();
        Reset();
    }

    private void Reset() {
        Score = 0;
    }

    public bool AddScore (int score) {
        if (score <= 0) {
           return false;
        }
        Score += score;

        return true;
    }

    public bool SubtractScore (int score) {
        if (score <= 0) {
            return false;
        }
        Score -= score;
        return true;
    }

}
