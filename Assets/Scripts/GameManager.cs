using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject WinMenu;
    public GameObject DeathMenu;
    public GameObject[] pauseObjects;
    public GameObject[] resumeObjects;
    private bool isPaused = false;
    private bool isFinished = false;
    public float gold = 10;
    public TextMeshProUGUI goldIndicator;
    public PathScript startLocation;
    AudioSource sfxSource;
    public AudioClip sfxWin;
    public AudioClip sfxLose;

    private void Start()
    {
        Time.timeScale = 1;
        //Null check
        if (instance == null)
        {
            //Define instance for easy access later
            instance = this;
        }
        isPaused = false;
        //Toggle menu to correct states
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(true);
            }
        }
        if (this.GetComponent<AudioSource>() != null)
        {
            sfxSource = this.GetComponent<AudioSource>();
        }
        WinMenu.SetActive(false);
        DeathMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isFinished)
        {
            togglePause();
        }
        goldIndicator.text = gold + "";
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Death()
    {
        sfxSource.Stop();
        playSfx(sfxLose);
        DeathMenu.SetActive(true);
        togglePause();
        //Music.PlayOneShot(VictoryJingle);
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void switchScenes(string scene)
    {
        if (SceneManager.GetSceneByName(scene) == null) { Debug.LogError("Scene " + scene + "did not exist!"); return; }
        SceneManager.LoadScene(scene);
    }

    public void togglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            //Music.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            //Music.UnPause();
        }
        isPaused = !isPaused;
        //Update all pause/unpause objects
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(isPaused);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(!isPaused);
            }
        }
    }


    public void finishLevel()
    {
        sfxSource.Stop();
        playSfx(sfxWin);
        //Show win menu
        WinMenu.SetActive(true);
        isFinished = true;
        isPaused = false;
        togglePause();
        //Music.PlayOneShot(VictoryJingle);
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void incrementGold(int amount)
    {
        gold += amount;
    }

    public void spawnTower(GameObject Tower)
    {
        Instantiate(Tower, transform.position, Quaternion.identity);
    }

    public void playSfx(AudioClip sfx)
    {
        if (sfx != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfx);
        }
    }
}
