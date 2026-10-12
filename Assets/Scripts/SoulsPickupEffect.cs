using UnityEngine;

public class SoulPickupEffect : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform targetTransform;
    private float duration = 0.4f;
    private float elapsed = 0f;
    private Vector2 startPos;

    public void Init(RectTransform target)
    {
        rectTransform = GetComponent<RectTransform>();
        targetTransform = target;
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        elapsed += Time.unscaledDeltaTime; // unscaled so it still works if timeScale changes
        float t = elapsed / duration;

        rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetTransform.anchoredPosition, t);

        if (t >= 1f)
        {
            if (SoulManager.Instance != null)
            {
                SoulManager.Instance.AddSouls(1);
            }

            Destroy(gameObject);
        }
    }
}