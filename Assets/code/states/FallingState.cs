using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    [RequireComponent(typeof(IState))]
    [RequireComponent(typeof(Player))]
    public class FallingState : IState {

        private Player player;

        public FallingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool("Falling", true);
        }

        public void ExecuteState() {
            if (!player.anim.GetBool(Constants.IS_DOUBLE_JUMPING_STATE) && player.anim.GetBool(Constants.IS_GROUNDED_STATE)) {
                player.movementStateMachine.ChangeState(new IdleState(player));
            }
            Debug.Log("Falling");
        }

        public void ExecuteState_Fixed() {
            
        }

        public void ExecuteState_Late() {
        }

        public void ExitState() {
            player.anim.SetBool("Falling", false);
        }
    }
}
