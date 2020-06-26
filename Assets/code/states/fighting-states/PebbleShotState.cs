using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class PebbleShotState : IState {

        private PlayerController player;

        public PebbleShotState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_02, true);
        }

        public void ExecuteState() {
            // Fire one projectile and return to previous state
            player.gun.FireProjectile();
            player.fightingStateMachine.SwitchToPreviousState();
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_02, false);
        }
    }
}