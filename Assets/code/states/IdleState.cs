using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    public class IdleState : IState {

        private Player player;

        public IdleState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Idle State Loaded");
        }

        public void ExecuteState() {
            if (player.anim.GetFloat("Speed") > 0.01) {
                player.movementStateMachine.ChangeState(new RunningState(player));
            }

            Debug.Log("IDLE STATE: " + player.CheckIfGrounded());
            if (!player.anim.GetBool("isGrounded")) {
                player.movementStateMachine.ChangeState(new JumpingState(player));
            }
        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxis("LeftJoystickHorizontal");
            float LeftJoyV = Input.GetAxisRaw("LeftJoystickVertical");

            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            //player.anim.SetBool("isJumping", player.CheckIfJumping());
            player.anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")));

            player.rb2d.AddForce((Vector2.right * player.movementForce) * LeftJoyH);

            player.rb2d.AddForce(new Vector2(0, player.jumpForce) * -LeftJoyV);
        }

        public void ExitState() {
            Debug.Log("Exiting Idle State");
        }
    }
}
