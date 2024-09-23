using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool isAlive;
    public bool IsAlive
    {
        get { return isAlive; }
        private set { isAlive = value; }
    }

    [SerializeField] bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    [SerializeField] bool hasDefuser;
    public bool HasDefuser
    {
        get { return hasDefuser; }
        private set { hasDefuser = value; }
    }

    CharacterUpperBody upperBody;
    CharacterLowerBody lowerBody;

    public float Speed { get; private set; }

    [SerializeField] float walkSpeed;
    public float WalkSpeed
    {
        get { return walkSpeed; }
        private set { walkSpeed = value; }
    }

    [SerializeField] float crouchWalkSpeed;
    public float CrouchWalkSpeed
    {
        get { return crouchWalkSpeed; }
        private set { crouchWalkSpeed = value; }
    }

    [SerializeField] float runSpeed;
    public float RunSpeed
    {
        get { return runSpeed; }
        private set { runSpeed = value; }
    }

    [SerializeField] new Rigidbody rigidbody;

    public static bool DoSwitchCrouchStandup { get; private set; }
    public static bool IsWalking { get; private set; }
    public static bool IsRunning { get; private set; }
    public static bool DoStop { get; private set; }
    public static bool IsADSing { get; private set; }
    public static bool DoReload { get; private set; }
    public static bool IsPlanting { get; private set; }

    void Awake()
    {
        upperBody = new CharacterUpperBody();
        upperBody.Init();

        lowerBody = new CharacterLowerBody();
        lowerBody.Init();
    }

    void Update()
    {
        UpdateState();

        upperBody.UpdateState();
        lowerBody.UpdateState();

        DebugPrint(upperBody, lowerBody);
    }

    [SerializeField] TextMeshPro upperBodyText;
    [SerializeField] TextMeshPro lowerBodyText;
    void DebugPrint(CharacterUpperBody upperBody, CharacterLowerBody lowerBody)
    {
        upperBodyText.text = upperBody.GetCurrentStateName();
        lowerBodyText.text = lowerBody.GetCurrentStateName();
    }

    void UpdateState()
    {
        KeyCode crouchStandupSwitchKey = KeyCode.V;
        KeyCode runKey = KeyCode.LeftShift;
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        int rightMouseButton = 1;
        KeyCode reloadKey = KeyCode.R;
        KeyCode plantKey = KeyCode.F;

        float walkThreshold = 0.1f;


        DoSwitchCrouchStandup = Input.GetKeyDown(crouchStandupSwitchKey);

        IsWalking = Mathf.Abs(horizontalMove) >= walkThreshold || Mathf.Abs(verticalMove) >= walkThreshold;

        IsRunning = IsWalking && Input.GetKey(runKey);

        DoStop = Mathf.Abs(horizontalMove) <= walkThreshold && Mathf.Abs(verticalMove) <= walkThreshold;

        IsADSing = Input.GetMouseButton(rightMouseButton);

        DoReload = Input.GetKeyDown(reloadKey);

        IsPlanting = Input.GetKey(plantKey);
    }

    public void Idle()
    {
        Debug.Log("Idle");
    }

    public void Crouch()
    {
        Debug.Log("Crouch");
    }

    public void Walk()
    {
        Debug.Log("Walk");
    }

    public void Run()
    {
        Debug.Log("Run");
    }

    public void ADS()
    {
        Debug.Log("ADS");
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
    }

    public void Reload()
    {
        Debug.Log("Reload");
    }

    public void Plant()
    {
        Debug.Log("Plant");
    }
}