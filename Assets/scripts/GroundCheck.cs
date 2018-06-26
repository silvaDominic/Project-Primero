using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    PlayerController player;

    private void Start() {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Ground") {
            player.grounded = true;
        } else {
            Debug.Log("GroundCheck collided with " + col.name);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.tag == "Ground") {
            player.grounded = false;
        } else {
            Debug.Log("GroundCheck leaving from " + col.name);
        }
    }
}
