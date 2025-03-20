using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TextAssetPoolManager : MonoBehaviour
{
    // one pool = all dialogues for one character
    private ObjectPool<TextAsset> pool1;
    private ObjectPool<TextAsset> pool2;
    private ObjectPool<TextAsset> pool3;
    private ObjectPool<TextAsset> pool4;

    // Path to folders containing dialogues, one folder = one character
    [SerializeField] private string folder1Path;
    [SerializeField] private string folder2Path;
    [SerializeField] private string folder3Path;
    [SerializeField] private string folder4Path;

    // Assets of a specific folder
    private Dictionary<string, TextAsset> folder1Assets = new Dictionary<string, TextAsset>();
    private Dictionary<string, TextAsset> folder2Assets = new Dictionary<string, TextAsset>();
    private Dictionary<string, TextAsset> folder3Assets = new Dictionary<string, TextAsset>();
    private Dictionary<string, TextAsset> folder4Assets = new Dictionary<string, TextAsset>();

    // default values when creating pool
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int defaultMaxSize = 20;

    // Singleton
    private static TextAssetPoolManager instance;
    public static TextAssetPoolManager GetInstance()
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

        // Load text assets from each folder
        LoadTextAssets(folder1Path, folder1Assets);
        LoadTextAssets(folder2Path, folder2Assets);
        LoadTextAssets(folder3Path, folder3Assets);
        LoadTextAssets(folder4Path, folder4Assets);

        // Initialize pools
        // initialize pool 1 with all assets of folder 1
        pool1 = new ObjectPool<TextAsset>(
            createFunc: () => CreateTextAsset(folder1Assets),
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPooledObject,
            defaultCapacity: defaultCapacity,
            maxSize: defaultMaxSize
        );
        pool2 = new ObjectPool<TextAsset>(
            createFunc: () => CreateTextAsset(folder2Assets),
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPooledObject,
            defaultCapacity: defaultCapacity,
            maxSize: defaultMaxSize
        );
        pool3 = new ObjectPool<TextAsset>(
            createFunc: () => CreateTextAsset(folder3Assets),
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPooledObject,
            defaultCapacity: defaultCapacity,
            maxSize: defaultMaxSize
        );
        pool4 = new ObjectPool<TextAsset>(
            createFunc: () => CreateTextAsset(folder4Assets),
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPooledObject,
            defaultCapacity: defaultCapacity,
            maxSize: defaultMaxSize
        );
    }

    private void Start()
    {
        // Créer une liste temporaire pour stocker les références
        List<TextAsset> tempList = new List<TextAsset>();

        // Obtenir plusieurs objets du pool
        for (int i = 0; i < defaultCapacity; i++)
        {
            tempList.Add(pool1.Get());
        }
    }

    private TextAsset CreateTextAsset(Dictionary<string, TextAsset> targetedFolderAssets)
    {
        // Return the first TextAsset found from a folder
        foreach (var asset in targetedFolderAssets.Values)
        {
            return asset;
        }
        return null;
    }

    private void LoadTextAssets(string folderPath, Dictionary<string,  TextAsset> targetedFolderAssets)
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>(folderPath);
        if ( textAssets == null )
        {
            Debug.Log("No TextAsset found in folder : " +  folderPath);
        }
        foreach (TextAsset asset in textAssets)
        {
            targetedFolderAssets[asset.name] = asset;
            Debug.Log($"Chargé: {asset.name} depuis {folderPath}");
        }
    }

    private void OnGetFromPool(TextAsset asset)
    {
        // Method executed to handle logic when a TextAsset is retrieved from the pool
        Debug.Log($"TextAsset {asset.name} obtenu du pool");
    }

    private void OnReleaseToPool(TextAsset asset)
    {
        // Method executed to handle logic when a TextAsset is put back in the pool
        Debug.Log($"TextAsset {asset.name} remis dans le pool");
    }

    private void OnDestroyPooledObject(TextAsset asset)
    {
        // Method executed to handle logic if a TextAsset is to be destroyed
        Debug.Log($"TextAsset {asset.name} détruit");
    }

    // Method to obtain a specific TextAsset from a pool 1
    public TextAsset GetTextAssetFromPool1(string assetName)
    {
        if (folder1Assets.TryGetValue(assetName, out TextAsset asset))
        {
            return pool1.Get();
        }
        return null;
    }

    // Method to obtain all TextAssets from a pool 1
    public TextAsset[] GetTextAssetsFromPool1()
    {
        List<TextAsset> textAssets = new List<TextAsset>();
        foreach (var asset in folder1Assets.Values)
        {
            textAssets.Add(asset);
            // Normally pool.Get() method that executes OnGetFromPool for specific logic
        }

        return textAssets.ToArray();
    }

    // Put back a TextAsset in his pool
    public void ReleaseTextAsset(TextAsset asset, int poolIndex)
    {
        switch (poolIndex)
        {
            case 1:
                pool1.Release(asset);
                break;
            case 2:
                pool2.Release(asset);
                break;
            case 3:
                pool3.Release(asset);
                break;
            case 4:
                pool4.Release(asset);
                break;
        }
    }
}
