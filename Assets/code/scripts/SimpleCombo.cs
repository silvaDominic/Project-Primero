using System;

namespace Assets.Code.Scripts {

    /// <summary>
    /// A combo that consists of two inputs (e.g. A-button + B-button) and a timer
    /// *** Can be extended ***
    /// </summary>
    public class SimpleCombo {

        private float comboTimer = 1.0f;
        private Tuple<InputType, string> input1;
        private Tuple<InputType, string> input2;

        public SimpleCombo(Tuple<InputType, string> input1, Tuple<InputType, string> input2, float comboTimer) {
            this.input1 = input1;
            this.input2 = input2;
            this.comboTimer = comboTimer;
        }

        public Tuple<InputType, string> GetInput1() {
            return input1;
        }

        public Tuple<InputType, string> GetInput2() {
            return input2;
        }

        public InputType GetInput1Type() {
            return input1.Item1;
        }

        public InputType GetInput2Type() {
            return input2.Item1;
        }

        public string GetInput1Name() {
            return input1.Item2;
        }

        public string GetInput2Name() {
            return input2.Item2;
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
