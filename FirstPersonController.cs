using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Floats")]
    [Range(5f,25f)]
    public float gravity = 15f;
    [Range(5f,15f)]
    public float movementSpeed = 10f;
    [Range(10f, 25f)]
    public float RunSpeed = 15f;
    [Range(15f, 30f)]
    public float SuperRunSpeed = 20f;
    [Range(5f,15f)]
    public float jumpSpeed = 10f;
    [Range(5f, 25f)]
    public float SuperJumpSpeed = 15;
    [Range(0.5f, 5f)]
    public float mouseSensitivity = 1.5f;

    [Header("Cam Settings")]
    [Range(1f,90f)]
    public float maxPitch = 85f;
    [Range(-1f, -90f)]
    public float minPitch = -85f;


    [Header("SuperPower")]
    public bool SuperJump = false;
    public bool TwoJump = false;
    public bool SpeedUp = false;

    [Header("Power GameObjects Tags")]
    public string superJumpTag = "superJumpPower";
    public string twoJumpTag = "twoJumpPower";
    public string speedUpTag = "speedUpPower";
    CharacterController controller;
    float yVelocity = 0f;
    Transform cameraTransform;
    float pitch = 0f;
    int jumpCount = 2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float xInput = Input.GetAxis("Mouse X") * mouseSensitivity;
        float yInput = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(0, xInput, 0);
        pitch -= yInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        Quaternion rot = Quaternion.Euler(pitch, 0, 0);
        cameraTransform.localRotation = rot;
    }

    void Move()
    {
        float speed;
        float jSpeed;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        if (SuperJump)
            jSpeed = SuperJumpSpeed;
        else
            jSpeed = jumpSpeed;
        if (SpeedUp)
            speed = SuperRunSpeed;
        else if (Input.GetKey(KeyCode.LeftShift))
            speed = RunSpeed;
        else
            speed = movementSpeed;
        Vector3 move = transform.TransformVector(input) * speed;
        if (controller.isGrounded)
        {
            jumpCount = 2;
            yVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jSpeed;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space) && TwoJump && jumpCount > 1)
        {
            jumpCount--;
            yVelocity = jSpeed;
        }
        yVelocity -= gravity * Time.deltaTime;
        move.y = yVelocity;
        controller.Move(move * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains(speedUpTag))
        {
            SpeedUp = true;
            Debug.Log("Speed Up Activated");
        }
        else if (other.gameObject.tag.Contains(twoJumpTag))
        {
            TwoJump = true;
            Debug.Log("Two Jump Activated");
        }
        else if (other.gameObject.tag.Contains(superJumpTag))
        {
            SuperJump = true;
            Debug.Log("Super Jump Activated");
        }
    }

}
