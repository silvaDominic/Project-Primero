using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;

public class Deathzone : MonoBehaviour
{

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponentInChildren<GroundCheck>() != null) {
            Debug.Log("Found ground check script");
            if (collider.gameObject.GetComponentInParent<PlayerController>() != null) {
                Debug.Log("Is valid PLAYER CONTROLLER");
                collider.gameObject.GetComponentInParent<PlayerController>().PlayerDeath();
            }
        } else {
            Destroy(collider);
        }
    }
}
