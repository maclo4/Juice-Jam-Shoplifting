using UnityEngine;

public class LampLightSource : MonoBehaviour
{
    private FieldOfView fieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroySelf", 60, 60);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
