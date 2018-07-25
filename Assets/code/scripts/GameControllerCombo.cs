using UnityEngine;

namespace Assets.Code.Scripts {

    public class GameControllerCombo {
        public string[] inputs;
        private int currentIndex = 0;

        private float allowedTimeBetweenButtons = 0.75f;
        private float timeLastButtonPressed;

        public GameControllerCombo(Combo combo) {
            this.inputs = combo.GetAllInputs();
            this.allowedTimeBetweenButtons = combo.ComboTimer;
            this.timeLastButtonPressed = 0;
        }

        public GameControllerCombo(string[] inputs) {
            this.inputs = inputs;
        }

        /// <summary>
        /// Called from an Update function and checks for a correct series of button pressed over a predefined time
        /// </summary>
        /// <returns>A boolean indicating whether the combo was successfully executed</returns>
        public bool Check() {
            if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) {
                Debug.Log("Resetting Index");
                currentIndex = 0;
            }

            if (currentIndex < inputs.Length) {
                Debug.Log("Looking for: " + inputs[currentIndex]);
                Debug.Log("Index: " + currentIndex);
                // Check if input is any of the valid [left] axis direction (this accounts for uses of entire axis as well)
                if ((inputs[currentIndex] == Constants.DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == 1) ||
                    (inputs[currentIndex] == Constants.UP && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) == -1) ||
                    (inputs[currentIndex] == Constants.LEFT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == -1) ||
                    (inputs[currentIndex] == Constants.RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) == 1) ||
                    (inputs[currentIndex] == Constants.LEFT_AND_RIGHT && Input.GetAxisRaw(Constants.LEFT_JOY_HORIZONTAL) != 0) ||
                    (inputs[currentIndex] == Constants.UP_AND_DOWN && Input.GetAxisRaw(Constants.LEFT_JOY_VERTICAL) != 0) ||
                    // also check if it is NOT a valid [left] axis direction, but instead a valid button press
                    (inputs[currentIndex] != Constants.DOWN) && 
                    (inputs[currentIndex] != Constants.UP) && 
                    (inputs[currentIndex] != Constants.LEFT) && 
                    (inputs[currentIndex] != Constants.RIGHT) &&
                    (inputs[currentIndex] != Constants.LEFT_AND_RIGHT) &&
                    (inputs[currentIndex] != Constants.UP_AND_DOWN) &&
                    Input.GetButtonDown(inputs[currentIndex])) {

                    Debug.Log("Found input: " + inputs[currentIndex]);
                        // If any of the inputs are valid, preserve the time and increment the input index to search for the next valid input
                        timeLastButtonPressed = Time.time;
                        currentIndex++;
                }

                // If the end of the array is reached (before the timers has run out) it means all the inputs have checked out
                // and a successful combo sequence has been executed, return true.
                if (currentIndex >= inputs.Length) {
                    Debug.Log("Combo Success! Resetting index and returning TRUE");
                    currentIndex = 0;
                    return true;
                // Otherwise, wrong sequence, return false.
                } else return false;
            }
            // Otherwise, timer has run out, return false.
            return false;
        }
    }
}