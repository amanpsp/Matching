using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardStateManager : MonoBehaviour
{
    public PlayerTurn playerTurnState;
    public PlayerActionState playerActionState;
    public CheckingForMatches checkingState;
    public DestroyingMatches destroyingState;
    public FillingBoard fillingBoardState;
    public EnemyTurn enemyTurnState;
    public UIManager uiMan;
    public BaseState currentState;
    public Board board;
    
    
   

    private void Awake(){
        currentState = GetInitialState();
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
        //base.uiMan = FindObjectOfType<UIManager>();
        playerTurnState = new PlayerTurn(this);
        playerActionState = new PlayerActionState(this);
        checkingState = new CheckingForMatches(this);
        destroyingState = new DestroyingMatches(this);
        fillingBoardState = new FillingBoard(this);
        enemyTurnState = new EnemyTurn(this);
    }

    void Start(){
        /* currentState = GetInitialState();
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
        //base.uiMan = FindObjectOfType<UIManager>();
        playerTurnState = new PlayerTurn(this);
        checkingState = new CheckingForMatches(this);
        destroyingState = new DestroyingMatches(this);
        boardFillState = new FillingBoard(this);
        enemyTurn = new EnemyTurn(this); */
        
        //ChangeState(playerTurnState);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentState != null){
            currentState.UpdateLogic();
        }
    }

    public void ChangeStateToPlayerTurn(){
        ChangeState(playerTurnState);
    }
    public void ChangeStateToPlayerActionState(){
        ChangeState(playerActionState);
    }
    public void ChangeStateToCheckingForMatches(){
        ChangeState(checkingState);
    }
    public void ChangeStateToDestroyingMatches(){
        ChangeState(destroyingState);
    }
    public void ChangeStateToFillingBoard(){
        ChangeState(fillingBoardState);
    }
    public void ChangeStateToEnemyTurn(){
        ChangeState(enemyTurnState);
    }

    public void ChangeState(BaseState newState){
        if(currentState!=null){
        currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public BaseState CheckCurrentState(){
        return currentState;
    }

    protected virtual BaseState GetInitialState(){
        return null;
    }
}
