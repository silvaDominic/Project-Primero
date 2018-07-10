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
            //Vector2 jumpForceApplied = new Vector2(0, player.doubleJumpForce);
            if (player.CheckIfAxisInUse()) {
                player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, player.doubleJumpVelocity);
                player.SetAxisInUse(false);
            }

            //if (player.rb2d.velocity.y < 0) {
            //    player.movementStateMachine.ChangeState(new FallingState(player));
            //}
        }

        public void ExecuteState_Fixed() {

        }

        public void ExecuteState_Late() {
            if (player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                Debug.Log("SWITCHING AT: " + Time.time + " GROUNDED IS TRUE");
                player.movementStateMachine.ChangeState(new IdleState(player));
            }
        }

        public void ExitState() {
            // Set double jumping parameter to false upon exiting state
            player.anim.SetBool(Constants.IS_DOUBLE_JUMPING_STATE, false);
            Debug.Log("Exiting Double Jumping State");
        }
    }
}
