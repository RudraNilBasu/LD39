using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    enum States
    {
        deactive, active, dead
    };

    [SerializeField]
    GameObject activationText;

    [SerializeField]
    SpriteRenderer zombieGfx;
    [SerializeField]
    Sprite deactive, active;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed, jumpSpeed;

    [SerializeField]
    GameObject explosion;

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

            zombieGfx.sprite = deactive;
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

            if (gameObject.tag == "Player") {
                if (speedX != 0)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().Pause();
                }
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
        if (currentState == (int)States.dead) {
            return;
        }
        currentState = (int)States.dead;
        motor.MoveBody(0.0f);
        if (gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Zombie") {
            zombieGfx.sprite = deactive;
        }
    }

    public void activate()
    {
        currentState = (int)States.active;

        if (gameObject.tag == "Zombie") {
            activationText.SetActive(true);

            zombieGfx.sprite = active;
        }
    }
}
