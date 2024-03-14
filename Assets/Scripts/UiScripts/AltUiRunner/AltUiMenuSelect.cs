using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AltUiMenuSelect : IAltUiRunner
{
    private TaskCompletionSource<string> _resolver;
    private List<GameObject> _options;

    public void OnOptionClicked(GameObject option)
    {
        GameObject selectMenu = GvUi.ui.GetMenuGameObject();
        selectMenu.SetActive(false);
        
        foreach (GameObject optionText in _options)
        {
            GameObject.Destroy(optionText);
        }

        _resolver.SetResult(option.name);
    }

    public async Task<string> GetUserOptionSelect(List<string> options)
    {
        GameObject selectMenu = GvUi.ui.GetMenuGameObject();
        selectMenu.SetActive(true);
        GameObject optionText = Resources.Load<GameObject>("Prefabs/OptionText");
        _options = new List<GameObject>();
        
        // For each option, create a new optionText object, set the text to the option, and add it to the selectMenu
        foreach (string option in options)
        {
            GameObject optionTextInstance = GameObject.Instantiate(optionText, selectMenu.transform);
            optionTextInstance.GetComponent<TMP_Text>().text = option;
            optionTextInstance.name = option;
            _options.Add(optionTextInstance);
        }
        
        _resolver = new TaskCompletionSource<string>();
        string selectedOption = await _resolver.Task;
        return selectedOption;
    }
}