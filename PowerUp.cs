using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PowerUp : MonoBehaviour
{
    public AudioClip Audio;

    private float Speed = 5;
    public float YLImit = -5.8f;

    // Power Up Section 

    public Enums.PowerUp PowerUpStatus;
    public float PowerUpTime = 5;
    public float SpeedBoost = 5;

    // Player Section 

    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        // Objects Reference Section

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Initial Configuration Section 

        VerifyPowerUpInstance();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PowerUpLimit();
    }
      
    public void SetActivePowerUp(Enums.PowerUp powerUp)
    {
        switch ((int)powerUp)
        {
            case 0:
                Player.EnableTripleShoot(PowerUpTime);
                break;

            case 1:
                Player.EnableSpeed(PowerUpTime, SpeedBoost);
                break;

            case 2:
                Player.EnableShield(PowerUpTime);
                break;
        }
    }

    public void VerifyPowerUpInstance()
    {
        if (this.gameObject.name.Contains("TripleShoot"))
            PowerUpStatus = Enums.PowerUp.TripleShoot;

        if (this.gameObject.name.Contains("Shield"))
            PowerUpStatus = Enums.PowerUp.Shield;

        if (this.gameObject.name.Contains("Speed"))
            PowerUpStatus = Enums.PowerUp.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(Audio, transform.position);
            
            SetActivePowerUp(PowerUpStatus);
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    public void PowerUpLimit()
    {
        if (transform.position.y < YLImit)
            Destroy(this.gameObject);
    }
}
