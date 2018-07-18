using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class DashState : IState {

        private Player player;
        private float startDashTimer = 0.1f;
        private float dashTimer;
        private int direction = 0;

        public DashState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Entering Dash State");
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_01, true);
            dashTimer = startDashTimer;
        }

        public void ExecuteState() {
            float LeftAxisH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);
            if (LeftAxisH > 0) {
                direction = 1;
            } else if (LeftAxisH < 0) {
                direction = -1;
            }

            if (dashTimer <= 0) {
                dashTimer = startDashTimer;
                player.comboManager.Reset();
                player.rb2d.velocity = new Vector2(0, player.transform.position.y);
                player.fightingStateMachine.SwitchToPreviousState();
            } else {
                dashTimer -= Time.deltaTime;
                player.rb2d.velocity = new Vector2(30 * direction, player.transform.position.y);
            }
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_STANDARD_ATTACK_01, false);
            Debug.Log("Leaving Dash State");
        }
    }
}
