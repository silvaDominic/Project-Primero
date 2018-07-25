using System;

namespace Assets.Code.Scripts {

    /// <summary>
    /// Provides a directory of predefined combos that can be referenced
    /// </summary>
    public static class ComboDirectory {

        public static Combo dash = new Combo(Constants.A_BUTTON, Constants.LEFT_AND_RIGHT, 1.0f);
        public static Combo slam = new Combo(Constants.B_BUTTON, Constants.DOWN, 1.0f);

    }
}
