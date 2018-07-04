using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class DoubleJumpingState : IState {

        private Player player;

        public DoubleJumpingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            // Set double jump parameter to true upon entering state
            player.anim.SetBool(Constants.IS_DOUBLE_JUMPING_STATE, true);
            Debug.Log("Double Jumping State Loaded");
        }

        public void ExecuteState() {
            //player.movementStateMachine.ChangeState(new FatiguedState());
            // Return to Idle state if double jumping and grounded state are true
            if (player.anim.GetBool(Constants.IS_DOUBLE_JUMPING_STATE) && player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                player.movementStateMachine.ChangeState(new IdleState(player));
            }
            Debug.Log("CALL FATIGUED STATE");
        }

        public void ExecuteState_Fixed() {

        }

        public void ExitState() {
            // Set double jumping parameter to false upon exiting state
            player.anim.SetBool(Constants.IS_DOUBLE_JUMPING_STATE, false);
            Debug.Log("Exiting Double Jumping State");
        }
    }
}
