using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public AudioClip Audio;

    // Score Section 

    public int Score = 0;

    // Health Section 

    public const int DefaultHealth = 3;
    public int CurrentHealth;

    // Shoot Section 

    public GameObject Laser;
    public GameObject TripleLaser;

    // Move Section

    public const float DefaultSpeed = 10;
    public float CurrentSpeed;

    // Player Bounds Section 

    private float XPositiveLimit = 8.25f;
    private float XNegativeLimit = -8.25f;
    private float YPositiveLimit = 3.8f;
    private float YNegativeLimit = -3.8f;

    // CoolDown Section 

    public const float FireRate = 0.2f;
    public float NextFire;

    // Power Ups Section

    public bool CanTripleShoot = false;
    public bool IsSpeedActive = false;
    public bool isShieldActive = false;

    // Shield Section 

    public GameObject Shield;

    // Thruster Section 

    public GameObject Thruster;

    // Engine Section 

    public GameObject Engine_Right;
    public GameObject Engine_Left;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(XNegativeLimit, transform.position.y);

        ConfigureInitualProperties();
        ConfigureInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerBounds();
        Shoot(CanTripleShoot);
    }

    public void ConfigureInitialPosition()
    {
        transform.position = Vector2.zero;
    }

    public void ConfigureInitualProperties()
    {
        CurrentSpeed = DefaultSpeed;
        CurrentHealth = DefaultHealth;
        UIManager.Instance.UpdateScore(Score);
    }

    public void Move()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        transform.Translate(new Vector2(horizontal * CurrentSpeed * Time.deltaTime, vertical * CurrentSpeed * Time.deltaTime));
    }

    public void PlayerBounds()
    {
        if (transform.position.x > XPositiveLimit)
            transform.position = new Vector2(XNegativeLimit, transform.position.y);

        if (transform.position.x < XNegativeLimit)
            transform.position = new Vector2(XPositiveLimit, transform.position.y);

        if (transform.position.y > YPositiveLimit)
            transform.position = new Vector2(transform.position.x, YPositiveLimit);

        if (transform.position.y < YNegativeLimit)
            transform.position = new Vector2(transform.position.x, YNegativeLimit);
    }

    public void Shoot(bool canTripleShoot)
    {
        if (Time.time < NextFire)
            return;


        if (/*Input.GetKeyDown(KeyCode.Mouse0) || */CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            //#if UNITY_EDITOR

            //            print("running at Unity Editor");
            //#endif

            if (canTripleShoot)
                Instantiate(TripleLaser, new Vector2(transform.position.x, transform.position.y + 1.2f), Quaternion.identity);
            else
                Instantiate(Laser, new Vector2(transform.position.x, transform.position.y + 1.2f), Quaternion.identity);

            NextFire = Time.time + FireRate;
        }
    }

    public IEnumerator DisableTripleShootRoutine(float powerUpTime)
    {
        yield return new WaitForSeconds(powerUpTime);
        CanTripleShoot = false;
    }

    public void EnableTripleShoot(float powerUpTime)
    {
        CanTripleShoot = true;
        StartCoroutine(DisableTripleShootRoutine(powerUpTime));
    }

    public void EnableSpeed(float powerUpTime, float boost)
    {
        CurrentSpeed += boost;
        IsSpeedActive = true;

        StartCoroutine(DisableSpeedRoutine(powerUpTime));
    }

    public IEnumerator DisableSpeedRoutine(float powerUpTime)
    {
        yield return new WaitForSeconds(powerUpTime);

        CurrentSpeed = DefaultSpeed;
        IsSpeedActive = false;
    }

    public void EnableShield(float powerUpTime)
    {
        Shield.SetActive(true);
        isShieldActive = true;

        StartCoroutine(DisableShieldRoutine(powerUpTime));
    }

    public IEnumerator DisableShieldRoutine(float powerUpTime)
    {
        yield return new WaitForSeconds(powerUpTime);

        Shield.SetActive(false);
        isShieldActive = false;
    }

    public void Damage()
    {
        if (isShieldActive)
            return;

        --CurrentHealth;
        EngineDamage();

        if (CurrentHealth < 0)
        {
            Animation.Instance.PlayerExplosion(transform.position);
            AudioSource.PlayClipAtPoint(Audio, transform.position);

            UIManager.Instance.UpdateHighScore(Score);
            Manager.Instance.FinishMatch();
        }
        else
            UIManager.Instance.UpdateHealth(CurrentHealth);
    }

    public void UpdateScore()
    {
        Score++;
        UIManager.Instance.UpdateScore(Score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyPosition = collision.transform.position;

            Destroy(collision.gameObject);
            UpdateScore();

            Animation.Instance.EnemyExplosion(enemyPosition);
            AudioSource.PlayClipAtPoint(Audio, transform.position);

            Damage();
        }
    }

    public void EngineDamage()
    {
        if (Engine_Right.active && Engine_Left.active)
            return;

        if (Engine_Right.active)
            Engine_Left.SetActive(true);
        else
            Engine_Right.SetActive(true);
    }
}
