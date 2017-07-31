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
    GameObject levelGenerator;

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

    Camera _camera;

    // Use this for initialization
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            currentState = (int)States.active;
            levelGenerator = GameObject.Find("GameManager");
            if (levelGenerator == null) {
                Debug.Log("Level Generator not found");
            }
        }
        else if (gameObject.tag == "Zombie") {
            currentState = (int)States.deactive;

            zombieGfx.sprite = deactive;
        }

        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        motor = gameObject.GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == (int)States.active && gameObject.tag == "Player")
        {
            bool _activeZombiesPresent = false;
            if (levelGenerator != null)
            {
                _activeZombiesPresent = levelGenerator.GetComponent<ZombieSelect>().isActiveZombiesPresent();
            }
            else {
                Debug.LogError("Level generator is NULL");
            }
            float speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;// * Time.deltaTime;
            if (_activeZombiesPresent)
            {
                speedX = 0.0f;
            }
            motor.MoveBody(speedX);

            if (Input.GetKeyDown(KeyCode.UpArrow) && gameObject.tag == "Player" && !_activeZombiesPresent)
            {
                motor.jump(jumpSpeed);
            }
            /*
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
            */
        }

        if (gameObject.tag == "Player") {
            Vector3 screenPoint = _camera.WorldToViewportPoint(transform.position);
            if (screenPoint.x < 0.0f || screenPoint.x > 1.0f) {
                kill();
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
