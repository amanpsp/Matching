using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : BaseState
{
    public EnemyTurn(BoardStateManager stateManager) : base("Enemy Turn State",stateManager){

    }

    public override void Enter(){
        base.Enter();
        //no enemy yet so just gonna change to player turn state
        stateManager.ChangeState(stateManager.playerTurnState);
        
    }
    public override void UpdateLogic(){
        base.UpdateLogic();
        // this is being called by the update method in StateMachine.cs so this can be used to detect a change every frame
        //might not need to do this for all states cause most logic is meant to happen once entering the state and wont need to constantly check for a change somewhere
    }
}