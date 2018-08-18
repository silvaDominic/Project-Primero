using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class SlamState : IState {

        PlayerController player;

        private float currentYpos;
        private float fallSpeed;

        public SlamState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_SPECIAL_ATTACK_01, true);
            currentYpos = player.transform.position.y;
            fallSpeed = 15f;
            player.rb2d.velocity = new Vector2(0, currentYpos);
            Debug.Log("Slam State Loaded");
        }

        public void ExecuteState() {
            
            if (currentYpos >= 7) {
                Debug.Log("Phase 3");
            } else if (currentYpos <= 5) {
                if (currentYpos <= 3) {
                    fallSpeed = 30;
                    Debug.Log("Phase 1");
                } else {
                    fallSpeed = 20;
                    Debug.Log("Phase 2");
                }
            }
            if (player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                Debug.Log("Switching to previous state");
                player.fightingStateMachine.ChangeState(new IdleFightingState(player));
            }
        }

        public void ExecuteState_Fixed() {
            player.rb2d.AddForce(Vector2.down * 30);
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_SPECIAL_ATTACK_01, false);
            Debug.Log("Leaving Slam State");
        }
    }
}
