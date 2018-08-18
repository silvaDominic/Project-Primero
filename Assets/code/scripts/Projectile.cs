using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class Projectile : MonoBehaviour {

        public float damage;
        public float defaultSpeed;
        private Rigidbody2D rb2d;
        private Transform transform;
        private GameObject target;
        private Material material;

        // Use this for initialization
        void Start() {
            transform = GetComponent<Transform>();
            rb2d = GetComponent<Rigidbody2D>();
            material = GetComponent<Material>();
        }

        // Update is called once per frame
        void Update() {
            rb2d.velocity = new Vector2(defaultSpeed, transform.position.y);
        }

        private void OnCollisionEnter2D(Collision2D target) {
            if (target.gameObject.GetComponent<PlayerController>()) {
                Debug.Log("HIT: " + target.gameObject.name);
            }
        }
    }
}
