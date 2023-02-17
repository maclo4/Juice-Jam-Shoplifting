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
    [HideInInspector] public SecurityAi securityAi;
    private bool playerSpottedLastFrame;
    private Transform playerTransform;
    public Color meshColor;
    private Material meshMaterial;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshMaterial = GetComponent<MeshRenderer>().material;
        meshMaterial.color = meshColor;
        
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        var playerInLineOfSight = false;
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;

        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        vertices[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        for (var i = 0; i <= rayCount; i++) 
        {
            Vector3 vertex;
            var raycastHit2D = Physics2D.Raycast(origin, 
                UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            
            if (raycastHit2D.collider == null) 
            {
                // No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            } 
            else 
            {
                // Hit object
                vertex = raycastHit2D.point;
                
                if (raycastHit2D.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("enemy vision");
                    Debug.DrawLine(origin, 
                        raycastHit2D.point, Color.red, Time.deltaTime );
                    playerInLineOfSight = true;
                    playerTransform = raycastHit2D.transform;
                }
            }
            vertices[vertexIndex] = vertex;

            if (i > 0) 
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        
        
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
    public float GetAimDirection() {
        return startingAngle;
    }

    public void SetFoV(float fov) {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance) {
        this.viewDistance = viewDistance;
    }

    public void SetColor(Color color)
    { 
        meshMaterial.color = color;
    }
}
