using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    [RequireComponent(typeof(Player))]
    public class GroundCheck : MonoBehaviour {

        Player player;

        private void Start() {
            player = GetComponentInParent<Player>();
        }

        /// <summary>
        /// Checks for an entry collision with the ground
        /// </summary>
        private void OnTriggerEnter2D(Collider2D col) {
            Debug.Log("Colliding with ground");
            // Set parameter in animator to true if colliding with ground
            player.anim.SetBool(Constants.IS_GROUNDED_STATE, true);
        }

        /// <summary>
        /// Checks for an exit collision with the ground
        /// </summary>
        private void OnTriggerExit2D(Collider2D col) {
            Debug.Log("Leaving ground");
            // Set parameter in animator to true if leaving ground
            player.anim.SetBool(Constants.IS_GROUNDED_STATE, false);
        }
    }
}
