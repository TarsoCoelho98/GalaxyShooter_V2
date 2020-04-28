using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public int HighScore = 0;
    public Text HighScoreText;

    public static UIManager Instance = null;

    // Source Path Section 

    public static string AssetsSourcePath = @"Assets/2D Galaxy Assets/Game/Sprites/UI/Lives/";

    // Health Section 

    public Image HealthImage;
    public Dictionary<int, Sprite> HealthSprites = new Dictionary<int, Sprite>();

    // Score Section 

    public Text Score;


    public Sprite No;
    public Sprite One;
    public Sprite Two;
    public Sprite Three;



    // Start is called before the first frame update
    private void Awake()
    {
        InstanceVerify();
    }

    void Start()
    {
        InitializeParamsReference();
        InitializeHealthImage();

        InitializeHighScore();
        InitializeHighScoreText();
    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void InstanceVerify()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void InitializeHealthImage()
    {
        HealthImage.sprite = HealthSprites.FirstOrDefault(x => x.Key == 3).Value;
    }

    public void InitializeParamsReference()
    {
        HealthSprites.Add(0, No);
        HealthSprites.Add(1, One);
        HealthSprites.Add(2, Two);
        HealthSprites.Add(3, Three);
    }

    public void UpdateHealth(int health)
    {
        HealthImage.sprite = HealthSprites.FirstOrDefault(x => x.Key == health).Value;
    }

    public void UpdateScore(int score)
    {
        Score.text = string.Concat("Score: ", score.ToString());
    }

    public void UpdateHighScore(int currentScore)
    {
        if (HighScore > currentScore)
            return;

        HighScore = currentScore;
        SaveData();
    }

    public void InitializeHighScoreText()
    {
        HighScoreText.text = string.Concat("High Score: ", HighScore.ToString());
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    //public void InitializeHighScoreData()
    //{
    //    PlayerPrefs.SetInt("HighScore", 0);
    //}

    public void InitializeHighScore()
    {
        if (PlayerPrefs.GetInt("HighScore") != null)
            HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

}
