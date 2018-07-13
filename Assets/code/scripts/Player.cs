using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States;

namespace Assets.Code.Scripts {

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StateMachine))]
    public class Player : MonoBehaviour {

        // Movement variables
        public float movementForce = 15f;
        public float maxMovementSpeed = 2f;
        public float jumpVelocity = 8f;
        public float doubleJumpVelocity = 6f;
        [Tooltip("The amount the movement force will be divded by when moving from left to right in the air")]
        public float airBornMovementDetraction = 0;

        private bool axisInUse = false;
        private int lockAxis = 0;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public StateMachine movementStateMachine;
        [HideInInspector] public ComboMonitor comboMonitor;

        void Awake() {
            // Initialize core player objects
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movementStateMachine = GetComponent<StateMachine>();
            comboMonitor = GetComponent<ComboMonitor>();
        }

        // Use this for initialization
        private void Start() {
            // Check for controller
            foreach (string controller in Input.GetJoystickNames()) {
                Debug.Log(controller);
            }

            // Set default state to Idle
            this.movementStateMachine.ChangeState(new IdleState(this));
        }

        // Update is called once per frame
         void Update() {
            // Prevent rotation of player due to physics and execute regular update logic for state machine
            transform.rotation = Quaternion.Euler(new Vector3(lockAxis, lockAxis, lockAxis));
            this.movementStateMachine.ExecuteStateUpdate();
         }

        // Used for physics updates
         void FixedUpdate() {
            // Execute physics related logic for state machine
            this.movementStateMachine.ExecuteStateFixedUpdate();
         }

        private void LateUpdate() {
            this.movementStateMachine.ExecuteStateLateUpdate();
        }

        public bool CheckIfAxisInUse() {
            return this.axisInUse;
        }

        public void SetAxisInUse(bool axisState) {
            this.axisInUse = axisState;
        }
    }
}

/*TODO 
 * 
 * Create 'action' camera
 * 
 */
