using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    [SerializeField]
    RectTransform healthBarRect;

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

        if (healthBarRect == null && gameObject.tag == "Player") {
            Debug.LogError("ERROR: No health bar found");
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

            if (gameObject.tag == "Player")
            {
                float _healthBarValue = (float)timeLeft / timeForCurrentLevel[SceneManager.GetActiveScene().buildIndex];
                healthBarRect.localScale = new Vector3(_healthBarValue, healthBarRect.localScale.y, healthBarRect.localScale.z);
            }
            else if (gameObject.tag == "Zombie")
            {
                float _healthBarValue = (float)timeLeft / 1.5f;
                healthBarRect.localScale = new Vector3(_healthBarValue, healthBarRect.localScale.y, healthBarRect.localScale.z);
            }
            if (timeLeft < 0.0f)
            {
                controller.kill();
            }
        }
        else
        {
            if (gameObject.tag == "Zombie")
            {
                healthBarRect.localScale = new Vector3(0.0f, healthBarRect.localScale.y, healthBarRect.localScale.z);
            }
        }
	}
}
