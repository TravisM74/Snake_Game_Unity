using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsState : GameStateBase
{
       public override Type StateType{
        get{return Type.Options; }
    }

    public override string SceneName {
        get{ return "Options";}
    }

    public OptionsState() : base(isAdditive: true)
    {
        AddTargetState(Type.Level);
        AddTargetState(Type.Menu);
    }

    public override void Activate()
    {
        Time.timeScale = 0;
        base.Activate();
    }
    public override void Deactivate()
    {
        base.Deactivate();
        Time.timeScale = 1;
    }
}
