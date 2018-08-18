using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class DashState : IState {

        private PlayerController player;
        private float startDashTimer = 0.10f;
        private float dashTimer;
        private int direction = 0;

        public DashState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Entering Dash State");
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_01, true);
            dashTimer = startDashTimer;
        }

        public void ExecuteState() {

        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);
            if (LeftJoyH > 0) {
                direction = 1;
            } else if (LeftJoyH < 0) {
                direction = -1;
            }

            if (dashTimer <= 0) {
                dashTimer = startDashTimer;
                player.rb2d.velocity = new Vector2(0, player.transform.position.y);
                player.fightingStateMachine.SwitchToPreviousState();
            } else {
                dashTimer -= Time.deltaTime;
                player.rb2d.velocity = new Vector2(30 * direction, player.transform.position.y);
            }
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_01, false);
            Debug.Log("Leaving Dash State");
        }
    }
}
