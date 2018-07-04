using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class IdleState : IState {

        private Player player;

        public IdleState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Idle State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL);

            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));

            // Check speed and grounded properties in animator to decide which state to switch to
            if (Mathf.Abs(Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL)) > 0) {
                player.movementStateMachine.ChangeState(new RunningState(player));
            }

            // Change to jumping state if player is not grounded
            // *** Might need additional conditions in the future
            if (!player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                player.movementStateMachine.ChangeState(new JumpingState(player));
            }

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
        }

        public void ExitState() {
            Debug.Log("Exiting Idle State");
        }
    }
}
