using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        upperBody = new CharacterUpperBody();
        upperBody.Init();

        lowerBody = new CharacterLowerBody();
        lowerBody.Init();
    }

    void Update()
    {
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