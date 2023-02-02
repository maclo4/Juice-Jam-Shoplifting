using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public CutsceneManager cutsceneManager;
    
    public void TriggerCaughtScreen()
    {
        Debug.Log("caught screen loading");
        cutsceneManager.EnableCaughtScreen();
    }
}
