using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    private static GameSessionManager _instance;
    private string _jwtToken;

    public static GameSessionManager InstanceGameSessionManager
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameSessionManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("SessionManager");
                    _instance = go.AddComponent<GameSessionManager>();
                }
            }

            return _instance;
        }
    }

    public string JwtToken
    {
        get => _jwtToken;
        set => _jwtToken = value;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Persist through scenes
    }

    public void SetToken(string jwtToken)
    {
        _jwtToken = jwtToken;
    }


    public string GetAuthorizationHeader()
    {
        return $"Bearer {_jwtToken}";
    }

    public bool IsSessionActive()
    {
        return !string.IsNullOrEmpty(_jwtToken);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
