
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class GroundCheck : MonoBehaviour {

        private PlayerController player;
        private RaycastHit2D groundCheck;
        public float groundCheckThreshhold = 0f;

        private void Start() {
            player = GetComponentInParent<PlayerController>();
        }

        private void Update() {
            groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckThreshhold, LayerMask.GetMask(Constants.GROUND_LAYER));
            if (groundCheck) {
                // Set parameter in animator to true if colliding with ground
                player.anim.SetBool(Constants.IS_GROUNDED_STATE, true);
            } else if (!groundCheck) {
                // Set parameter in animator to true if colliding with ground
                player.anim.SetBool(Constants.IS_GROUNDED_STATE, false);
            }
        }

        private void FixedUpdate() {
            
        }
    }
}
