using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public CutsceneManager cutsceneManager;
    public CharacterController characterController;
    
    public void TriggerCaughtScreen()
    {
        Debug.Log("caught screen loading");
        cutsceneManager.EnableCaughtScreen();
    }

    public void TriggerTeleport()
    {
        characterController.TeleportPlayer();
    }

    public void EndTeleportPlayer()
    {
        characterController.teleporting = false;
    }
}
