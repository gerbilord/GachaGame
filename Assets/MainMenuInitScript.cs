using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInitScript : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button openPacksButton;
    
    void Start()
    {
        openPacksButton.onClick.AddListener(() => SceneLoader.Instance.LoadSceneAdditive(SceneType.PackOpener));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
