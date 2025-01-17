using System.Collections;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class SignInManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private Button signInButton;
    [SerializeField] private TextMeshProUGUI errorText;

    void Start()
    {
        signInButton.onClick.AddListener(HandleSignin);
    }

    void Update()
    {
        
    }

    private void HandleSignIn()
    {
        StartCoroutine(SignInCouroutine);
    }

    private IEnumerator SignInCoroutine()
    {
        var signInData = new SignInRequest
        {

        }
    }
}
