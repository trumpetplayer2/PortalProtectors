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
    bool ready = true;

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
        if(Gold.Length < Wave + 1 && ready)
        {
            GameManager.instance.finishLevel();
            ready = false;
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
            if (ready)
            {
                //No more gold, enable next wave button
                StartWave.gameObject.SetActive(true);
                ready = false;
            }
        }
    }

    public void nextWave()
    {
        StartWave.gameObject.SetActive(false);
        if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            //No ending wave early, wait a second before showing the button again
            Invoke("showWave", 1);
        }
        else
        {
            Wave += 1;
            ready = true;
        }
    }

    public void showWave()
    {
        StartWave.gameObject.SetActive(true);
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
