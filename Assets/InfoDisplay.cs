using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoDisplay : MonoBehaviour
{
    private TextMeshProUGUI infoText;

    void Awake()
    {
        SetupTextComponent();
    }

    private void SetupTextComponent()
    {
        infoText = GetComponentInChildren<TextMeshProUGUI>();
        if (infoText == null)
        {
            Debug.LogError("InfoDisplay: No TextMeshProUGUI component found on " + gameObject.name);
        }
    }

    public void SetInfoText(string text)
    {
        if (infoText != null)
        {
            infoText.text = text;
        }
    }

    public void ClearInfoText()
    {
        if (infoText != null)
        {
            infoText.text = "";
        }
    }

    public void AppendInfoText(string text)
    {
        if (infoText != null)
        {
            infoText.text += text;
        }
    }
}
