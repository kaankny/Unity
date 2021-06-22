using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;

    [SerializeField] private Animator anim;

    private int speed = 10; // rotation speed
    private void Update()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        anim.SetFloat("Blend", Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
    }

    private void FixedUpdate()
    {
        var MoveH = joystick.Horizontal;
        var MoveV = joystick.Vertical;
        Vector3 dir = new Vector3(MoveH, 0, MoveV);
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        
    }
}
