using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.States;
using Assets.Code.Scripts;

namespace Assets.Code.Interfaces {

    public interface IState {

        void EnterState();

        void ExecuteState();

        void ExecuteState_Fixed();

        void ExitState();
    }
}
