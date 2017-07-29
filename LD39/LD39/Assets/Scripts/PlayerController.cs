using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField]
    float moveSpeed, jumpSpeed;

    PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
        motor = gameObject.GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;// * Time.deltaTime;

        motor.MoveBody(speedX);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            motor.jump(jumpSpeed);
        }
    }
}
