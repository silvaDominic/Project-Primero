using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States.FightingStates;
using Assets.Code.States.MovementStates;

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
        public Dictionary<string, SimpleCombo> simpleCombos;
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody2D rb2d;
        [HideInInspector] public StateMachine movementStateMachine;
        [HideInInspector] public StateMachine fightingStateMachine;
        [HideInInspector] public ComboManager comboManager;

        void Awake() {
            // Initialize core player objects
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            foreach (StateMachine stmchn in this.GetComponents<StateMachine>()) {
                if (stmchn.ID == Constants.MOVEMENT_STATE_MACHINE) {
                    movementStateMachine = stmchn;
                }
                if (stmchn.ID == Constants.FIGHTING_STATE_MACHINE) {
                    fightingStateMachine = stmchn;
                }
            }
            comboManager = GetComponent<ComboManager>();
        }

        // Use this for initialization
        private void Start() {
            // Check for controller
            foreach (string controller in Input.GetJoystickNames()) {
                Debug.Log(controller);
            }

            // Initialize simpleCombos dictionary and add all relavant combos for player
            simpleCombos = new Dictionary<string, SimpleCombo>();
            this.simpleCombos.Add(Constants.DASH, new SimpleCombo(BasicInput.A_Button, BasicInput.LeftAxisHorz, 3.0f));

            // Set default state to Idle
            //this.fightingStateMachine.ChangeState(new IdleState(this));
            this.movementStateMachine.ChangeState(new IdleState(this));
            this.fightingStateMachine.ChangeState(new IdleFightingState(this));
        }

        // Update is called once per frame
         void Update() {
            // Prevent rotation of player due to physics and execute regular update logic for state machine
            transform.rotation = Quaternion.Euler(new Vector3(lockAxis, lockAxis, lockAxis));
            //this.fightingStateMachine.ExecuteStateUpdate();
            this.fightingStateMachine.ExecuteStateUpdate();
            this.movementStateMachine.ExecuteStateUpdate();
         }

        // Used for physics updates
         void FixedUpdate() {
            // Execute physics related logic for state machine
            //this.fightingStateMachine.ExecuteStateFixedUpdate();
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
    }
}
