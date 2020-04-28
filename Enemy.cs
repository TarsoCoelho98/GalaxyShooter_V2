using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private float Speed = 5;

    // Reload Position Section 

    public float YNegativeLimit = -6.7f;
    public float YPositiveLimit = 6.7f;
    public static float XPositiveLimit = 7.5f;
    public static float XNegativeLimit = -7.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ReloadPosition();
    }

    public void Move()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    public void ReloadPosition()
    {
        float xReloadPosition = Random.Range(XNegativeLimit, XPositiveLimit);

        if (transform.position.y < YNegativeLimit)
            transform.position = new Vector2(xReloadPosition, YPositiveLimit);
    }
}
