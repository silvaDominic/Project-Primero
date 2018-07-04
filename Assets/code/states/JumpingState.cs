using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class JumpingState : IState {

        private Player player;

        public JumpingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            // Set jumping parameter in animator to true upon entering state
            player.anim.SetBool("isJumping", true);
            Debug.Log("Jumping State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL);

            // Switch back to previous state if player has returned to the ground
            if (player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                player.movementStateMachine.SwitchToPreviousState();
            }

            // Check when joy axis is 'up' (-1) whether player is jumping. Then check if axis has already been triggered.
            // If it has not apply double jump force and enter Double Jumping state.
            if (LeftJoyV == -1 && player.anim.GetBool(Constants.IS_JUMPING_STATE)) {
                if (!player.CheckIfAxisInUse()) {
                    player.SetAxis(true);
                    Vector2 jumpForceApplied = new Vector2(0, player.doubleJumpForce);
                    player.rb2d.AddForce(jumpForceApplied);
                    player.movementStateMachine.ChangeState(new DoubleJumpingState(player));
                }
            // If joy axis is centered or 'down', reset axis state.
            } else if (LeftJoyV <= 0) {
                player.SetAxis(false);
            }
        }

        public void ExecuteState_Fixed() {
            // Limit player's horizontal movement while airborn to a predefined fraction of their normal movement
            player.rb2d.AddForce(((Vector2.right * player.movementForce) * Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL)) / player.airBornMovementDetraction);
            // Set speed in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));
        }

        public void ExitState() {
            // Set jumping parameter to false before leaving state
            player.anim.SetBool(Constants.IS_JUMPING_STATE, false);
            Debug.Log("Exiting Jumping State");
        }
    }
}
