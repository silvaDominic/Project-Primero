using System.Threading.Tasks;
using UnityEngine;

using Assets.Code.Interfaces;
using Assets.Code.Scripts;
using Assets.Code.States.FightingStates;

namespace Assets.Code.States.MovementStates {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(PlayerController))]
    public class JumpingState : IState {

        private PlayerController player;
        private bool executedJump = false;
        private bool isJumping;
        private bool isDoubleJumping;
        private bool isPerformingFightingMove;

        public JumpingState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            // Set jumping parameter in animator to true upon entering state
            player.anim.SetBool(Constants.IS_JUMPING_STATE, true);
            isJumping = player.anim.GetBool(Constants.IS_JUMPING_STATE);
            isPerformingFightingMove = false;
            // Debug.Log("Jumping State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxis(Constants.LEFT_JOY_VERTICAL);

            if (player.CheckIfLeftAxisInUse() && !executedJump) {
                player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, player.jumpVelocity);
                executedJump = true;
            }

            if (player.gameControllerCombos[Constants.SLAM].Check()) {
                if (isJumping) {
                    isPerformingFightingMove = true;
                    player.fightingStateMachine.ChangeState(new SlamState(player));
                }
            }

            // Check when joy axis is 'up' (-1) whether player is jumping. Then check if axis has already been triggered.
            // If it has not apply double jump force and enter Double Jumping state.
            if (LeftJoyV == -1 && player.anim.GetBool(Constants.IS_JUMPING_STATE)) {
                if (!player.CheckIfLeftAxisInUse()) {
                    player.SetLeftAxisInUse(true);
                    player.movementStateMachine.ChangeState(new DoubleJumpingState(player));
                }
            } else if (LeftJoyV > -1) {
                player.SetLeftAxisInUse(false);
            }
        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);
            // Limit player's horizontal movement while airborn to a predefined fraction of their normal movement
            if (!isPerformingFightingMove) {
                player.rb2d.AddForce(((Vector2.right * player.movementForce) * LeftJoyH) / player.airBornMovementDetraction);
            }
            // Set speed in animator
            player.anim.SetFloat(Constants.SPEED, Mathf.Abs(player.rb2d.velocity.x));
        }

        public void ExecuteState_Late() {
            CheckForPreviousStateSwitch(250);
        }

        public async void CheckForPreviousStateSwitch(int time) {
            await Task.Delay(time);
            // Switch back to previous state if player has returned to the ground
            if (player.anim.GetBool(Constants.IS_GROUNDED_STATE) && executedJump) {
                player.movementStateMachine.SwitchToPreviousState();
            }
        }

        public void ExitState() {
            // Set jumping parameter to false before leaving state
            player.anim.SetBool(Constants.IS_JUMPING_STATE, false);
            executedJump = false;
            // Debug.Log("Exiting Jumping State");
        }
    }
}
