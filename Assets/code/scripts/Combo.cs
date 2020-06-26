using System;
using System.Collections;
using System.Collections.Specialized;

namespace Assets.Code.Scripts {

    /// <summary>
    /// A combo that consists of two inputs (e.g. A-button + B-button) and a timer
    /// *** Can be extended ***
    /// </summary>
    public class Combo {

        //private string[] inputs;
        private float chargeTimer;
        private float comboTimer = 1.0f;
        private Tuple<string, string>[] inputs;

        public Combo(Tuple<string, string>[] inputs, float comboTimer) {
            this.inputs = inputs;
            this.comboTimer = comboTimer;
        }

        public Combo(Tuple<string, string>[] inputs, float comboTimer, float chargeTimer) {
            this.inputs = inputs;
            this.comboTimer = comboTimer;
            this.chargeTimer = chargeTimer;
        }

        public Tuple<string, string>[] GetInputs() {
            return this.inputs;
        }

        public float ComboTimer {
            get {
                return comboTimer;
            }

            set {
                comboTimer = value;
            }
        }

        public float ChargeTimer {
            get {
                return chargeTimer;
            }

            set {
                chargeTimer = value;
            }
        }
    }
}
