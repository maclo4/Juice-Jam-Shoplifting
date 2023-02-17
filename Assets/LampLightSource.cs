using UnityEngine;

public class LampLightSource : MonoBehaviour
{
    public FieldOfView fieldOfView;
    public GameObject fieldOfViewGameObject;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroySelf", 60, 90);
        
        fieldOfViewGameObject.transform.position = Vector3.zero;
    }

    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
    }
    
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
