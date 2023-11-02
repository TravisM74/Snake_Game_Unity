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

    public event System.Action<int> ScoreChanged;

    private int score;

   private List<GameStateBase> gameStates = new List<GameStateBase>();

   
    public int Score {
        get { return score; }
        private set {
            score = value;
            if(ScoreChanged != null){
                ScoreChanged(score);
            }
        }
    }

    public AudioManager AudioManager{
        get;
        private set;
    }

    public GameStateBase CurrentState{
        get;
        private set;
    }
    public GameStateBase PreviousState{
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // TODO: add any additional initalisation here.
        DontDestroyOnLoad(gameObject);

        InitialiseAudioManager();
        InitializeGameStateSystem();

    }

    private void InitialiseAudioManager()
    {
        // Initialise Audio Manager
        AudioManager = GetComponent<AudioManager>();
        if (AudioManager == null)
        {
            AudioManager = gameObject.AddComponent<AudioManager>();
        }
    }

    private void InitializeGameStateSystem()
    {
        gameStates.Add(new MenuState());
        gameStates.Add(new OptionsState());
        gameStates.Add(new GameOverState());
        // TODO: Only one level atm. Add support for multiple levels .
        gameStates.Add(new LevelState(1));

        foreach (GameStateBase state in gameStates)
        {
            if (GameStateBase.IsCurrentScene(state.SceneName))
            {
                // Activates the state for the scene we aare currently in 
                CurrentState = state;
                CurrentState.Activate();
                break;
            }
        }
    }

    private void Start() {
   
        Reset();
    }

    public void Reset() {
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

    public bool GoBack(){
        return Go(PreviousState.StateType);
    }

    public bool Go(GameStateBase.Type targetStateType){
        if(!CurrentState.IsValidTargetState(targetStateType)){
            return false;
        }
        GameStateBase nextState = GetStateByType(targetStateType);
        if (nextState == null){
            // state for the type targetStateType could not be found. 
          return false;  
        }
        PreviousState = CurrentState;

        CurrentState.Deactivate(); // Deactivates the current state
        CurrentState = nextState;
        CurrentState.Activate(); // activates the next state
        return true;
    }

    private GameStateBase GetStateByType(GameStateBase.Type stateType){
        foreach (GameStateBase state in gameStates){
            if (state.StateType == stateType){
                return state;
            }
        }
        return null;
    }

}
