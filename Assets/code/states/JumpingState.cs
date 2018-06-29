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
            
        }

        public void ExecuteState() {
            float LeftJoyV = Input.GetAxis("LeftJoystickVertical");

            player.anim.SetBool("isGrounded", player.CheckIfGrounded());
            player.anim.SetBool("isJumping", player.isJumping);

            player.rb2d.AddForce(new Vector2(0, player.jumpForce) * LeftJoyV, ForceMode2D.Impulse);
        }

        public void ExitState() {

        }
    }
}
