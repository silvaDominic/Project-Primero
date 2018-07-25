using System;

namespace Assets.Code.Scripts {

    /// <summary>
    /// A combo that consists of two inputs (e.g. A-button + B-button) and a timer
    /// *** Can be extended ***
    /// </summary>
    public class Combo {

        private string input1;
        private string input2;
        private float comboTimer = 1.0f;

        public Combo(string input1, string input2, float comboTimer) {
            this.input1 = input1;
            this.input2 = input2;
            this.comboTimer = comboTimer;
        }

        public string GetInput1() {
            return this.input1;
        }

        public string GetInput2() {
            return this.input2;
        }

        public string[] GetAllInputs() {
            return new string[] {this.input1, this.input2};
        }

        public float ComboTimer {
            get {
                return comboTimer;
            }

            set {
                comboTimer = value;
            }
        }
    }
}
