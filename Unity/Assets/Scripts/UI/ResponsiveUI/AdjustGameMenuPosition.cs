using UnityEngine;

public class AdjustGameMenuPosition : MonoBehaviour
{

    [SerializeField] private RectTransform gameMenu; // above the footer
    [SerializeField] private RectTransform projectProgression; // footer
    [SerializeField] private RectTransform gameInformation; // header
    [SerializeField] private RectTransform globalCanvas; // parent Canvas


    // between the footer and header
    [SerializeField] private RectTransform trainingMenu;
    [SerializeField] private RectTransform publicityMenu;
    [SerializeField] private RectTransform hiringMenu;


    void Start()
    {
        // setting gameMenu position
        float projectProgressionHeight = projectProgression.rect.height;
        float gameMenuHalfHeight = gameMenu.rect.height / 2;
        float gameMenuHalfWidth = gameMenu.rect.width / 2;
        gameMenu.anchoredPosition = new Vector2(gameMenuHalfWidth, projectProgressionHeight + gameMenuHalfHeight);

        // setting other menus position
        float gameInformationHeight = gameInformation.rect.height;

        float top = gameInformationHeight;
        float bottom = projectProgressionHeight;

        trainingMenu.offsetMin = new Vector2 (trainingMenu.offsetMin.x, bottom);
        trainingMenu.offsetMax = new Vector2 (trainingMenu.offsetMax.x, -top);

        publicityMenu.offsetMin = trainingMenu.offsetMin;
        publicityMenu.offsetMax = trainingMenu.offsetMax;

        hiringMenu.offsetMin = trainingMenu.offsetMin;
        hiringMenu.offsetMax = trainingMenu.offsetMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
