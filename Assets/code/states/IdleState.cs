using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    public class IdleState : IState {

        private Player player;
        private bool axisInUse = false;

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
            float deadzone = 0.25f;

            Vector2 stickInput = new Vector2(Input.GetAxis("LeftJoystickHorizontal"), Input.GetAxisRaw("LeftJoystickVertical"));
            if (Mathf.Abs(stickInput.x) < deadzone)
                stickInput.x = 0.0f;
            if (Mathf.Abs(stickInput.y) < deadzone)
                stickInput.y = 0.0f;


            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            player.anim.SetFloat("Speed", Mathf.Abs(player.rb2d.velocity.x));

            player.rb2d.AddForce((Vector2.right * player.movementForce) * LeftJoyH);

            Debug.Log(LeftJoyV);
            if (LeftJoyV == -1) {
                if (!this.axisInUse) {
                    this.axisInUse = true;
                    Vector2 jumpForceApplied = new Vector2(0, player.jumpForce);
                    player.rb2d.AddForce(jumpForceApplied);
                }
            } else if (LeftJoyV <= 0) {
                this.axisInUse = false;
            }
        }

        public void ExitState() {
            Debug.Log("Exiting Idle State");
        }
    }
}
