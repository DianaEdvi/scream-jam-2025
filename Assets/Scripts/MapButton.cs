using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image Map;
    [SerializeField] private Image mapButton;

    private bool mapUp = false;

    private Vector2 mapUpPos = new Vector2(616, -240);
    private Vector2 mapDownPos = new Vector2(616, -850);

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mapUp)
        {
            StartCoroutine(mapGoDown());
            mapUp = false;
        }
        else
        {
            StartCoroutine(mapGoUp());
            mapUp = true;
        }
    }

    private IEnumerator mapGoUp()
    {
        mapButton.raycastTarget = false;

        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            Map.rectTransform.anchoredPosition = Vector2.Lerp(mapDownPos, mapUpPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Map.rectTransform.anchoredPosition = mapUpPos;

        yield return new WaitForSeconds(0.2f);

        mapButton.raycastTarget = true;
    }

    private IEnumerator mapGoDown()
    {
        mapButton.raycastTarget = false;

        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            Map.rectTransform.anchoredPosition = Vector2.Lerp(mapUpPos, mapDownPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Map.rectTransform.anchoredPosition = mapDownPos;

        yield return new WaitForSeconds(0.2f);

        mapButton.raycastTarget = true;
    }
}
