using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AuthDTOs;

public class SignInManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputUsername;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private Button _buttonSignIn;
    [SerializeField] private Button _buttonSignUp;

    private const string SIGN_UP_URL = "https://localhost:7239/User/login";
    private const string STR_SIGN_UP_SCENE = "Scenes/Scene_SignUp";
    private const string STR_HOME_SCENE = "Scenes/Scene_Invocation";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _buttonSignIn.onClick.AddListener(() => _ = HandleSignUp());
        _buttonSignUp.onClick.AddListener(LoadSceneSignUp);
    }

    private void LoadSceneSignUp()
    {
        StartCoroutine(LoadSceneAsyncSignUp());
    }

    private void LoadSceneHome()
    {
        StartCoroutine(LoadSceneAsyncHome());
    }

    private IEnumerator LoadSceneAsyncSignUp()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(STR_SIGN_UP_SCENE);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    private IEnumerator LoadSceneAsyncHome()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(STR_HOME_SCENE);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Task HandleSignUp()
    {
        try
        {
            var signUpData = new LoginRequest
            {
                Username = _inputUsername.text,
                Password = _inputPassword.text,
            };

            string jsonData = JsonUtility.ToJson(signUpData);

            using ( var request = new UnityWebRequest(SIGN_UP_URL, "POST"))
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
                    Debug.Log("Sign in succesful");
                    LoginResponse tokenData = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                    string jwt = tokenData.accessToken;
                    GameSessionManager.Instance.SetToken(jwt);
                    LoadSceneHome();
                }
                else
                {
                    Debug.LogError($"Error when trying to sign in : {request.error}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception when trying to sign in : {ex.Message}");
        }
    }
}
