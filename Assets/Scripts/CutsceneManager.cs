using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public GameObject caughtScreen;
    public Button firstButton;
    public TMP_Text amountOwed;
    public List<GameObject> objectsToDisable;
    public GameObject cutsceneCamera, mainCamera;
    public GameObject game;
    public CharacterController characterController;

    public void EnableCaughtScreen()
    {
        cutsceneCamera.SetActive(true);
        //game.SetActive(false);
        //mainCamera.SetActive(false);
        caughtScreen.SetActive(true);
        firstButton.Select();

        amountOwed.text = "$" + characterController.valueStolen;
        /*foreach (var objectToDisable in objectsToDisable)
        {
            objectToDisable.SetActive(false);
        }*/
    }
}
