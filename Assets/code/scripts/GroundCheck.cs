using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class GroundCheck : MonoBehaviour {

        Player player;

        private void Start() {
            player = GetComponentInParent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Debug.Log("Colliding with ground");
            player.SetGrounded(true);
            Debug.Log("GROUNDED SCRIPT: " + player.CheckIfGrounded());
        }

        private void OnTriggerExit2D(Collider2D col) {
            Debug.Log("Leaving ground");
            player.SetGrounded(false);
            Debug.Log("GROUNDED SCRIPT: " + player.CheckIfGrounded());
        }
    }
}
