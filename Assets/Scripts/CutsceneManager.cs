using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public GameObject caughtScreen;
    public Button firstButton;
    public List<GameObject> objectsToDisable;
    public GameObject cutsceneCamera, mainCamera;
    public GameObject game;

    public void EnableCaughtScreen()
    {
        cutsceneCamera.SetActive(true);
        game.SetActive(false);
        //mainCamera.SetActive(false);
        caughtScreen.SetActive(true);
        firstButton.Select();
        
        /*foreach (var objectToDisable in objectsToDisable)
        {
            objectToDisable.SetActive(false);
        }*/
    }
}
