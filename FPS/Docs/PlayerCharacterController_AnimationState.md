## Lower Body Half
```mermaid
stateDiagram-v2
    [*] --> Idle

    Idle --> Crouch_Idle: Crouch
    Idle --> Walk: Walk
    Idle --> Run: Run

    Crouch_Idle --> Idle: Standup
    Crouch_Idle --> Crouch_Walk: Walk
    
    Crouch_Walk --> Crouch_Idle: Stop
    Crouch_Walk --> Walk: Standup
    Crouch_Walk --> Run: Run

    Walk --> Idle: Stop
    Walk --> Crouch_Walk: Crouch
    Walk --> Run: Run

    Run --> Idle: Stop
    Run --> Crouch_Walk: Crouch
    Run --> Walk: Walk
```

## Upper Body Half
```mermaid
stateDiagram-v2
    [*] --> Idle

    Idle --> ADS: Aim_Hold_On
    Idle --> Reload: Reload
    Idle --> Plant: Plant_Hold_On

    ADS --> Idle: Aim_Hold_Off
    ADS --> Reload: Reload
    ADS --> Plant: Plant_Hold_On

    Reload --> ADS: Aim_Hold_On
    Reload --> Idle: Time
    Reload --> Plant: Plant_Hold_On

    Plant --> Idle: Plant_Hold_Off, Time
```