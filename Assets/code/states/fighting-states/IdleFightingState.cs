using UnityEngine;
using Assets.Code.Scripts;
using Assets.Code.Interfaces;

namespace Assets.Code.States.FightingStates {

    public class IdleFightingState : IState {

        private PlayerController player;
        private bool isMovementStateIdle;

        public IdleFightingState(PlayerController player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_FIGHTING_IDLE_STATE, true);
            isMovementStateIdle = player.anim.GetBool(Constants.IS_MOVEMENT_IDLE_STATE);
            // Debug.Log("Idle Fighting State Loaded");
        }

        public void ExecuteState() {
            if (player.anim.GetBool(Constants.IS_MOVEMENT_IDLE_STATE)) {
                if (player.gameControllerCombos[Constants.DASH].Check()) {
                    // Debug.Log("DASH SUCCESS");
                    player.fightingStateMachine.ChangeState(new DashState(player));
                }

                if (player.gameControllerCombos[Constants.PEBBLE_SHOT].Check()) {
                    // Debug.Log("PEBBLE SHOT SUCCESS");
                    player.fightingStateMachine.ChangeState(new PebbleShotState(player));
                }
            }

            if (!player.anim.GetBool(Constants.IS_RUNNING_STATE)) {
                if (player.gameControllerCombos[Constants.CRYSTAL_SHARD].Check()) {
                    // Debug.Log("CRYSTAL SHARD SUCCESS");
                    player.fightingStateMachine.ChangeState(new CrystalShardState(player));
                }
            }

            //if (player.gameControllerCombos[Constants.DASH].Check()) {
            //    if (player.anim.GetBool(Constants.IS_MOVEMENT_IDLE_STATE)) {
            //        Debug.Log("DASH SUCCESS");
            //        player.fightingStateMachine.ChangeState(new DashState(player));
            //    }
            //}

            //if (player.gameControllerCombos[Constants.PEBBLE_SHOT].Check()) {
            //    if (player.anim.GetBool(Constants.IS_MOVEMENT_IDLE_STATE)) {
            //        Debug.Log("PEBBLE SHOT SUCCESS");
            //        player.fightingStateMachine.ChangeState(new PebbleShotState(player));
            //    }
            //}

            //if (player.gameControllerCombos[Constants.CRYSTAL_SHARD].Check()) {
            //    if (!player.anim.GetBool(Constants.IS_RUNNING_STATE)) {
            //        Debug.Log("CRYSTAL SHARD SUCCESS");
            //        player.fightingStateMachine.ChangeState(new CrystalShardState(player));
            //    }
            //}
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            player.anim.SetBool(Constants.IS_FIGHTING_IDLE_STATE, false);
            Debug.Log("Exiting Idle Fighting State");
        }
    }
}
