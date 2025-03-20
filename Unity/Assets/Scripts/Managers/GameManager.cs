using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }
    public PlayerStatGeneratorManager playerStatGeneratorManager;

    private void OnApplicationQuit()
    {
        PlayerStatManager.GetInstance().saveAllPlayersStat();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerStatManager.GetInstance().loadAllPlayersStat();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
        Debug.Log("StartGame called");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // Instanciate PlayerStatGeneratorManager if doesn't exist yet
        if (playerStatGeneratorManager == null)
        {
            playerStatGeneratorManager = GetComponent<PlayerStatGeneratorManager>();
        }
    }

}
