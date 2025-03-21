using UnityEngine;

public class GameInformationUI : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnDayEnd += UpdateDeadlineDisplay;
    }

    private void OnDisable()
    {
        EventManager.OnDayEnd -= UpdateDeadlineDisplay;
    }

    private void UpdateDeadlineDisplay()
    {
        int remainingDays = ProjectManager.GetInstance().deadlineTimeInDaysLeft;
        Debug.Log("Jours restants :" + remainingDays);
    }
}
