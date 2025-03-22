using TMPro;
using UnityEngine;

public class GameInformationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DeadLineText;
    [SerializeField] private TextMeshProUGUI BudgetText;
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
        // retrieve remaining days and format
        int deadlineTimeInDaysLeft = ProjectManager.GetInstance().deadlineTimeInDaysLeft;

        // change the format into week - day left, quotient = number of weeks and reste = number of days
        int remainingWeeks = deadlineTimeInDaysLeft / 7;
        int remainingDays = deadlineTimeInDaysLeft % 7;

        // concat into a single string to send it to a single TextMeshProUI
        string formatedDeadLine = remainingWeeks.ToString() + "W " + remainingDays.ToString() + "D";

        DeadLineText.text = formatedDeadLine;

    }
}
