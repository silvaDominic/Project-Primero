using System;
using System.Collections;
using System.Collections.Specialized;

namespace Assets.Code.Scripts {

    /// <summary>
    /// Provides a directory of predefined combos that can be referenced
    /// </summary>
    public static class ComboDirectory {

        public static Combo dash = new Combo(new Tuple<string, string>[] { Tuple.Create(Constants.BUTTON, Constants.A_BUTTON), Tuple.Create(Constants.LEFTJOY, Constants.LEFT_AND_RIGHT) }, 0.5f);
        public static Combo slam = new Combo(new Tuple<string, string>[] { Tuple.Create(Constants.BUTTON, Constants.A_BUTTON), Tuple.Create(Constants.LEFTJOY, Constants.DOWN) }, 1.0f);
        public static Combo pebbleShot = new Combo(new Tuple<string, string>[] { Tuple.Create(Constants.BUTTON, Constants.B_BUTTON) }, 0.1f, 2.0f);
        public static Combo crystalShard = new Combo(new Tuple<string, string>[] { Tuple.Create(Constants.RIGHTJOY, Constants.LEFT_AND_RIGHT_AND_UP) }, 0.5f);
    }
}
