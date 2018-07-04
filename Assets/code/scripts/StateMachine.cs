using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;

public class StateMachine : MonoBehaviour {

    private IState currentState;
    private IState previousState;

    /// <summary>
    /// Changes the current state to the new state that is passed in.
    /// </summary>
    /// <param name="newState">The new state to be executed</param>
    public void ChangeState(IState newState) {
        // Check if there is a current state and exit it if there is
        if (this.currentState != null) {
            this.currentState.ExitState();
        }
        
        // Assign previous state
        this.previousState = this.currentState;
        // Reassign current state and enter new state
        this.currentState = newState;
        this.currentState.EnterState();
    }

    /// <summary>
    /// Executes a states update method
    /// This is called by the Monobehaviour REGULAR update method in the Player controller.
    /// </summary>
    public void ExecuteStateUpdate() {
        // Check if state exists and calls its execute function
        var stagedState = this.currentState;
        if (stagedState != null) {
            stagedState.ExecuteState();
        }
    }

    /// <summary>
    /// Executes a states fixed update method
    /// This is called by the Monobehaviour FIXED update method in the Player controller.
    /// </summary>
    public void ExecuteStateFixedUpdate() {
        var stagedState = this.currentState;
        if (stagedState != null) {
            stagedState.ExecuteState_Fixed();
        }
    }

    /// <summary>
    /// Switches to the previous state
    /// </summary>
    public void SwitchToPreviousState() {
        // Check if a current state exists and exit if it does.
        if (this.currentState != null) {
            this.currentState.ExitState();
        }


        // Check if a previous state exists and (re)enter it if it does.
        if (this.previousState != null) {
            this.currentState = this.previousState;
            this.currentState.EnterState();
        }
    }

}
