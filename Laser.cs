using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Speed = 10;
    public float GameTime = 1.5f;
    public Player PlayerComponent;

    public AudioClip Audio;
    public AudioClip AudioExplosion;

    // Start is called before the first frame update
    void Start()
    {
        // Objects Reference Section

        PlayerComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Start Methods Section 

        AudioSource.PlayClipAtPoint(Audio, transform.position);
        Destroy(this.gameObject, GameTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyPosition = collision.transform.position;
            Destroy(collision.gameObject);

            PlayerComponent.UpdateScore();

            Animation.Instance.EnemyExplosion(enemyPosition);
            AudioSource.PlayClipAtPoint(Audio, transform.position);

            Destroy(this.gameObject);
        }
    }

}
