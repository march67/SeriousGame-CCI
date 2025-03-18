using TMPro;
using UnityEngine;

public class StatProgression : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI musicProgression;
    [SerializeField] public TextMeshProUGUI artProgression;
    [SerializeField] public TextMeshProUGUI creativityProgression;
    [SerializeField] public TextMeshProUGUI funProgression;

    public int musicProgressionValue;
    public int artProgressionValue;
    public int creativityProgressionValue;
    public int funProgressionValue;

    public int musicProjectGoal = 100;
    public int artProjectGoal = 100;
    public int creativityProjectGoal = 100;
    public int funProjectGoal = 100;

    void Start()
    {
        musicProgression.text = musicProgressionValue.ToString() + " / " + musicProjectGoal.ToString();
        artProgression.text = artProgressionValue.ToString() + " / " + artProjectGoal.ToString();
        creativityProgression.text = creativityProgressionValue.ToString() + " / " + creativityProjectGoal.ToString();
        funProgression.text = funProgressionValue.ToString() + " / " + funProjectGoal.ToString();
    }


    void Update()
    {
        musicProgression.text = musicProgressionValue.ToString() + " / " + musicProjectGoal.ToString();
        artProgression.text = artProgressionValue.ToString() + " / " + artProjectGoal.ToString();
        creativityProgression.text = creativityProgressionValue.ToString() + " / " + creativityProjectGoal.ToString();
        funProgression.text = funProgressionValue.ToString() + " / " + funProjectGoal.ToString();
    }
}
