using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static AuthDTOs;

public class SignUpManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputUsername;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private Button _buttonSignIn;
    [SerializeField] private Button _buttonSignUp;

    private const string SIGN_IN_URL = "https://localhost:7239/User/register";
    private const string STR_SCENE_SIGN_IN = "Scenes/Scene_SignIn";

    void Start()
    {
        _buttonSignUp.onClick.AddListener(() => _ = HandleSignUp());
        _buttonSignIn.onClick.AddListener(LoadSceneSignIn);
    }

    void Update()
    {

    }

    private void LoadSceneSignIn()
    {
        StartCoroutine(LoadSceneAsyncSignIn());
    }

    private IEnumerator LoadSceneAsyncSignIn()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(STR_SCENE_SIGN_IN);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private async Task HandleSignUp()
    {
        try
        {
            var signInData = new SignInRequest
            {
                Email = _inputEmail.text,
                Username = _inputUsername.text,
                Password = _inputPassword.text
            };

            string jsonData = JsonUtility.ToJson(signInData);

            using (var request = new UnityWebRequest(SIGN_IN_URL, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Succesful sign up !");
                }
                else
                {
                    Debug.LogError($"Error when trying to sign up : {request.error}");
                }

            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception when trying to sign up : {ex.Message}");
        }
    }
}
