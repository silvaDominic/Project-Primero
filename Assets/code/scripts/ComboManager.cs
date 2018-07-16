using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    /// <summary>
    /// Handles the evaluation of controller based input used for combos.
    /// </summary>
    public class ComboManager : MonoBehaviour {

        [Tooltip("Window that player has to perform combo")]
        public float currentComboTimer = 1.0f;

        private bool ActivateTimerToReset = false;
        private float origTimer;
        private bool isSuccess = false;

        private void Start() {
            origTimer = currentComboTimer;
        }

        /// <summary>
        /// Begins the countdown for the predefined window of opportunity that the player
        /// has to execute the proper combo.
        /// Resets the timer and isSuccess variable after reaching 0.
        /// </summary>
        /// <param name="beginCountdown"></param>
        public void ComboWindow(bool beginCountdown) {
            // If ActivateTimerToReset is true, then the timer starts
            if (beginCountdown) {
                currentComboTimer -= Time.deltaTime;
                Mathf.Floor(currentComboTimer);
                // If timer reaches 0 (combo not executed fast enough),
                // Active timer and success variable are reset
                if (currentComboTimer <= 0) {
                    ActivateTimerToReset = false;
                    isSuccess = false;
                }
            }
        }

        private void Update() {
            // Runs every frame to update timer
            ComboWindow(ActivateTimerToReset);
        }

        /// <summary>
        /// Fully resets all relevant variables
        /// </summary>
        public void Reset() {
            ActivateTimerToReset = false;
            isSuccess = false;
            currentComboTimer = origTimer;
        }

        /// <summary>
        /// Checks if a combo is successfully or unsuccessfully executed.
        /// </summary>
        /// <param name="simpleCombo">The SimpleCombo object that contains the parameters for evaluation</param>
        /// <returns>A bool that represents whether the combo was a success or not</returns>
        public bool CheckForCombo(SimpleCombo simpleCombo) {
            return EvaluatePrimaryInput(simpleCombo);
        }

        /// <summary>
        /// Evaluates the first input in the SimpleCombo object to determine how evaluation should begin.
        /// </summary>
        /// <param name="simpleCombo">The SimpleCombo object that contains the parameters for evaluation</param>
        /// <returns>A bool that represents whether the combo was a success or not</returns>
        public bool EvaluatePrimaryInput(SimpleCombo simpleCombo) {
            bool comboEvaluationResult = false;
            switch (simpleCombo.GetInput1Type()) {
                case InputType.Button:
                    comboEvaluationResult = ButtonStartCombo(simpleCombo);
                    break;
                case InputType.Axis:
                    comboEvaluationResult = AxisStartCombo(simpleCombo);
                    break;
                default:
                    break;
            }
            return comboEvaluationResult;
        }

        /// <summary>
        /// Evaluates the second input in the SimpleCombo object to determine how evaluation should continue.
        /// A coroutine is used in conjunction with a timer (supplied by the SimpleCombo object) to evaluate
        /// the success of the player input
        /// </summary>
        /// <param name="simpleCombo">The SimpleCombo object that contains the parameters for evaluation</param>
        public void EvaluteSecondaryInput(SimpleCombo simpleCombo) {
            switch (simpleCombo.GetInput2Type()) {
                case InputType.Button:
                    StartCoroutine(CheckForSecondButtonInput(simpleCombo.GetInput2Name()));
                    break;
                case InputType.Axis:
                    StartCoroutine(CheckForSecondAxisInput(simpleCombo.GetInput2Name()));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Used for a combo that begins with a button press.
        /// Evaluates the press of the first button and, if successful, begins a timer, and calls for a check of the secondary input
        /// The Coroutine running in the secondard evaluation is halted and a full reset is executed if the button is released
        /// </summary>
        /// <param name="simpleCombo">The SimpleCombo object that contains the parameters for evaluation</param>
        /// <returns>A bool that represents whether the combo was a success or not</returns>
        public bool ButtonStartCombo(SimpleCombo simpleCombo) {
            // Simple check for first button press
            if (Input.GetButtonDown(simpleCombo.GetInput1Name())) {
                Debug.Log("Button 1 down, starting timer");
                ActivateTimerToReset = true;
                // Check for 
                EvaluteSecondaryInput(simpleCombo);
                // Check for button release resulting in termination of combo
            } else if (Input.GetButtonUp(simpleCombo.GetInput1Name())) {
                StopAllCoroutines();
                Debug.Log("Button 1 up, Resetting");
                Reset();
            }
            Debug.Log("RESULT: " + isSuccess);
            return isSuccess;
        }

        /// <summary>
        /// Used for a combo that begins with a axis movement.
        /// Evaluates the movement of an axis and, if successful, begins a timer, and calls for a check of the secondary input
        /// The Coroutine running in the secondard evaluation is halted and a full reset is executed if the axis returns to 0.
        /// </summary>
        /// <param name="simpleCombo"></param>
        /// <returns></returns>
        public bool AxisStartCombo(SimpleCombo simpleCombo) {
            // Simple check for first button press
            if (Input.GetAxis(simpleCombo.GetInput1Name()) > 0) {
                Debug.Log("Axis moved, starting timer");
                ActivateTimerToReset = true;
                EvaluteSecondaryInput(simpleCombo);
                // Check for button release resulting in termination of combo
            } else if (Input.GetAxis(simpleCombo.GetInput1Name()) == 0) {
                StopAllCoroutines();
                Debug.Log("Button 1 up, Resetting");
                Reset();
            }
            Debug.Log("RESULT: " + isSuccess);
            return isSuccess;
        }

        /// <summary>
        /// The Coroutine that is started after an initial input has been evalauted.
        /// A loop begins that waits for yeilds null upon negative input and timer conditions
        /// If a positive condition is met (button pressed or timer reaches 0) the loop exits
        /// and a further check is evaluted.
        /// </summary>
        /// <param name="buttonTwo">A string that represents the desired button; button that must be pressed (within predefined window)
        /// to perform combo</param>
        /// <returns>An IEnumerator necessary for a Coroutine</returns>
        public IEnumerator CheckForSecondButtonInput(string buttonTwo) {
            while(!Input.GetButtonDown(buttonTwo) && currentComboTimer != 0) {
                Debug.Log(currentComboTimer);
                Debug.Log("Combo window still available");
                yield return null;
            }
            // Check if positive condition reached in loop is not the expiration of the combo timer
            if (currentComboTimer > 0) {
                // If the timer is still above 0, then a successful combo has been made and the isSuccess variable is set to true
                isSuccess = true;
                Debug.Log("Button 2 pressed in time");
                // Otherwise, the timer has expired and the coroutine ends
            } else {
                Debug.Log("Not fast enough");
            }
        }

        /// <summary>
        /// The Coroutine that is started after an initial input has been evalauted.
        /// A loop begins that waits for yeilds null upon negative input and timer conditions
        /// If a positive condition is met (button pressed or timer reaches 0) the loop exits
        /// and a further check is evaluted.
        /// </summary>
        /// <param name="axis">A string that represents the desired axis; axis that initiates combo</param>
        /// <returns>An IEnumerator necessary for a Coroutine</returns>
        public IEnumerator CheckForSecondAxisInput(string axis) {
            while (Input.GetAxis(axis) == 0 && currentComboTimer != 0) {
                Debug.Log(currentComboTimer);
                Debug.Log("Combo window still available");
                yield return null;
            }
            // Check if positive condition reached in loop is not the expiration of the combo timer
            if (currentComboTimer > 0) {
                // If the timer is still above 0, then a successful combo has been made and the isSuccess variable is set to true
                isSuccess = true;
                Debug.Log("Axis pressed in time");
                // Otherwise, the timer has expired and the coroutine ends
            } else {
                Debug.Log("Not fast enough");
            }
        }
    }
}