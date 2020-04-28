using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public float Timer;
    public bool GameOver = true;

    public GameObject Player;

    public static Manager Instance = null;
    Player playerComponent;

    private void Awake()
    {
        VerifyInstance();

        // Objects Reference Section 

        playerComponent = Player.GetComponent<Player>();

        StartGame();
    }

    public void VerifyInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }    

    public void UpdateTimer()
    {
        Timer = Time.time;
    }

    public void FinishMatch()
    {
        GameOver = true;
        Player.SetActive(false);
        SceneManager.LoadScene("Zero");
    }

    public void StartGame()
    {
        GameOver = false;
        Player.SetActive(true);

        playerComponent.ConfigureInitualProperties();
        playerComponent.ConfigureInitialPosition();
    }
}
