using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurn : BaseState
{
    //public UIManager stateUiMan;
    public PlayerTurn(BoardStateManager stateManager) : base("PlayerTurn", stateManager){
        //stateUiMan = uiMan;
    }

    public override void Enter(){
        base.Enter();//
        //playerTurn i dont think will do anything and will for now act as an idle state until the player does something to change the state like OnMouseUp
        TextMeshProUGUI testSomething = stateManager.uiMan.somethingText;
        stateManager.uiMan.somethingText.text = "Player Turn";
        //stateUiMan.somethingText.text = "PlayerTurn";
        
    }
    public override void Exit(){
        base.Exit();
    }
    public override void UpdateLogic(){
        base.UpdateLogic();
        // this is being called by the update method in StateMachine.cs so this can be used to detect a change every frame
        //might not need to do this for all states cause most logic is meant to happen once entering the state and wont need to constantly check for a change somewhere
    }
}
