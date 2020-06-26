using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;

namespace Assets.Code.States.FightingStates {

    public class CrystalShardState : IState {

        private PlayerController player;
        private float startShardTimer = 0.3f;
        private float shardTimer;
        private bool timerSwitch;
        CapsuleCollider2D thisCollider;

        public CrystalShardState(PlayerController player) {
            this.player = player;
            this.thisCollider = player.crystalCollider as CapsuleCollider2D;
        }

        public void EnterState() {
            player.anim.SetBool(Constants.IS_POWER_ATTACK_01, true);
            shardTimer = startShardTimer;
            timerSwitch = false;
            thisCollider.transform.parent = thisCollider.transform.parent;
        }

        public void ExecuteState() {

        }

        public void ExecuteState_Fixed() {
            float RightJoyH = Input.GetAxisRaw(Constants.RIGHT_JOY_HORIZONTAL);
            float RightJoyV = Input.GetAxis(Constants.RIGHT_JOY_VERTICAL);

            if (!player.CheckIfRightAxisInUse()) {
                player.SetRightAxisInUse(true);
                timerSwitch = true;
                if (RightJoyV > 0) {
                    Debug.Log("Shard Up");
                    thisCollider.transform.position = thisCollider.transform.parent.position + new Vector3(0, 0.75f, 0);
                    thisCollider.transform.eulerAngles = new Vector3(0, 0, 0);
                } else if (RightJoyH > 0) {
                    Debug.Log("Shard Right");
                    thisCollider.transform.position = thisCollider.transform.parent.position + new Vector3(0.75f, 0, 0);
                } else if (RightJoyH < 0) {
                    Debug.Log("Shard Left");
                    thisCollider.transform.position = thisCollider.transform.parent.position + new Vector3(-0.75f, 0, 0);
                } 
            }

            if (timerSwitch) {
                Timer();
            }

            if (RightJoyH == 0 || RightJoyV == 0) {
                player.fightingStateMachine.SwitchToPreviousState();
            }

            // Set up 
            Collider incomingCol = Physics.OverlapCapsule(thisCollider.bounds.center, thisCollider.bounds.extents, LayerMask.GetMask("AttackHitBox"))[0];
            Debug.Log(incomingCol);
        }

        public void ExecuteState_Late() {
            
        }

        public void ExitState() {
            ResetShard();
            player.anim.SetBool(Constants.IS_POWER_ATTACK_01, false);
            player.SetRightAxisInUse(false);
        }

        private void Timer() {
            Debug.Log(shardTimer);
            if (shardTimer <= 0) {
                ResetShard();
                shardTimer = startShardTimer;
                timerSwitch = false;
            } else {
                shardTimer -= Time.deltaTime;
            }
        }

        private void ResetShard() {
            thisCollider.transform.position = thisCollider.transform.parent.position;
            thisCollider.transform.eulerAngles = new Vector3(0, 0, 90);
        }
    }
}