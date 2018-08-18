using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class CrystalShardState : IState {

        private PlayerController player;

        public CrystalShardState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_POWER_ATTACK_01, true);
        }

        public void ExecuteState() {
            Debug.Log("DOING CRYSTAL SHARD ATTACK");
            player.fightingStateMachine.SwitchToPreviousState();
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_POWER_ATTACK_01, false);
        }
    }
}