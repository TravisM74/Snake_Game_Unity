using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameStateBase
{
    public override Type StateType{
        get{return Type.GameOver; }
    }

    public override string SceneName {
        get{ return "GameOver";}
    }

    public GameOverState() : base()
    {
        AddTargetState(Type.Level);
        AddTargetState(Type.Menu);
    }
}
