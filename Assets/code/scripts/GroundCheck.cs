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
            player.grounded = true;
        }

        private void OnTriggerExit(Collider col) {
            Debug.Log("Leaving Ground");
            player.grounded = false;
        }
    }
}
