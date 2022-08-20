using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingBoard : BaseState
{
    public FillingBoard(BoardStateManager stateManager) : base("FillingBoard",stateManager){

    }

    public override void Enter(){
        base.Enter();
        
    }
    public override void UpdateLogic(){
        base.UpdateLogic();
        // this is being called by the update method in StateMachine.cs so this can be used to detect a change every frame
        //might not need to do this for all states cause most logic is meant to happen once entering the state and wont need to constantly check for a change somewhere
    }
}
