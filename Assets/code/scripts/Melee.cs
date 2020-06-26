using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using UnityEditor;
using Assets.Code.Scripts;

public class Melee : MonoBehaviour
{

    private float damage = 15f;
    private Health targetHealth;
    private void OnTriggerEnter2D(Collider2D collider) {

        GameObject targetObject = collider.gameObject;
        Color originalColor = targetObject.GetComponent<SpriteRenderer>().color;

        if (targetObject != null) {
            targetHealth = targetObject.GetComponent<Health>();
            if (targetHealth != null) {
                targetObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                targetHealth.DealDamage(damage);

                targetObject.GetComponent<PlayerController>().anim.SetBool(Constants.IS_TAKING_DAMAGE, true);
                if (targetHealth.GetHealth() <= 0) {
                    DestroyTarget(targetObject);
                }
            }
        }
    }

    private void DestroyTarget(GameObject target) {
        /* Trigger Death Animation */

        Destroy(target);
    }
}
