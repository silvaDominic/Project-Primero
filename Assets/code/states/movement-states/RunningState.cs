using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.MovementStates {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class RunningState : IState {

        private Player player;

        public RunningState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_RUNNING_STATE, true);
            Debug.Log("Running State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxis(Constants.LEFT_JOY_VERTICAL);
            float LeftJoyH = Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL);

            float deadzone = 0.25f;
            Vector2 stickInput = new Vector2(LeftJoyH, LeftJoyV);
            if (stickInput.magnitude < deadzone) {
                stickInput = Vector2.zero;
            }

            // Change to Idle state if players speed falls below 0.01
            if (player.anim.GetFloat(Constants.SPEED) < 0.001) {
                player.movementStateMachine.ChangeState(new IdleState(player));
            }

            // Clamp player speed
            if (player.rb2d.velocity.x > player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(player.maxMovementSpeed, player.rb2d.velocity.y);
            }

            if (player.rb2d.velocity.x < -player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(-player.maxMovementSpeed, player.rb2d.velocity.y);
            }

            // Set speed of player in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));

            // Check when joy axis is 'up' (-1) and then check if axis has already been triggered
            // If it has not apply jump force.
            if (LeftJoyV == -1 && player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                if (!player.CheckIfAxisInUse()) {
                    player.SetAxisInUse(true);
                    player.movementStateMachine.ChangeState(new JumpingState(player));
                }
            } else if (LeftJoyV > -1) {
                player.SetAxisInUse(false);
            }
        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL);
            float LeftJoyV = Input.GetAxis(Constants.LEFT_JOY_VERTICAL);

            float deadzone = 0.75f;
            Vector2 stickInput = new Vector2(LeftJoyH, LeftJoyV);
            if (stickInput.magnitude < deadzone) {
                stickInput = Vector2.zero;
            }
            // Apply horizontal force continuously according to joy axis direction
            player.rb2d.AddForce((Vector2.right * player.movementForce) * LeftJoyH);
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_RUNNING_STATE, false);
            Debug.Log("Exiting Running State");
        }
    }
}
