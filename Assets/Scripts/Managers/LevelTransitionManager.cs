using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void RestartGame()
    {
        _audioSource.Play();
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        _audioSource.Play();
        Application.Quit();
    }

    public void StartGame()
    {
        _audioSource.Play();
        SceneManager.LoadScene("Main");
    }
    public void EnableCaughtScreen()
    {
        SceneManager.LoadScene("Scenes/Caught Menu");
    }
}