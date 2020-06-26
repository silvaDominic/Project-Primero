using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States.FightingStates;
using Assets.Code.States.MovementStates;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;
using System;

namespace Assets.Code.Scripts {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StateMachine))]
    public class PlayerController : MonoBehaviour {

        // Movement variables
        public float movementForce = 15f;
        public float maxMovementSpeed = 2f;
        public float jumpVelocity = 8f;
        public float doubleJumpVelocity = 6f;
        public Boolean isDummy = false;
        [Tooltip("The amount the movement force will be divded by when moving from left to right in the air")]
        public float airBornMovementDetraction = 0;

        private bool leftAxisInUse = false;
        private bool rightAxisInUse = false;
        private int lockAxis = 0;
        private int direction;
        private bool isFacingRight = true;
        private float LeftJoyH;

        public Dictionary<string, JoyCombo> gameControllerCombos;
        public Collider2D[] attackColliders;
        public CapsuleCollider2D crystalCollider;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public Transform trans;
        [HideInInspector] public StateMachine movementStateMachine;
        [HideInInspector] public StateMachine fightingStateMachine;
        [HideInInspector] public GunController gun;

        void Awake() {
            // Initialize core player objects
            trans = GetComponent<Transform>();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            gun = GetComponentInChildren<GunController>();
            foreach (StateMachine stmchn in this.GetComponents<StateMachine>()) {
                if (stmchn.ID == Constants.MOVEMENT_STATE_MACHINE) {
                    movementStateMachine = stmchn;
                }
                if (stmchn.ID == Constants.FIGHTING_STATE_MACHINE) {
                    fightingStateMachine = stmchn;
                }
            }
        }

        // Use this for initialization
        private void Start() {
            direction = 1;
            // Check for controller
            if (!isDummy) {
                gameControllerCombos = new Dictionary<string, JoyCombo>();
                gameControllerCombos.Add(Constants.DASH, new JoyCombo(ComboDirectory.dash));
                gameControllerCombos.Add(Constants.SLAM, new JoyCombo(ComboDirectory.slam));
                gameControllerCombos.Add(Constants.PEBBLE_SHOT, new JoyCombo(ComboDirectory.pebbleShot));
                gameControllerCombos.Add(Constants.CRYSTAL_SHARD, new JoyCombo(ComboDirectory.crystalShard));

                foreach (string controller in Input.GetJoystickNames()) {
                    Debug.Log(controller);
                }
            }
            // Set default state to Idle
            this.movementStateMachine.ChangeState(new IdleState(this));
            this.fightingStateMachine.ChangeState(new IdleFightingState(this));
        }

        // Update is called once per frame
        void Update() {
            if (!isDummy) {
                LeftJoyH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);

                if (LeftJoyH > 0 && !isFacingRight) {
                    //this.direction = 1;
                    Flip();
                } else if (LeftJoyH < 0 && isFacingRight) {
                    //this.direction = -1;
                    Flip();
                }
            }
            // Prevent rotation of player due to physics and execute regular update logic for state machine
            transform.rotation = Quaternion.Euler(new Vector3(lockAxis, lockAxis, lockAxis));
            this.fightingStateMachine.ExecuteStateUpdate();
            this.movementStateMachine.ExecuteStateUpdate();
        }

        // Used for physics updates
        void FixedUpdate() {
            // Execute physics related logic for state machine
            this.fightingStateMachine.ExecuteStateFixedUpdate();
            this.movementStateMachine.ExecuteStateFixedUpdate();
        }

        private void LateUpdate() {
            //this.fightingStateMachine.ExecuteStateLateUpdate();
            this.movementStateMachine.ExecuteStateLateUpdate();
        }

        public string getCurrentMovementState() {
            return movementStateMachine.getCurrentState();
        }

        public string getCurrentFightingState() {
            return fightingStateMachine.getCurrentState();
        }

        public bool CheckIfLeftAxisInUse() {
            return this.leftAxisInUse;
        }

        public bool CheckIfRightAxisInUse() {
            return this.rightAxisInUse;
        }

        public void SetLeftAxisInUse(bool axisState) {
            this.leftAxisInUse = axisState;
        }

        public void SetRightAxisInUse(bool axisState) {
            this.rightAxisInUse = axisState;
        }

        public void SetDirection(int value) {
            this.direction = value;
        }

        public int GetDirection() {
            return this.direction;
        }

        private void Flip() {
            isFacingRight = !isFacingRight;

            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            direction *= -1;
            trans.localScale = theScale;
        }

        public void PlayerDeath() {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
}