using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationEventManager : MonoBehaviour
{
    public LevelTransitionManager levelTransitionManager;
    [FormerlySerializedAs("characterController")] public PlayerCharacterController playerCharacterController;
    
    public void TriggerCaughtScreen()
    {
        Debug.Log("caught screen loading");
        levelTransitionManager.LoadGameOverScreen();
    }

    public void TriggerTeleport()
    {
        playerCharacterController.TeleportPlayer();
    }

    public void EndTeleportPlayer()
    {
        playerCharacterController.teleporting = false;
    }
}
