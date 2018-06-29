using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class StateMachine : MonoBehaviour {

    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState) {
        // Check if there is a current state and exit it if there is
        Debug.Log("CHANGE STATE:");
        Debug.Log("New State: " + newState);
        if (this.currentState != null) {
            this.currentState.ExitState();
        }
        
        // Assign previous state
        this.previousState = this.currentState;
        // Reassign current state and enter new state
        this.currentState = newState;
        this.currentState.EnterState();
    }

    public void ExecuteStateUpdate() {
        Debug.Log("EXECUTE STATE:");
        var stagedState = this.currentState;
        Debug.Log("Staged State: " + stagedState);
        if (stagedState != null) {
            stagedState.ExecuteState();
        }
    }

    public void SwitchToPreviousState() {
        this.currentState.ExitState();
        this.currentState = this.previousState;
        this.currentState.EnterState();
    }

}
