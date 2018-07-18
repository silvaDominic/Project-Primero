using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.MovementStates {

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
        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);
            // Limit player's horizontal movement while airborn to a predefined fraction of their normal movement
            player.rb2d.AddForce(((Vector2.right * player.movementForce) * LeftJoyH) / player.airBornMovementDetraction);
            // Set speed in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));
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
