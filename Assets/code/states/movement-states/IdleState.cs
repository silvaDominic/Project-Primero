using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.MovementStates {

    [RequireComponent(typeof(PlayerController))]
    public class IdleState : IState {

        private PlayerController player;

        public IdleState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_MOVEMENT_IDLE_STATE, true);
            Debug.Log("Idle State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL);
            float LeftJoyH = Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL);

            // Update animator with current speed of player
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));

            // Check speed and grounded properties in animator to decide which state to switch to
            if (!player.anim.GetBool(Constants.IS_STANDARD_ATTACK_01)) {
                if (Mathf.Abs(LeftJoyH) > 0.001) {
                    player.movementStateMachine.ChangeState(new RunningState(player));
                }
            }

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
        }

        public void ExecuteState_Late() {

        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_MOVEMENT_IDLE_STATE, false);
            Debug.Log("Exiting Idle State");
        }
    }
}