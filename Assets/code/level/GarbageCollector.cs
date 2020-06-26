using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;

public class GarbageCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.GetComponentInParent<PlayerController>()) {
            Destroy(collider.gameObject);
        }
    }
}
