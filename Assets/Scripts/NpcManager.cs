using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public Transform spawn;
    public GameObject baseSecurityPrefab;
    public GameObject fwipSecurityPrefab;
    public GameObject blarghSecurityPrefab;
    private int securityLevel = 100;
    private int prevSecurityLevel = 100;
    public int securityLevelIncrement = 1;
    public int increaseDifficultyFrequency = 1;
    private List<SecurityAi> securityAis;
    public HudManager hudManager;

    private float speedModifier = 0;

    private void Awake()
    {
        if (FindObjectsOfType(typeof(SecurityAi)) is SecurityAi[] securityAiArray)
            securityAis = securityAiArray.ToList();
        else
            securityAis = new List<SecurityAi>();
    }

    private void Start()
    {
        InvokeRepeating("IncreaseDifficulty", 
            increaseDifficultyFrequency, increaseDifficultyFrequency);
    }

    private void IncreaseDifficulty()
    {
        IncreaseDifficulty(securityLevelIncrement);
    }
    private void IncreaseDifficulty(int increment)
    {
        prevSecurityLevel = securityLevel;
        securityLevel += increment;
        if (securityLevel < 1500)
            hudManager.securityMeter.value = securityLevel / 1500f;
        else
        {
            hudManager.securityMeter.value = 1;
        }
        
        
        if (PassedThreshold(200) || PassedThreshold(400))
        {
            var securityGuard = SpawnSecurityGuard(baseSecurityPrefab);
            var ai = securityGuard.GetComponent<SecurityAi>();
            
            ai.chaseSpeed += speedModifier;
            ai.walkSpeed += speedModifier;

            speedModifier += .25f;
        }
        if (PassedThreshold(300) || PassedThreshold(400) || PassedThreshold(500) || PassedThreshold(1000))
        {
            var securityGuard = SpawnSecurityGuard(fwipSecurityPrefab);
            var ai = securityGuard.GetComponent<SecurityAi>();
            
            ai.chaseSpeed += speedModifier;
            ai.walkSpeed += speedModifier;

            speedModifier += .25f;
        }
        else if ( PassedThreshold(600) || PassedThreshold(700) || PassedThreshold(800)
                 || PassedThreshold(900) || PassedThreshold(1000) || PassedThreshold(1100)
                 || PassedThreshold(1200) || PassedThreshold(1300) || PassedThreshold(1400) 
                 || PassedThreshold(1500))
        {
            //var randomNumber = UnityEngine.Random.Range(0, 3);
            var securityGuard = SpawnSecurityGuard(blarghSecurityPrefab);
            var ai = securityGuard.GetComponent<SecurityAi>();
            
            ai.chaseSpeed += speedModifier;
            ai.walkSpeed += speedModifier;

            speedModifier += .25f;
        }
    }

    private bool PassedThreshold(int threshold)
    {
        return securityLevel > threshold && prevSecurityLevel < threshold;
    }
    private GameObject SpawnSecurityGuard(GameObject securityGuard)
    {
        return Instantiate(securityGuard, spawn.position, Quaternion.identity);
    }

    public void TrackItemStolen(Item item)
    {
        IncreaseDifficulty(item.securityChange);
    }

    public float GetSecurityLevel()
    {
        return securityLevel;
    }

    public void SetVisionDistance(float viewDistance)
    {
        foreach (var ai in securityAis)
        {
            ai.SetVisionDistance(viewDistance);
        }
    }
}
