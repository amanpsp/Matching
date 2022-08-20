using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    BaseState currentState;
    //public UIManager uiMan;
    
    // Start is called before the first frame update
    void Start()
    {//
        currentState = GetInitialState();
        //uiMan = FindObjectOfType<UIManager>();
    }

    void Awake(){
        //uiMan = FindObjectOfType<UIManager>();
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
