## Lower Body Half
```mermaid
stateDiagram-v2
    [*] --> Idle

    Idle --> Crouch_Idle
    Idle --> Walk
    Idle --> Run

    Crouch_Idle --> Idle
    Crouch_Idle --> Crouch_Walk
    
    Crouch_Walk --> Crouch_Idle
    Crouch_Walk --> Walk
    Crouch_Walk --> Run

    Walk --> Idle
    Walk --> Crouch_Walk
    Walk --> Run

    Run --> Idle
    Run --> Crouch_Walk
    Run --> Walk
```

## Upper Body Half
```mermaid
stateDiagram-v2
    [*] --> Idle

    Idle --> ADS
    Idle --> Reload
    Idle --> Plant

    ADS --> Idle
    ADS --> Reload
    ADS --> Plant

    Reload --> ADS
    Reload --> Idle
    Reload --> Plant

    Plant --> Idle
```