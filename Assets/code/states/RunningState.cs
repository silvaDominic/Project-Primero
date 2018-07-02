using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States {

    public class RunningState : IState {

        private Player player;

        public RunningState(Player player) {
            this.player = player;
        }

        public void EnterState() {
            Debug.Log("Running State Loaded");
        }

        public void ExecuteState() {
            if (player.anim.GetFloat("Speed") < 0.01) {
                player.movementStateMachine.ChangeState(new IdleState(player));
            }

            if (player.rb2d.velocity.x > player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(player.maxMovementSpeed, player.rb2d.velocity.y);
            }

            if (player.rb2d.velocity.x < -player.maxMovementSpeed) {
                player.rb2d.velocity = new Vector2(-player.maxMovementSpeed, player.rb2d.velocity.y);
            }
        }

        public void ExecuteState_Fixed() {
            float LeftJoyH = Input.GetAxis("LeftJoystickHorizontal");

            player.anim.SetBool("isGrounded", player.grounded);
            player.anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")));

            player.rb2d.AddForce((Vector2.right * player.movementForce) * LeftJoyH);
        }

        public void ExitState() {
            Debug.Log("Exiting Running State");
        }
    }
}
