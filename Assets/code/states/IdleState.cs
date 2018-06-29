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

        }

        public void ExecuteState() {
            float LeftJoyH = Input.GetAxis("LeftJoystickHorizontal");

            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            player.anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")));

            player.rb2d.AddForce((Vector2.right * player.movementForce) * LeftJoyH);
        }

        public void ExitState() {

        }
    }
}
