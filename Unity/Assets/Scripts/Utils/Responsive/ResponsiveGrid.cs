using UnityEngine;

public class ResponsiveGrid : MonoBehaviour
{
    private float scaleDefault = 1f / ( 1080f / 2340f );

    [SerializeField] Grid grid;

    private Transform gridTransform;

    void Start()
    {
        
        gridTransform = grid.GetComponent<Transform>();
        AdjustGridScale();
    }

    private void Awake()
    {
        gridTransform = grid.GetComponent<Transform>();
        OnResolutionChanged();
    }

    void OnResolutionChanged()
    {
        AdjustGridScale();
    }

    void AdjustGridScale()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float scale = scaleDefault * aspectRatio;
        gridTransform.localScale = new Vector3(scale, scale, 1);
        Debug.Log("Responsive grid : grid scale = " + scale);
    }
}
