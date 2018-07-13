using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    public class ComboMonitor : MonoBehaviour {

        public float currentComboTimer = 1.0f;
        public int currentComboState = 0;
        
        private string comboButton;
        private bool ActivateTimerToReset = false;
        private float origTimer;
        private float LeftJoyV;
        private float LeftJoyH;

        private void Start() {
            // Store original timer reset duration
            origTimer = currentComboTimer;

            LeftJoyV = Input.GetAxis(Constants.LEFT_JOY_VERTICAL);
            LeftJoyH = Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL);
        }

        void Update() {
            ResetComboState(ActivateTimerToReset);
        }

        public void ResetComboState(bool resetTimer) {
            if (resetTimer)
            //if the bool that you pass to the method is true
            // (aka if ActivateTimerToReset is true, then the timer start
            {
                currentComboTimer -= Time.deltaTime;
                //If the parameter bool is set to true, a timer start, when the timer
                //runs out (because you don't press fast enought Z the second time)
                //currentComboState is set again to zero, and you need to press it twice again
                if (currentComboTimer <= 0) {
                    Reset();
                }
            }
        }

        private void Reset() {
            currentComboState = 0;
            comboButton = "";
            ActivateTimerToReset = false;
            currentComboTimer = origTimer;
        }

        public void StationaryCombo() {

            for (int i = 0; i < 4; i++) {
                if (Input.GetButtonDown("Button-" + i)) {
                    Debug.Log("FIRST CHECK TRUE");
                    currentComboState++;
                    ActivateTimerToReset = true;
                    StartCoroutine(CheckForAxis(i));
                } else if (Input.GetButtonUp("Button-" + i)) {
                    Reset();
                }

            }
        }

        private IEnumerator CheckForAxis(int buttonIndex) {
            while (Mathf.Abs(Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL)) == 0 && currentComboTimer != 0) {
                Debug.Log("Condition not met");
                yield return null;
            }
            Debug.Log("SECOND CHECK TRUE");
            comboButton = "Button-" + buttonIndex;
        }

        public string GetComboButton() {
            return this.comboButton;
        }

        public bool CheckTimerState() {
            return this.ActivateTimerToReset;
        }
    }
}

// TODO: Figure out why timer stops when analog stick is used