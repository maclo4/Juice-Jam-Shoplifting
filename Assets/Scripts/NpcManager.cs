using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public Transform spawn;
    public GameObject baseSecurityPrefab;
    private int securityLevel = 100;
    private int prevSecurityLevel = 100;
    public int securityLevelIncrement = 1;
    public int increaseDifficultyFrequency = 1;
    private List<SecurityAi> securityAis;


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
        prevSecurityLevel = securityLevel ;
        securityLevel += securityLevelIncrement;
        
        if (PassedThreshold(200) || PassedThreshold(300) )
        {
            SpawnSecurityGuard();
        }
        if (PassedThreshold(400) || PassedThreshold(500))
        {
            var securityGuard = SpawnSecurityGuard();
            var ai = securityGuard.GetComponent<SecurityAi>();
            var spriteRenderer = securityGuard.GetComponent<SpriteRenderer>();
            
            ai.chaseSpeed += 2;
            ai.walkSpeed += 2;
            
            spriteRenderer.color = Color.yellow;
        }
        else if (PassedThreshold(600) || PassedThreshold(700) || PassedThreshold(800)
                 || PassedThreshold(900) || PassedThreshold(1000) || PassedThreshold(1100))
        {
            var securityGuard = SpawnSecurityGuard();
            var ai = securityGuard.GetComponent<SecurityAi>();
            var spriteRenderer = securityGuard.GetComponent<SpriteRenderer>();
            
            ai.chaseSpeed += 5;
            ai.walkSpeed += 5;
            
            spriteRenderer.color = Color.red;
        }
    }

    private bool PassedThreshold(int threshold)
    {
        return securityLevel > threshold && prevSecurityLevel < threshold;
    }
    private GameObject SpawnSecurityGuard()
    {
        return Instantiate(baseSecurityPrefab, spawn.position, Quaternion.identity);
    }

    public void TrackItemStolen(Item item)
    {
        var prevSecurityLevel = (int) securityLevel;
        securityLevel += item.securityChange;
        
        if (prevSecurityLevel < (int) securityLevel)
        {
             SpawnSecurityGuard();
        }
    }

    public float GetSecurityLevel()
    {
        return securityLevel;
    }

    public void SetVisionDistance(float viewDistance)
    {
        foreach (var ai in securityAis)
        {
            ai.enemyVision.SetViewDistance(viewDistance);
        }
    }
}
