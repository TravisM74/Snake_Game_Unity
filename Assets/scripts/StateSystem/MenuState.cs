using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameStateBase
{
    public override Type StateType {
        get{return Type.Menu;}
        
    }

    public override string SceneName {
        get {return "Menu";}
    }

    public MenuState() : base(){
        AddTargetState(Type.Options);
        AddTargetState(Type.Level);
    }

}
