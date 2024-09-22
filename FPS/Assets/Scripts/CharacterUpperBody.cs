using IceMilkTea.StateMachine;
using UnityEngine;

public class CharacterUpperBody
{
    public enum StateEvent
    {
        Run_Start,
        Run_Stop,
        ADS_Hold_On,
        ADS_Hold_Off,
        Reload,
        Plant_Hold_On,
        Plant_Hold_Off,
        Time_Elapsed
    }

    ImtStateMachine<CharacterUpperBody, StateEvent> stateMachine;

    Animator animator;

    public void Init()
    {
        stateMachine = new ImtStateMachine<CharacterUpperBody, StateEvent>(this);

        stateMachine.AddTransition<IdleState, RunState>(StateEvent.Run_Start);
        stateMachine.AddTransition<IdleState, ADSState>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<IdleState, ReloadState>(StateEvent.Reload);
        stateMachine.AddTransition<IdleState, PlantState>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<RunState, IdleState>(StateEvent.Run_Stop);
        stateMachine.AddTransition<RunState, ADSState>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<RunState, PlantState>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<ADSState, IdleState>(StateEvent.ADS_Hold_Off);
        stateMachine.AddTransition<ADSState, RunState>(StateEvent.Run_Start);
        stateMachine.AddTransition<ADSState, ReloadState>(StateEvent.Reload);
        stateMachine.AddTransition<ADSState, PlantState>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<ReloadState, IdleState>(StateEvent.Time_Elapsed);
        stateMachine.AddTransition<ReloadState, RunState>(StateEvent.Run_Start);
        stateMachine.AddTransition<ReloadState, ADSState>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<ReloadState, PlantState>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<PlantState, IdleState>(StateEvent.Plant_Hold_Off);
        stateMachine.AddTransition<PlantState, IdleState>(StateEvent.Time_Elapsed);

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

    public void SetState(StateEvent _stateEvent)
    {
        stateMachine.SendEvent(_stateEvent);
    }

    class IdleState : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (Player.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }

            if (Player.IsADSing)
            {
                StateMachine.SendEvent(StateEvent.ADS_Hold_On);
            }

            if (Player.DoReload)
            {
                StateMachine.SendEvent(StateEvent.Reload);
            }

            if (Player.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class RunState : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        bool allowADS;

        protected override void Enter()
        {
            allowADS = false;
        }

        protected override void Update()
        {
            if (!Player.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Stop);
            }

            if (Player.IsADSing)
            {
                if (allowADS)
                {
                    StateMachine.SendEvent(StateEvent.ADS_Hold_On);
                }
            }
            else
            {
                allowADS = true;
            }

            if (Player.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class ADSState : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        bool allowRun;

        protected override void Enter()
        {
            allowRun = false;
        }

        protected override void Update()
        {
            if (!Player.IsADSing)
            {
                StateMachine.SendEvent(StateEvent.ADS_Hold_Off);
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

            if (Player.DoReload)
            {
                StateMachine.SendEvent(StateEvent.Reload);
            }

            if (Player.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class ReloadState : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        float elapsedTime;
        float reloadTime = 1.0f;

        bool allowADS;

        protected override void Enter()
        {
            elapsedTime = 0f;

            allowADS = false;
        }

        protected override void Update()
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > reloadTime)
            {
                StateMachine.SendEvent(StateEvent.Time_Elapsed);
            }

            if (Player.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }

            if (Player.IsADSing)
            {
                if (allowADS)
                {
                    StateMachine.SendEvent(StateEvent.ADS_Hold_On);
                }
            }
            else
            {
                allowADS = true;
            }

            if (Player.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class PlantState : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        float elapsedTime;
        float plantTime = 1.0f;

        protected override void Enter()
        {
            elapsedTime = 0f;
        }

        protected override void Update()
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > plantTime)
            {
                StateMachine.SendEvent(StateEvent.Time_Elapsed);
            }

            if (!Player.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_Off);
            }
        }
    }
}