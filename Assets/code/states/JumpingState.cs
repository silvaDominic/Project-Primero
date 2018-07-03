using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    public class JumpingState : IState {

        private Player player;

        public JumpingState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            player.anim.SetBool("isJumping", true);
            Debug.Log("Jumping State Loaded");
        }

        public void ExecuteState() {
            float LeftJoyH = Input.GetAxis("LeftJoystickHorizontal");

            player.rb2d.AddForce(((Vector2.right * player.movementForce) * LeftJoyH) / player.airBornMovementDetraction);
            player.anim.SetFloat("Speed", Mathf.Abs(player.rb2d.velocity.x));

            Debug.Log("JUMPING STATE: " + player.CheckIfGrounded());
            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            if (player.anim.GetBool("isGrounded")) {
                player.movementStateMachine.SwitchToPreviousState();
            }
        }

        public void ExecuteState_Fixed() {

        }

        public void ExitState() {
            player.anim.SetBool("isJumping", false);
            Debug.Log("Exiting Jumping State");
        }
    }
}
