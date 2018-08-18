using UnityEngine;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;

namespace Assets.Code.Scripts {

    /// <summary>
    /// Used to evaluate player inputted combos.
    /// Handles the evaluation of controller input for any given Combo object.
    /// </summary>
    public class JoyCombo {
        public Tuple<string, string>[] inputs;
        private int currentIndex = 0;

        private float allowedTimeBetweenButtons = 0.75f;
        private float timeLastButtonPressed;

        public JoyCombo(Combo combo) {
            this.inputs = combo.GetInputs();
            this.allowedTimeBetweenButtons = combo.ComboTimer;
            this.timeLastButtonPressed = 0;
        }

        /// <summary>
        /// Called from an Update function and checks for a correct series of button pressed over a predefined time
        /// </summary>
        /// <returns>A boolean indicating whether the combo was successfully executed</returns>
        public bool Check() {
            if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) {
                currentIndex = 0;
            }

            // Check if combos are still in the array to be evaluated
            if (currentIndex < inputs.Length) {

                // Check if input at current index is a button, left joy, or right joy input and process accordingly
                if (inputs[currentIndex].Item1 == Constants.BUTTON) {
                    if (Input.GetButtonDown(inputs[currentIndex].Item2)) {
                        RecordAndIncrement();
                    }

                } else if (inputs[currentIndex].Item1 == Constants.LEFTJOY) {
                    if ((inputs[currentIndex].Item2 == Constants.DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == 1) ||
                        (inputs[currentIndex].Item2 == Constants.UP && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == -1) ||
                        (inputs[currentIndex].Item2 == Constants.LEFT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == -1) ||
                        (inputs[currentIndex].Item2 == Constants.RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == 1) ||
                        (inputs[currentIndex].Item2 == Constants.LEFT_AND_RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) != 0) ||
                        (inputs[currentIndex].Item2 == Constants.UP_AND_DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) != 0)) {

                        RecordAndIncrement();
                    }
                } else if (inputs[currentIndex].Item1 == Constants.RIGHTJOY) {
                    if ((inputs[currentIndex].Item2 == Constants.DOWN && Input.GetAxisRaw(Constants.RIGHT_JOY_VERTICAL) == 1) ||
                        (inputs[currentIndex].Item2 == Constants.UP && Input.GetAxisRaw(Constants.RIGHT_JOY_VERTICAL) == -1) ||
                        (inputs[currentIndex].Item2 == Constants.LEFT && Input.GetAxisRaw(Constants.RIGHT_JOY_HORIZONTAL) == -1) ||
                        (inputs[currentIndex].Item2 == Constants.RIGHT && Input.GetAxisRaw(Constants.RIGHT_JOY_HORIZONTAL) == 1) ||
                        (inputs[currentIndex].Item2 == Constants.LEFT_AND_RIGHT && Input.GetAxisRaw(Constants.RIGHT_JOY_HORIZONTAL) != 0) ||
                        (inputs[currentIndex].Item2 == Constants.UP_AND_DOWN && Input.GetAxisRaw(Constants.RIGHT_JOY_VERTICAL) != 0)) {

                        RecordAndIncrement();
                    }
                }

                // If the end of the array is reached (before the timers has run out) it means all the inputs have checked out
                // and a successful combo sequence has been executed, return true.
                if (currentIndex >= inputs.Length) {
                    currentIndex = 0;
                    return true;
                // Otherwise, wrong sequence, return false.
                } else return false;
            }
            // Otherwise, timer has run out, return false.
            return false;
        }

        private void RecordAndIncrement() {
            timeLastButtonPressed = Time.time;
            currentIndex++;
        }

        private bool CheckForButtonPress() {
            if (Input.GetButtonDown(inputs[currentIndex].Item2)) {
                RecordAndIncrement();
                return true;
            } else {
                return false;
            }
        }

        private bool CheckForLeftJoyInput() {
            if ((inputs[currentIndex].Item2 == Constants.DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == 1) ||
                (inputs[currentIndex].Item2 == Constants.UP && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == -1) ||
                (inputs[currentIndex].Item2 == Constants.LEFT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == -1) ||
                (inputs[currentIndex].Item2 == Constants.RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == 1) ||
                (inputs[currentIndex].Item2 == Constants.LEFT_AND_RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) != 0) ||
                (inputs[currentIndex].Item2 == Constants.UP_AND_DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) != 0)) {

                RecordAndIncrement();
                return true;
            } else {
                return false;
            }
        }

        //private bool CheckForRightJoyInput() {

        //}
    }
}