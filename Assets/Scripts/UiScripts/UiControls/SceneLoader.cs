using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Core,
    MainMenu,
    CardLibrary,
    PackOpener
}

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SceneManager");
                    instance = go.AddComponent<SceneLoader>();
                    // DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    [Header("Scene References")]
    [SerializeField] private string sceneCoreReference = "Scene_Core";
    [SerializeField] private string sceneMainMenuReference = "Scene_MainMenu";
    [SerializeField] private string sceneCardLibraryReference = "Scene_CardLibrary";
    [SerializeField] private string scenePackOpenerReference = "Scene_PackOpener";

    private string currentAdditiveScene = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneAdditive(SceneType sceneType)
    {
        if (sceneType == SceneType.Core)
        {
            Debug.LogWarning("Cannot load Scene_Core additively - it should always remain loaded!");
            return;
        }
        
        string sceneName = Instance.GetSceneNameFromType(sceneType);
        Instance.StartCoroutine(Instance.LoadSceneAdditiveCoroutine(sceneName));
    }
    
    private string GetSceneNameFromType(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Core:
                return sceneCoreReference;
            case SceneType.MainMenu:
                return sceneMainMenuReference;
            case SceneType.CardLibrary:
                return sceneCardLibraryReference;
            case SceneType.PackOpener:
                return scenePackOpenerReference;
            default:
                Debug.LogError($"Unknown scene type: {sceneType}");
                return string.Empty;
        }
    }

    private IEnumerator LoadSceneAdditiveCoroutine(string newSceneName)
    {
        if (string.IsNullOrEmpty(newSceneName))
        {
            Debug.LogError("Scene name is null or empty!");
            yield break;
        }

        if (newSceneName == sceneCoreReference)
        {
            Debug.LogWarning("Cannot load Scene_Core additively - it should always remain loaded!");
            yield break;
        }

        if (currentAdditiveScene == newSceneName)
        {
            Debug.Log($"Scene {newSceneName} is already loaded.");
            yield break;
        }

        if (!string.IsNullOrEmpty(currentAdditiveScene))
        {
            Debug.Log($"Unloading scene: {currentAdditiveScene}");
            AsyncOperation unloadOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentAdditiveScene);
            
            if (unloadOperation != null)
            {
                while (!unloadOperation.isDone)
                {
                    yield return null;
                }
            }
        }

        Debug.Log($"Loading scene additively: {newSceneName}");
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        
        if (loadOperation == null)
        {
            Debug.LogError($"Failed to load scene: {newSceneName}");
            yield break;
        }

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        currentAdditiveScene = newSceneName;
        
        Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(newSceneName);
        if (loadedScene.IsValid())
        {
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(loadedScene);
        }

        Debug.Log($"Successfully loaded scene: {newSceneName}");
    }

    public string GetCurrentAdditiveScene()
    {
        return Instance.currentAdditiveScene;
    }

    public bool IsSceneLoaded(SceneType sceneType)
    {
        string sceneName = Instance.GetSceneNameFromType(sceneType);
        return Instance.currentAdditiveScene == sceneName;
    }
}