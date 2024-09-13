using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static float moveSpeed = 5f;
    public float rotationSpeed = 500f;

    [SerializeField] private Joystick joystick;

    private CharacterController characterController;
    private Animator anim;

    private float gravity = -10f; 
    private float verticalSpeed = 0f;
    public bool isMovile;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovementInput();
    }
    private void HandleMovementInput()
    {
        float horizontalInput = GetHorizontalInput();
        float verticalInput = GetVerticalInput();

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);

            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (characterController.isGrounded)
        {
            verticalSpeed = 0f; 
        }
        else
        {
            verticalSpeed += gravity * Time.deltaTime; 
        }

        characterController.Move(new Vector3(0, verticalSpeed, 0) * Time.deltaTime);
    }
    private float GetHorizontalInput()
    {
        if (isMovile)
        {
            return joystick.Horizontal;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    float GetVerticalInput()
    {
        if (isMovile)
        {
            return joystick.Vertical;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
