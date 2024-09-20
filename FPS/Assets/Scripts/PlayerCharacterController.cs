using System;
using TMPro;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
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

    new Rigidbody rigidbody;

    public static bool switchCrouchStandup;
    public static bool doMove;
    public static bool isRunHold;
    public static bool isStop;

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
        upperBodyText.text = "";
        lowerBodyText.text = lowerBody.GetCurrentStateName();
    }

    void UpdateState()
    {
        KeyCode crouchStandupSwitchKey = KeyCode.V;
        KeyCode runKey = KeyCode.LeftShift;
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        float moveThreshold = 0.1f;


        switchCrouchStandup = Input.GetKeyDown(crouchStandupSwitchKey);

        doMove = Mathf.Abs(horizontalMove) >= moveThreshold || Mathf.Abs(verticalMove) >= moveThreshold;

        isRunHold = Input.GetKey(runKey);

        isStop = Mathf.Abs(horizontalMove) <= moveThreshold && Mathf.Abs(verticalMove) <= moveThreshold;
    }

    void Walk()
    {

    }

    void Run()
    {

    }

    void ADS()
    {

    }

    void Shoot()
    {

    }

    void Reload()
    {

    }

    void Plant()
    {

    }
}