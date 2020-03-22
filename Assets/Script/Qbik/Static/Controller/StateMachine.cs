using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IDLE,
    MOVE,
    JUMP,
    ATTACK,
    COMBO_1,
    COMBO_2
}

namespace Asset.Scripts.Qbik.Static.Anim
{
    public static class StateMachine
    {
        public static State _state = State.IDLE;

        public static void SetState(State state, Animator anim) 
        {
             anim.SetBool(_state.ToString(), false);
             anim.SetBool(state.ToString(), true);
             _state = state;
        }
    }
}
