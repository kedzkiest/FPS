using IceMilkTea.StateMachine;
using UnityEngine;

public class CharacterLowerBody
{
    public enum StateEvent
    {
        Standup,
        Crouch,
        Walk,
        Run_Start,
        Run_Stop,
        Stop,
    }

    ImtStateMachine<CharacterLowerBody, StateEvent> stateMachine;

    Animator animator;

    public void Init()
    {
        stateMachine = new ImtStateMachine<CharacterLowerBody, StateEvent>(this);

        stateMachine.AddTransition<Idle, Crouch_Idle>(StateEvent.Crouch);
        stateMachine.AddTransition<Idle, Walk>(StateEvent.Walk);
        stateMachine.AddTransition<Idle, Run>(StateEvent.Run_Start);

        stateMachine.AddTransition<Crouch_Idle, Idle>(StateEvent.Standup);
        stateMachine.AddTransition<Crouch_Idle, Crouch_Walk>(StateEvent.Walk);

        stateMachine.AddTransition<Crouch_Walk, Crouch_Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Crouch_Walk, Walk>(StateEvent.Standup);
        stateMachine.AddTransition<Crouch_Walk, Run>(StateEvent.Run_Start);

        stateMachine.AddTransition<Walk, Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Walk, Crouch_Walk>(StateEvent.Crouch);
        stateMachine.AddTransition<Walk, Run>(StateEvent.Run_Start);

        stateMachine.AddTransition<Run, Idle>(StateEvent.Stop);
        stateMachine.AddTransition<Run, Crouch_Walk>(StateEvent.Crouch);
        stateMachine.AddTransition<Run, Walk>(StateEvent.Run_Stop);

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

    class Idle : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (PlayerCharacterController.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (PlayerCharacterController.IsWalking)
            {
                if (PlayerCharacterController.IsRunning)
                {
                    StateMachine.SendEvent(StateEvent.Run_Start);
                }
                else
                {
                    StateMachine.SendEvent(StateEvent.Walk);
                }
            }
        }
    }

    class Crouch_Idle : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (PlayerCharacterController.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Standup);
            }

            if (PlayerCharacterController.IsWalking)
            {
                StateMachine.SendEvent(StateEvent.Walk);
            }
        }
    }

    class Crouch_Walk : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        bool allowRun;

        protected override void Enter()
        {
            allowRun = false;
        }

        protected override void Update()
        {
            if (PlayerCharacterController.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (PlayerCharacterController.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Standup);
            }

            if (PlayerCharacterController.IsRunning)
            {
                if (allowRun)
                {
                    StateMachine.SendEvent(StateEvent.Run_Start);
                }
            }
            else
            {
                allowRun = true;
            }
        }
    }

    class Walk : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (PlayerCharacterController.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (PlayerCharacterController.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (PlayerCharacterController.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }
        }
    }

    class Run : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (PlayerCharacterController.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (PlayerCharacterController.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (!PlayerCharacterController.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Stop);
            }
        }
    }
}