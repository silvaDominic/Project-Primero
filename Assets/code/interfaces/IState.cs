namespace Assets.Code.Interfaces {

    public interface IState {

        void EnterState();

        void ExecuteState();

        void ExecuteState_Fixed();

        void ExecuteState_Late();

        void ExitState();
    }
}
