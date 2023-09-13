using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public LayerMask fogOfWarLayer; // LayerMask for the fog of war
    public Shader fogOfWarShader; // Shader for the fog of war

    private Material fogOfWarMaterial; // Material for the fog of war

    private void Start()
    {
        // Create a new material for the fog of war
        fogOfWarMaterial = new Material(fogOfWarShader);
        fogOfWarMaterial.SetVector("_PlayerPos", player.position);
        fogOfWarMaterial.SetFloat("_Radius", Mathf.Infinity);
    }

    private void LateUpdate()
    {
        // Update the player's position in the shader
        fogOfWarMaterial.SetVector("_PlayerPos", player.position);

        // Use a sprite renderer to draw the fog of war
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.gameObject.layer == fogOfWarLayer)
            {
                renderer.material = fogOfWarMaterial;
            }
        }
    }
}
