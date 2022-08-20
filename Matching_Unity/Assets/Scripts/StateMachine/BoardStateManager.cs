using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardStateManager : MonoBehaviour
{
    public PlayerTurn playerTurnState;
    public CheckingForMatches checkingState;
    public DestroyingMatches destroyingState;
    public FillingBoard boardFillState;
    public EnemyTurn enemyTurn;
    public UIManager uiMan;
    BaseState currentState;
    public Board board;
    
    
   

    private void Awake(){
        
    }

    void Start(){
        currentState = GetInitialState();
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
        //base.uiMan = FindObjectOfType<UIManager>();
        playerTurnState = new PlayerTurn(this);
        checkingState = new CheckingForMatches(this);
        destroyingState = new DestroyingMatches(this);
        boardFillState = new FillingBoard(this);
        enemyTurn = new EnemyTurn(this);
        ChangeState(playerTurnState);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentState != null){
            currentState.UpdateLogic();
        }
    }

    public void ChangeState(BaseState newState){
        if(currentState!=null){
        currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState(){
        return null;
    }
}
