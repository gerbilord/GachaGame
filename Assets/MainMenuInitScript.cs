using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInitScript : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button openPacksButton;
    [SerializeField] private Button backButton;
    
    void Start()
    {
        openPacksButton.onClick.AddListener(() => SceneLoader.Instance.LoadSceneAdditive(SceneType.PackOpener));
        backButton.onClick.AddListener(QuitGame);
    }
    
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
