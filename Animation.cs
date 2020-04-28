using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public static Animation Instance = null;
    
    public GameObject Enemy_Explosion;
    public GameObject Player_Explosion;

    private float AnimationTime = 2.5f;
    private Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        InstanceVerify();

        // Object Reference Section 

        Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerAnimation();
    }

    public void InstanceVerify()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EnemyExplosion(Vector2 position)
    {
        var anim = Instantiate(Enemy_Explosion, position, Quaternion.identity) as GameObject;
        Destroy(anim, AnimationTime);
    }

    public void PlayerExplosion(Vector2 position)
    {
        var anim = Instantiate(Player_Explosion, position, Quaternion.identity) as GameObject;
        Destroy(anim, AnimationTime);
    }

    public void PlayerAnimation()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Anim.SetBool("isLeftPressed", false);
            Anim.SetBool("isRightPressed", true);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Anim.SetBool("isRightPressed", false);
            Anim.SetBool("isLeftPressed", false);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Anim.SetBool("isRightPressed", false);
            Anim.SetBool("isLeftPressed", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Anim.SetBool("isRightPressed", false);
            Anim.SetBool("isLeftPressed", false);
        }
    }
}
