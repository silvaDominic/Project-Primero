using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States;

namespace Assets.Code.Scripts {

    public class Player : MonoBehaviour {

        public float movementForce = 15f;
        public float maxMovementSpeed = 2f;
        public float jumpForce = 100f;
        public float doubleJumpForce = 75f;
        [Tooltip("The amount the movement force will be divded by when moving from left to right in the air")]
        public float airBornMovementDetraction = 0;
        public bool grounded = false;
        public bool isJumping = false;
        
        private int lockAxis = 0;
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody2D rb2d;
        [HideInInspector]
        public StateMachine movementStateMachine;

        void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movementStateMachine = new StateMachine();
        }

        // Use this for initialization
        private void Start() {
            // Check for controller
            foreach (string controller in Input.GetJoystickNames()) {
                Debug.Log(controller);
            }
            this.movementStateMachine.ChangeState(new IdleState(this));
        }

        // Update is called once per frame
         void Update() {
            // Prevent rotation of player due to physics
            transform.rotation = Quaternion.Euler(new Vector3(lockAxis, lockAxis, lockAxis));

            if (anim.GetFloat("Speed") > 0.01) {
                this.movementStateMachine.ChangeState(new RunningState(this));
            } else if (anim.GetFloat("Speed") < 0.01) {
                this.movementStateMachine.ChangeState(new IdleState(this));
            }
         }

         void FixedUpdate() {
            this.movementStateMachine.ExecuteStateUpdate();
         }

        public bool CheckIfGrounded() {
            return this.grounded;
        }
    }
}

/*TODO 
 * 
 * Create better link between state machine and animator
 * Find out why animator is not responding to conditions
 * 
 */
