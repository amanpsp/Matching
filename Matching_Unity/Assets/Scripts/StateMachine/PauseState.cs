using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : BaseState
{
    public PlayerActionState(BoardStateManager stateManager) : base("Player Action State",stateManager){

    }

    public override void Enter(){
        base.Enter();
        
        stateManager.ChangeState(stateManager.playerActionState);
        
    }
    public override void UpdateLogic(){
        base.UpdateLogic();
        // this is being called by the update method in StateMachine.cs so this can be used to detect a change every frame
        //might not need to do this for all states cause most logic is meant to happen once entering the state and wont need to constantly check for a change somewhere
    }
}