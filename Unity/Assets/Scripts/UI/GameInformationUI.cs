using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameInformationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DeadLineText;
    [SerializeField] private TextMeshProUGUI BudgetText;

    private static GameInformationUI instance;

    public static GameInformationUI GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }

        instance = this;
    }

    private void OnEnable()
    {
        EventManager.AddListener(EventManager.EventType.DayEnd, UpdateDeadlineDisplay, 1);
        EventManager.AddListener(EventManager.EventType.DayEnd, UpdateBudgetDisplay, 4);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventManager.EventType.DayEnd, UpdateDeadlineDisplay);
        EventManager.RemoveListener(EventManager.EventType.DayEnd, UpdateBudgetDisplay);
    }

    public void UpdateDeadlineDisplay()
    {
        // retrieve remaining days and format it to be displayable
        int deadlineTimeInDaysLeft = ProjectManager.GetInstance().deadlineTimeInDaysLeft;

        // change the format into week - day left, quotient = number of weeks and reste = number of days
        int remainingWeeks = deadlineTimeInDaysLeft / 7;
        int remainingDays = deadlineTimeInDaysLeft % 7;

        // concat into a single string to send it to a single TextMeshProUI
        string formatedDeadLine = remainingWeeks.ToString() + "W " + remainingDays.ToString() + "D";

        DeadLineText.text = formatedDeadLine;

    }

    public void UpdateBudgetDisplay()
    {
        // retrieve current budget and format it to be displayable
        int currentBudget = ProjectManager.GetInstance().currentBudget;

        // change the format into a string with the dollar symbol
        string formatedBudget = currentBudget.ToString() + "$";

        BudgetText.text = formatedBudget;
    }
}