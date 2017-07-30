using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour {

    [SerializeField]
    float timeLeft;

    Rigidbody2D rb;

    PlayerMotor motor;
    PlayerController controller;

    [SerializeField]
    float[] timeForCurrentLevel;

    struct energyRate
    {
        int horizontalEffect;
        int verticalEffect;
        float rate;

        public void setVerticalRate(int _rate) {
            verticalEffect = _rate;
        }

        public void setHorrizontalRate(int _rate) {
            horizontalEffect = _rate;
        }

        public float getRate() {
            return (horizontalEffect + verticalEffect) * 1.0f;
        }
    };

    energyRate playerEnergy;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        motor = GetComponent<PlayerMotor>();
        controller = GetComponent<PlayerController>();
        
        if (gameObject.tag == "Zombie")
        {
            timeLeft = 1.5f;
        }
        else if (gameObject.tag == "Player") {
            timeLeft = timeForCurrentLevel[SceneManager.GetActiveScene().buildIndex];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.isActive())
        {
            if (!motor.groundCheck())
            {
                playerEnergy.setVerticalRate(1);
            }
            else
            {
                playerEnergy.setVerticalRate(0);
            }
            if (rb.velocity.x != 0)
            {
                playerEnergy.setHorrizontalRate(1);
            }
            else
            {
                playerEnergy.setHorrizontalRate(0);
            }
            timeLeft -= playerEnergy.getRate() * Time.deltaTime;
            if (timeLeft < 0.0f)
            {
                controller.kill();
            }
        }
	}
}
