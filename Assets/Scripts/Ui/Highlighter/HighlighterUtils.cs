
using System.Collections.Generic;
using UnityEngine;

public static class HighlighterUtils
{
        public static void ToggleHighlight(GameObject gameObject, bool toggle)
        {
            gameObject.GetComponent<IHighlighter>().ToggleHighlight(toggle);
        }
}
