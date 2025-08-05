using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_InitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneType sceneToLoadFirst = SceneType.MainMenu;
    void Start()
    {
        SceneLoader.Instance.LoadSceneAdditive(sceneToLoadFirst);
    }
}
