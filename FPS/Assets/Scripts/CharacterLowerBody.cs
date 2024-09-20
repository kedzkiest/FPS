using IceMilkTea.StateMachine;
using UnityEngine;

public class CharacterLowerBody
{
    public enum StateEvent
    {
        Standup,
        Crouch,
        Walk,
        Run,
        Stop,
    }

    ImtStateMachine<CharacterLowerBody, StateEvent> stateMachine;

    Animator animator;

    public void Init()
    {
        stateMachine = new ImtStateMachine<CharacterLowerBody, StateEvent>(this);

        stateMachine.AddTransition<Idle, Crouch_Idle>(StateEvent.Crouch);
        stateMachine.AddTransition<Idle, Walk>(StateEvent.Walk);
        stateMachine.AddTransition<Idle, Run>(StateEvent.Run);

        stateMachine.AddTransition<Crouch_Idle, Idle>(StateEvent.Standup);
        stateMachine.AddTransition<Crouch_Idle, Crouch_Walk>(StateEvent.Walk);

        stateMachine.AddTransition<Crouch_Walk, Crouch_Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Crouch_Walk, Walk>(StateEvent.Standup);
        stateMachine.AddTransition<Crouch_Walk, Run>(StateEvent.Run);

        stateMachine.AddTransition<Walk, Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Walk, Crouch_Walk>(StateEvent.Crouch);
        stateMachine.AddTransition<Walk, Run>(StateEvent.Run);

        stateMachine.AddTransition<Run, Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Run, Crouch_Walk>(StateEvent.Crouch);
        stateMachine.AddTransition<Run, Walk>(StateEvent.Walk);

        stateMachine.SetStartState<Idle>();
    }

    public void UpdateState()
    {
        stateMachine.Update();
    }

    public string GetCurrentStateName()
    {
        return stateMachine.CurrentStateName;
    }

    public void SetState(StateEvent _stateEvent){
        stateMachine.SendEvent(_stateEvent);
    }

    class Idle: ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }
        }
    }

    class Crouch_Idle : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StateMachine.SendEvent(StateEvent.Standup);
            }
        }
    }

    class Crouch_Walk : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Enter()
        {
            Debug.Log("Crouch_Walk enter");
        }
    }

    class Walk : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Enter()
        {
            Debug.Log("Walk enter");
        }
    }

    class Run : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Enter()
        {
            Debug.Log("Run enter");
        }
    }
}