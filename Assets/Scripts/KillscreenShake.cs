using UnityEngine;
using System.Collections;

public class KillscreenShake : MonoBehaviour
{
    public RectTransform uiElementToShake;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 10f; // How far it shakes

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartShake();
        }
    }

    public void StartShake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector2 originalPosition = uiElementToShake.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;

            uiElementToShake.anchoredPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }

        uiElementToShake.anchoredPosition = originalPosition; // Reset to original position
    }
}
