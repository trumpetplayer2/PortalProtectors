using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioSource sfxSource;
    public AudioClip sfxButton;

    private void Awake()
    {
        if (this.GetComponent<AudioSource>() != null)
        {
            sfxSource = this.GetComponent<AudioSource>();
        }
    }
    public void SwitchOpenMenu(GameObject newMenu)
    {
        if (newMenu == null) { Debug.LogError("Object did not exist!"); return; }
        newMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void switchScene(string scene)
    {
        if(SceneManager.GetSceneByName(scene) == null) { Debug.LogError("Scene " + scene + "did not exist!"); return; }
        SceneManager.LoadScene(scene);
    }

    //Exit the game
    public void Exit()
    {
        Application.Quit();
        
    }

    private void quitDelay()
    {
        Invoke("Exit", 0.5f);
    }

    public void playSfx(AudioClip sfx)
    {
        if (sfx != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfx);
        }
    }

    public void playSfx()
    {
        playSfx(sfxButton);
    }
}
