using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    //
    public string name;
    public BoardStateManager stateManager;
    //public UIManager uiMan;
    public BaseState(string name, BoardStateManager stateManager){
        this.name = name;
        this.stateManager = stateManager;
        //uiMan = stateManager.uiMan;
        //might assign a board variable and things here so every state can access allGems and stuff
        //uiMan = stateMachine.uiMan;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void UpdateLogic(){}
}
