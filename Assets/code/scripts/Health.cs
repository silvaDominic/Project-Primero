using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void DealDamage(float damage) {
        this.health -= damage;
    }

    public float GetHealth() {
        return this.health;
    }

    public void SetHealth(float health) {
        this.health = health;
    }
}
