using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] PowerUp = new GameObject[3];

    public float XPowerUpPositiveLimit = 6.6f;
    public float XPowerUpNegativeLimit = -6.6f;
    public float YFixedPowerUpSpawnPosition = 5.6f;

    public const float YFixedEnemySpawnPosition = 6.4f;
    private const int EnemySpawnRate = 3;
    public const int PowerUpSpawnRate = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator SpawnEnemy()
    {
        while(!Manager.Instance.GameOver)
        {
            yield return new WaitForSeconds(EnemySpawnRate);
            Instantiate(EnemyPrefab, GetEnemySpawnPosition(), Quaternion.identity);
        }
    }

    public Vector2 GetEnemySpawnPosition()
    {
        float xPosition = Random.Range(Enemy.XNegativeLimit, Enemy.XPositiveLimit);
        return new Vector2(xPosition, YFixedEnemySpawnPosition);
    }

    public IEnumerator SpawnPowerUp()
    {
        while (!Manager.Instance.GameOver)
        {
            yield return new WaitForSeconds(PowerUpSpawnRate);

            var powerUpObjectPosition = GetPowerUpSpawnObjectPosition();
            Instantiate(powerUpObjectPosition.Key, powerUpObjectPosition.Value, Quaternion.identity);
        }
    }

    public KeyValuePair<GameObject, Vector2> GetPowerUpSpawnObjectPosition()
    {
        var powerUp = PowerUp[Random.Range(0, PowerUp.Count())];
        var powerUpXPosition = Random.Range(XPowerUpNegativeLimit, XPowerUpPositiveLimit);

        return new KeyValuePair<GameObject, Vector2>(powerUp, new Vector2(powerUpXPosition, YFixedPowerUpSpawnPosition));

    }

}
