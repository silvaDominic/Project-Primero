using System;

namespace Assets.Code.Scripts {

    /// <summary>
    /// Provides a more robust input object that provides type and name.
    /// *** Can be extended ***
    /// </summary>
    public static class BasicInput {

        public static Tuple<InputType, string> A_Button = new Tuple<InputType, string>(InputType.Button, Constants.A_BUTTON);
        public static Tuple<InputType, string> B_Button = new Tuple<InputType, string>(InputType.Button, Constants.B_BUTTON);
        public static Tuple<InputType, string> X_Button = new Tuple<InputType, string>(InputType.Button, Constants.X_BUTTON);
        public static Tuple<InputType, string> Y_Button = new Tuple<InputType, string>(InputType.Button, Constants.Y_BUTTON);

        public static Tuple<InputType, string> LeftAxisHorz = new Tuple<InputType, string>(InputType.Axis, Constants.LEFT_JOY_HORIZONTAL);
        public static Tuple<InputType, string> RightAxisHorz = new Tuple<InputType, string>(InputType.Axis, Constants.RIGHT_JOY_HORIZONTAL);

        public static Tuple<InputType, string> LeftAxisVert = new Tuple<InputType, string>(InputType.Axis, Constants.LEFT_JOY_VERTICAL);
        public static Tuple<InputType, string> RightAxisVert = new Tuple<InputType, string>(InputType.Axis, Constants.RIGHT_JOY_VERTICAL);
    }
}
