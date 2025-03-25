using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public GameObject gameMenu;
    [SerializeField] public GameObject trainingMenu;
    [SerializeField] public GameObject publicityMenu;
    [SerializeField] public GameObject hiringMenu;

    public void ToggleGameMenu()
    {
        gameMenu.SetActive(!gameMenu.activeSelf);
    }

    public void ToggleTrainingMenu()
    {
        trainingMenu.SetActive(!trainingMenu.activeSelf);
    }

    public void TogglePublicityMenu()
    {
        publicityMenu.SetActive(!publicityMenu.activeSelf);
    }

    public void ToggleHiringMenu()
    {
        hiringMenu.SetActive(!hiringMenu.activeSelf);
    }

    public void CloseGrandParentGameObject(GameObject button)
    {
        Transform parent = button.transform.parent;
        Transform grandParent = button.transform.parent.parent;
        if (parent != null && parent.parent != null)
        {
            parent.parent.gameObject.SetActive(false);
        }
    }

    public void CloseParentGameObject(GameObject button)
    {
        Transform parent = button.transform.parent;
        if (parent != null)
        {
            parent.gameObject.SetActive(false);
        }
    }
}
