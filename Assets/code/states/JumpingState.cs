using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class JumpingState : IState {

        private Player player;
        private bool executeJump = false;

        public JumpingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            // Set jumping parameter in animator to true upon entering state
            player.anim.SetBool(Constants.IS_JUMPING_STATE, true);
            Debug.Log("Jumping State Loaded at: " + Time.time);
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxis(Constants.LEFT_JOY_VERTICAL);

            if (player.CheckIfAxisInUse() && !executeJump) {
                player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, player.jumpVelocity);
                executeJump = true;
            }

            // Check when joy axis is 'up' (-1) whether player is jumping. Then check if axis has already been triggered.
            // If it has not apply double jump force and enter Double Jumping state.
            if (LeftJoyV == -1 && player.anim.GetBool(Constants.IS_JUMPING_STATE)) {
                if (!player.CheckIfAxisInUse()) {
                    player.SetAxisInUse(true);
                    player.movementStateMachine.ChangeState(new DoubleJumpingState(player));
                }
            } else if (LeftJoyV > -1) {
                player.SetAxisInUse(false);
            }
        }

        public void ExecuteState_Fixed() {
            // Limit player's horizontal movement while airborn to a predefined fraction of their normal movement
            player.rb2d.AddForce(((Vector2.right * player.movementForce) * Input.GetAxis(Constants.LEFT_JOY_HORIZONTAL)) / player.airBornMovementDetraction);
            // Set speed in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));
        }

        public void ExecuteState_Late() {
            CheckForPreviousStateSwitch(200);
        }

        public async void CheckForPreviousStateSwitch(int time) {
            await Task.Delay(time);
            // Switch back to previous state if player has returned to the ground
            if (player.anim.GetBool(Constants.IS_GROUNDED_STATE) && executeJump == true) {
                Debug.Log("SWITCHING AT: " + Time.time + " GROUNDED IS TRUE");
                player.movementStateMachine.SwitchToPreviousState();
            }
        }

        public void ExitState() {
            // Set jumping parameter to false before leaving state
            player.anim.SetBool(Constants.IS_JUMPING_STATE, false);
            executeJump = false;
            Debug.Log("Exiting Jumping State");
        }
    }
}
