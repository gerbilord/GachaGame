using System.Collections;
using UnityEngine;

public class LoadingScreenAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float slideAnimationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float screenPadding = 50f;
    
    private GameObject loadingScreen;
    private RectTransform loadingScreenRect;
    private Coroutine currentAnimation;
    
    public void Initialize(GameObject loadingScreenObject)
    {
        loadingScreen = loadingScreenObject;
        if (loadingScreen != null)
        {
            loadingScreenRect = loadingScreen.GetComponent<RectTransform>();
            if (loadingScreenRect == null)
            {
                Debug.LogWarning("Loading screen does not have a RectTransform component!");
            }
            else
            {
                ResetToOffscreenPosition();
            }
        }
    }
    
    public void ResetToOffscreenPosition()
    {
        if (loadingScreenRect != null)
        {
            float screenHeight = loadingScreenRect.rect.height;
            loadingScreenRect.anchoredPosition = new Vector2(loadingScreenRect.anchoredPosition.x, screenHeight);
            loadingScreen.SetActive(false);
        }
    }
    
    public IEnumerator ShowLoadingScreen()
    {
        if (loadingScreen == null || loadingScreenRect == null)
        {
            Debug.LogWarning("Loading screen is not configured!");
            yield break;
        }

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        loadingScreen.SetActive(true);
        
        float screenHeight = loadingScreenRect.rect.height + screenPadding;
        Vector2 startPos = new Vector2(loadingScreenRect.anchoredPosition.x, screenHeight);
        Vector2 endPos = new Vector2(loadingScreenRect.anchoredPosition.x, 0);
        
        currentAnimation = StartCoroutine(AnimateLoadingScreen(startPos, endPos));
        yield return currentAnimation;
    }
    
    public IEnumerator HideLoadingScreen()
    {
        if (loadingScreen == null || loadingScreenRect == null)
        {
            yield break;
        }

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        float screenHeight = loadingScreenRect.rect.height + screenPadding;
        Vector2 startPos = new Vector2(loadingScreenRect.anchoredPosition.x, 0);
        Vector2 endPos = new Vector2(loadingScreenRect.anchoredPosition.x, -screenHeight);
        
        currentAnimation = StartCoroutine(AnimateLoadingScreen(startPos, endPos));
        yield return currentAnimation;
        
        loadingScreen.SetActive(false);
    }
    
    private IEnumerator AnimateLoadingScreen(Vector2 startPosition, Vector2 endPosition)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < slideAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / slideAnimationDuration;
            float curveValue = slideAnimationCurve.Evaluate(normalizedTime);
            
            loadingScreenRect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, curveValue);
            
            yield return null;
        }
        
        loadingScreenRect.anchoredPosition = endPosition;
        currentAnimation = null;
    }
    
    public void SetAnimationDuration(float duration)
    {
        slideAnimationDuration = duration;
    }
    
    public void SetAnimationCurve(AnimationCurve curve)
    {
        slideAnimationCurve = curve;
    }
    
    public void SetScreenPadding(float padding)
    {
        screenPadding = padding;
    }
}