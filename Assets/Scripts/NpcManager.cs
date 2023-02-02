using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public Transform spawn;
    public GameObject baseSecurityPrefab;
    private float securityLevel = 1;
    

    private void SpawnSecurityGuard()
    {
        var securityGuard = Instantiate(baseSecurityPrefab, spawn.position, Quaternion.identity);
        //securityGuard.GetComponent<SecurityAi>();
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
}
