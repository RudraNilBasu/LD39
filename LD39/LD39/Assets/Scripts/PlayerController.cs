using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    enum States
    {
        deactive, active, dead
    };

    [Header("Movement")]
    [SerializeField]
    float moveSpeed, jumpSpeed;

    PlayerMotor motor;

    int currentState;

    // Use this for initialization
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            currentState = (int)States.active;
        }
        else if (gameObject.tag == "Zombie") {
            currentState = (int)States.deactive;
        }
        
        motor = gameObject.GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == (int)States.active)
        {
            float speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;// * Time.deltaTime;

            motor.MoveBody(speedX);

            if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.tag == "Player")
            {
                motor.jump(jumpSpeed);
            }
        }
    }

    public bool isDeactivated()
    {
        return currentState == (int)States.deactive;
    }

    public bool isActive()
    {
        return currentState == (int)States.active;
    }

    public void kill()
    {
        currentState = (int)States.dead;
        motor.MoveBody(0.0f);
    }

    public void activate()
    {
        currentState = (int)States.active;
    }
}
