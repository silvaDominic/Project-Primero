using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    public class JumpingState : IState {

        private Player player;

        public JumpingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Jumping State Loaded");
        }

        public void ExecuteState() {
            Debug.Log("JUMPING STATE: " + player.CheckIfGrounded());
            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            if (player.anim.GetBool("isGrounded")) {
                player.movementStateMachine.SwitchToPreviousState();
            }
        }

        public void ExecuteState_Fixed() {

        }

        public void ExitState() {
            Debug.Log("Exiting Jumping State");
        }
    }
}
