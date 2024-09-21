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

        stateMachine.AddTransition<Idle, Run>(StateEvent.Run_Start);
        stateMachine.AddTransition<Idle, ADS>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<Idle, Reload>(StateEvent.Reload);
        stateMachine.AddTransition<Idle, Plant>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<Run, Idle>(StateEvent.Run_Stop);
        stateMachine.AddTransition<Run, ADS>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<Run, Plant>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<ADS, Idle>(StateEvent.ADS_Hold_Off);
        stateMachine.AddTransition<ADS, Run>(StateEvent.Run_Start);
        stateMachine.AddTransition<ADS, Reload>(StateEvent.Reload);
        stateMachine.AddTransition<ADS, Plant>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<Reload, Idle>(StateEvent.Time_Elapsed);
        stateMachine.AddTransition<Reload, Run>(StateEvent.Run_Start);
        stateMachine.AddTransition<Reload, ADS>(StateEvent.ADS_Hold_On);
        stateMachine.AddTransition<Reload, Plant>(StateEvent.Plant_Hold_On);

        stateMachine.AddTransition<Plant, Idle>(StateEvent.Plant_Hold_Off);
        stateMachine.AddTransition<Plant, Idle>(StateEvent.Time_Elapsed);

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

    public void SetState(StateEvent _stateEvent)
    {
        stateMachine.SendEvent(_stateEvent);
    }

    class Idle : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        protected override void Update()
        {
            if (PlayerCharacterController.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }

            if (PlayerCharacterController.IsADSing)
            {
                StateMachine.SendEvent(StateEvent.ADS_Hold_On);
            }

            if (PlayerCharacterController.DoReload)
            {
                StateMachine.SendEvent(StateEvent.Reload);
            }

            if (PlayerCharacterController.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class Run : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        bool allowADS;

        protected override void Enter()
        {
            allowADS = false;
        }

        protected override void Update()
        {
            if (!PlayerCharacterController.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Stop);
            }

            if (PlayerCharacterController.IsADSing)
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

            if (PlayerCharacterController.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class ADS : ImtStateMachine<CharacterUpperBody, StateEvent>.State
    {
        bool allowRun;

        protected override void Enter()
        {
            allowRun = false;
        }

        protected override void Update()
        {
            if (!PlayerCharacterController.IsADSing)
            {
                StateMachine.SendEvent(StateEvent.ADS_Hold_Off);
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

            if (PlayerCharacterController.DoReload)
            {
                StateMachine.SendEvent(StateEvent.Reload);
            }

            if (PlayerCharacterController.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class Reload : ImtStateMachine<CharacterUpperBody, StateEvent>.State
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

            if (PlayerCharacterController.IsRunning)
            {
                StateMachine.SendEvent(StateEvent.Run_Start);
            }

            if (PlayerCharacterController.IsADSing)
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

            if (PlayerCharacterController.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_On);
            }
        }
    }

    class Plant : ImtStateMachine<CharacterUpperBody, StateEvent>.State
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

            if (!PlayerCharacterController.IsPlanting)
            {
                StateMachine.SendEvent(StateEvent.Plant_Hold_Off);
            }
        }
    }
}