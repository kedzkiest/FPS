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

        stateMachine.AddTransition<IdleState, CrouchIdleState>(StateEvent.Crouch);
        stateMachine.AddTransition<IdleState, WalkState>(StateEvent.Walk);
        stateMachine.AddTransition<IdleState, RunState>(StateEvent.Run_Start);

        stateMachine.AddTransition<CrouchIdleState, IdleState>(StateEvent.Standup);
        stateMachine.AddTransition<CrouchIdleState, CrouchWalkState>(StateEvent.Walk);

        stateMachine.AddTransition<CrouchWalkState, CrouchIdleState>(StateEvent.Stop);
        stateMachine.AddTransition<CrouchWalkState, WalkState>(StateEvent.Standup);
        stateMachine.AddTransition<CrouchWalkState, RunState>(StateEvent.Run_Start);

        stateMachine.AddTransition<WalkState, IdleState>(StateEvent.Stop);
        stateMachine.AddTransition<WalkState, CrouchWalkState>(StateEvent.Crouch);
        stateMachine.AddTransition<WalkState, RunState>(StateEvent.Run_Start);

        stateMachine.AddTransition<RunState, IdleState>(StateEvent.Stop);
        stateMachine.AddTransition<RunState, CrouchWalkState>(StateEvent.Crouch);
        stateMachine.AddTransition<RunState, WalkState>(StateEvent.Run_Stop);

        stateMachine.SetStartState<IdleState>();
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

    class IdleState : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Player.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (Player.IsWalking)
            {
                if (Player.IsRunning)
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

    class CrouchIdleState : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Player.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Standup);
            }

            if (Player.IsWalking)
            {
                StateMachine.SendEvent(StateEvent.Walk);
            }
        }
    }

    class CrouchWalkState : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        bool allowRun;

        protected override void Enter()
        {
            allowRun = false;
        }

        protected override void Update()
        {
            if (Player.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (Player.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Standup);
            }

            if (Player.IsRunning)
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

    class WalkState : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Player.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (Player.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (Player.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }
        }
    }

    class RunState : ImtStateMachine<CharacterLowerBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Player.DoStop)
            {
                StateMachine.SendEvent(StateEvent.Stop);
            }

            if (Player.DoSwitchCrouchStandup)
            {
                StateMachine.SendEvent(StateEvent.Crouch);
            }

            if (!Player.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Stop);
            }
        }
    }
}