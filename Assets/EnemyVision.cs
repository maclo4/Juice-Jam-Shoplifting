using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    public float fov = 360f;
    public float viewDistance = 10f;
    public int rayCount = 150;
    private Vector3 origin;
    private float startingAngle;
    public SecurityAi securityAi;
    private bool playerSpottedLastFrame;
    private Transform playerTransform;

    private void Start()
    {
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        var playerInLineOfSight = false;
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;
        
        for (var i = 0; i <= rayCount; i++) 
        {
            var raycastHit2D = Physics2D.Raycast(origin, 
                UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            
            if (raycastHit2D.collider != null && raycastHit2D.transform.gameObject.CompareTag("Player")) 
            {
                Debug.Log("enemy vision");
                Debug.DrawLine(origin, 
                    raycastHit2D.point, Color.red, Time.deltaTime );
                playerInLineOfSight = true;
                playerTransform = raycastHit2D.transform;
                break;
            } 
            angle -= angleIncrease;
        }

        if (playerInLineOfSight)
        {
            securityAi.SetPlayerPathOnSight(playerTransform);
        }
        else if(playerSpottedLastFrame)
        {
            securityAi.StartChaseTimer();
        }

        playerSpottedLastFrame = playerInLineOfSight;
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov) {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance) {
        this.viewDistance = viewDistance;
    }
}
