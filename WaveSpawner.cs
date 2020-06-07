using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public GameManager gameManager;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 15f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            PlayerStats.Score += 100 * waveIndex;
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    void Start()
    {
        EnemiesAlive = 0;
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position, spawnPoint.rotation);
    }

}
