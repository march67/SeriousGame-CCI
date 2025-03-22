using UnityEngine;

public class GameInformationUI : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener(EventManager.EventType.DayEnd, UpdateDeadlineDisplay, 1);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventManager.EventType.DayEnd,UpdateDeadlineDisplay);
    }

    private void UpdateDeadlineDisplay()
    {
        int remainingDays = ProjectManager.GetInstance().deadlineTimeInDaysLeft;
        Debug.Log("Jours restants :" + remainingDays);
    }
}
