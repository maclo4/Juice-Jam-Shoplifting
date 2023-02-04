using UnityEngine;

public class ItemTrap : MonoBehaviour
{
    public float chaseSlowdown = 2f;
    public float walkSlowdown = .5f;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        Debug.Log("tag was enemy");
        var securityAi = other.GetComponent<SecurityAi>();
        // for some reason it was colliding multiple times
        if (securityAi.trapColliding == true) return;
        securityAi.EnterTrap(walkSlowdown, chaseSlowdown);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        Debug.Log("OnTriggerExit");
        var securityAi = other.GetComponent<SecurityAi>();
        if (securityAi.trapColliding == false) return;
        
        securityAi.ExitTrap(walkSlowdown, chaseSlowdown);
    }
}
