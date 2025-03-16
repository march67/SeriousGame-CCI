using TMPro;
using UnityEngine;

public class StatProgression : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicProgression;
    [SerializeField] private TextMeshProUGUI artProgression;
    [SerializeField] private TextMeshProUGUI creativityProgression;
    [SerializeField] private TextMeshProUGUI funProgression;

    void Start()
    {
        musicProgression.text = "0 / 0";
        artProgression.text = "0 / 0";
        creativityProgression.text = "0 / 0";
        funProgression.text = "0 / 0";
    }


    void Update()
    {
        
    }
}
