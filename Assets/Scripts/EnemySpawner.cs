using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public int[] Gold;
    public int Wave;
    public static EnemySpawner instance;
    public GameObject[] Pool;
    public Button StartWave;
    public TextMeshProUGUI waveIndicator;
    private float nextSpawn;
    public float timeBetweenSpawns;

    private void Start()
    {
        instance = this;
        nextSpawn = Time.time;
    }

    private void Update()
    {
        WaveHandler();
        waveIndicator.text = "Wave " + Wave;
    }

    private void WaveHandler()
    {
        if(Gold.Length < Wave + 1)
        {
            GameManager.instance.finishLevel();
            return;
        }
        if(Gold[Wave] > 0)
        {
            if (nextSpawn < Time.time)
            {
                //There is money and gold to spend here, spend it
                attemptSpawn();

                nextSpawn = Time.time + timeBetweenSpawns;
            }
        }
        else
        {
            //No more gold, enable next wave button
            StartWave.gameObject.SetActive(true);
        }
    }

    public void nextWave()
    {
        Wave += 1;
        StartWave.gameObject.SetActive(false);
    }

    void attemptSpawn()
    {
        int temp = Random.Range(0, Pool.Length);
        GameObject attempt = Pool[temp];
        EnemyAI ai = attempt.GetComponent<EnemyAI>();
        if (ai.minWave <= Wave && ai.cost <= Gold[Wave])
        {
            Instantiate(attempt);
            Gold[Wave] -= ai.cost;
        }
        else
        {
            //Default to first type of enemy, regardless of cost. If the cost is more, the wave will end after it
            Instantiate(Pool[0]);
            Gold[Wave] -= ai.cost;
        }
    }
}
