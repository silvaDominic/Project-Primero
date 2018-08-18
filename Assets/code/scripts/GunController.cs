using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class GunController : MonoBehaviour {

        public Projectile projectile;
        private Transform parent;
        private PlayerController player;

        private void Awake() {
            parent = GetComponentInParent<Transform>();
            player = GetComponentInParent<PlayerController>();
        }

        public void FireProjectile() {
            // Cloned projectile
            Projectile firedProjectile;
            Debug.Log("PLAYER DIR" + player.Direction);

            firedProjectile = Instantiate(projectile, new Vector2(parent.transform.position.x, parent.transform.position.y), Quaternion.identity);
            firedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectile.defaultSpeed * player.Direction, 0);
        }
    }
}