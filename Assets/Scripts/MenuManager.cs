 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> objectsToInitialize;
    public void RestartGame()
    {
        Debug.Log("button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        foreach (var objectToInitialize in objectsToInitialize)
        {
            objectToInitialize.SetActive(true);
        }
    }
}
