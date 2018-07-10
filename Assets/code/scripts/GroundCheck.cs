using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class GroundCheck : MonoBehaviour {

        private Player player;
        private RaycastHit2D groundCheck;
        public float groundCheckThreshhold = 0f;

        private void Start() {
            player = GetComponentInParent<Player>();
        }

        private void Update() {
            groundCheck = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckThreshhold, LayerMask.GetMask(Constants.GROUND_LAYER));
            if (groundCheck) {
                //Debug.Log("Colliding with ground");
                // Set parameter in animator to true if colliding with ground
                player.anim.SetBool(Constants.IS_GROUNDED_STATE, true);
                Debug.Log("Colliding with: " + groundCheck.collider);
            } else if (!groundCheck) {
                // Set parameter in animator to true if colliding with ground
                Debug.Log("Leaving: " + groundCheck.collider);
                player.anim.SetBool(Constants.IS_GROUNDED_STATE, false);
                Debug.Log("Leaving ground at: " + Time.time);
            }
        }

        private void FixedUpdate() {
            
        }
    }
}
