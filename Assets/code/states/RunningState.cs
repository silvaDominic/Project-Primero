using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class RunningState : IState {

        private Player player;

        public RunningState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Running State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL);

            // Change to Idle state if players speed falls below 0.01
            if (player.anim.GetFloat(Constants.SPEED) < 0.01) {
                player.movementStateMachine.ChangeState(new IdleState(player));
            }

            // Clamp player speed
            if (player.rb2d.velocity.x > player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(player.maxMovementSpeed, player.rb2d.velocity.y);
            }

            if (player.rb2d.velocity.x < -player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(-player.maxMovementSpeed, player.rb2d.velocity.y);
            }

            // Change to jumping state if player is not grounded
            // *** Might need additional conditions in the future
            if (!player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                player.movementStateMachine.ChangeState(new JumpingState(player));
            }

            // Set speed of player in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));

            // Check when joy axis is 'up' (-1) and then check if axis has already been triggered
            // If it has not apply jump force.
            if (LeftJoyV == -1) {
                if (!player.CheckIfAxisInUse()) {
                    player.SetAxis(true);
                    Vector2 jumpForceApplied = new Vector2(0, player.jumpForce);
                    player.rb2d.AddForce(jumpForceApplied);
                }
             // If joy axis is centered or 'down', reset axis state.
            } else if (LeftJoyV <= 0) {
                player.SetAxis(false);
            }
        }

        public void ExecuteState_Fixed() {
            // Apple horizontal force continuously according to joy axis direction
            player.rb2d.AddForce((Vector2.right * player.movementForce) * Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL));
        }

        public void ExitState() {
            Debug.Log("Exiting Running State");
        }
    }
}
