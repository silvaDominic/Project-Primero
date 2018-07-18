using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;
using Assets.Code.Interfaces;

namespace Assets.Code.States.FightingStates {

    public class IdleFightingState : IState {

        private Player player;

        public IdleFightingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_FIGHTING_IDLE_STATE, true);
            Debug.Log("Idle Fighting State Loaded");
        }

        public void ExecuteState() {
            if (player.comboManager.CheckForCombo(player.simpleCombos[Constants.DASH]) && player.anim.GetBool(Constants.IS_MOVEMENT_IDLE_STATE)) {
                player.fightingStateMachine.ChangeState(new DashState(player));
                Debug.Log("DASH SUCCESS");
            } else {
                Debug.Log("Waiting for Input");
            }
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_FIGHTING_IDLE_STATE, false);
            Debug.Log("Exiting Idle Fighting State");
        }
    }
}
