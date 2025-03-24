using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{

    [SerializeField] private Camera camera;
    private Camera cameraComponent;

    private float orthographicSizeDefault = 7f / ( 1080f / 2340f ) ;  

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        cameraComponent = camera.GetComponent<Camera>();
        OnResolutionChanged();
    }

    void OnResolutionChanged()
    {
        AdjustOrthographicSize();
    }

    void AdjustOrthographicSize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float orthographicSize = orthographicSizeDefault * aspectRatio;
        cameraComponent.orthographicSize = orthographicSize;
        Debug.Log("Responsive camera : orthographicSize = " + orthographicSize);
    }
}
