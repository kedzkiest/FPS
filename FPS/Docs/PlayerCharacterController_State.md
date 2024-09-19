```mermaid
stateDiagram-v2
[*] --> Alive

Alive --> Dead: die

Dead --> Alive: revive
```

```mermaid
stateDiagram-v2
[*] --> Grounded

Grounded --> Midair

Midair --> Grounded
```