using Unity.Mathematics;
using UnityEngine;

public class ResponsiveCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Canvas canvasComponent;

    private float defaultWidth = 6.5f / (1080f / 2340f);
    private float defaultHeight = 14f / (1080f / 2340f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        canvasComponent = canvas.GetComponent<Canvas>();
        AdjustCanvasSize();
    }

    void AdjustCanvasSize()
    {
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        float aspectRatio = (float)Screen.width / Screen.height;
        float width = defaultWidth * aspectRatio;
        float height = defaultHeight * aspectRatio;
        rectTransform.sizeDelta = new Vector2(width, height);
        Debug.Log("Responsive grid : grid scale = " + rectTransform.sizeDelta);
    }
}
