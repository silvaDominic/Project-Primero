using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States.FightingStates;
using Assets.Code.States.MovementStates;
using Assets.Code.Interfaces;

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
        [Tooltip("The amount the movement force will be divded by when moving from left to right in the air")]
        public float airBornMovementDetraction = 0;

        private bool axisInUse = false;
        private int lockAxis = 0;
        private float previousVelocity = 0;
        private int direction = 0;

        public Dictionary<string, JoyCombo> gameControllerCombos;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public StateMachine movementStateMachine;
        [HideInInspector] public StateMachine fightingStateMachine;
        [HideInInspector] public GunController gun;

        void Awake() {
            // Initialize core player objects
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
            // Check for controller
            foreach (string controller in Input.GetJoystickNames()) {
                Debug.Log(controller);
            }

            gameControllerCombos = new Dictionary<string, JoyCombo>();
            gameControllerCombos.Add(Constants.DASH, new JoyCombo(ComboDirectory.dash));
            gameControllerCombos.Add(Constants.SLAM, new JoyCombo(ComboDirectory.slam));
            gameControllerCombos.Add(Constants.PEBBLE_SHOT, new JoyCombo(ComboDirectory.pebbleShot));
            gameControllerCombos.Add(Constants.CRYSTAL_SHARD, new JoyCombo(ComboDirectory.crystalShard));

            // Set default state to Idle
            //this.fightingStateMachine.ChangeState(new IdleState(this));
            this.movementStateMachine.ChangeState(new IdleState(this));
            this.fightingStateMachine.ChangeState(new IdleFightingState(this));
        }

        // Update is called once per frame
        void Update() {
            if (rb2d.velocity.x > 0) {
                direction = 1;
            } else if (rb2d.velocity.x < 0) {
                direction = -1;
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

        public bool CheckIfAxisInUse() {
            return this.axisInUse;
        }

        public void SetAxisInUse(bool axisState) {
            this.axisInUse = axisState;
        }

        public int Direction {
            get {
                return direction;
            }

            set {
                direction = value;
            }
        }
    }
}

// TODO:
// How will health be handled?
//