using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : GameStateBase
{
    public override Type StateType {
        get{ return Type.Level;}
    }

    public override string SceneName {
        get { return "Level" + Index.ToString();}
    }

    public int Index{
        get;
    }

    public LevelState(int index) : base(){
        Index = index;
        AddTargetState(Type.GameOver);
        AddTargetState(Type.Options);
    }
}