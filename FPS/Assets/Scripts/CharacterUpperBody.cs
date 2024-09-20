using IceMilkTea.StateMachine;
using UnityEngine;

public class CharacterUpperBody
{
    enum StateEvent
    {
        Idle,
        ADS,
        Reload,
        Plant
    }

    ImtStateMachine<CharacterUpperBody, StateEvent> stateMachine;

    Animator animator;

    public void Init()
    {

    }

    public void UpdateState()
    {

    }

    void SetState(StateEvent _stateEvent){

    }
}