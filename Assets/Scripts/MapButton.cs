using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject Map;
    [SerializeField] private Image mapButton;

    private AudioSource mapRustle;

    private bool mapUp = false;

    private Vector2 mapUpPos;
    private Vector2 mapDownPos;

    private void Start()
    {
        mapRustle = GetComponent<AudioSource>();
        mapUpPos = new Vector2(5.7f, -3.3f);
        mapDownPos = new Vector2(5.7f, -8);
    }

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
        mapRustle.Play();

        mapButton.raycastTarget = false;
        mapButton.rectTransform.Rotate(0, 0, 180);

        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            Map.transform.position = Vector2.Lerp(mapDownPos, mapUpPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Map.transform.position = mapUpPos;

        yield return new WaitForSeconds(0.2f);

        mapButton.raycastTarget = true;
    }

    private IEnumerator mapGoDown()
    {
        mapRustle.Play();

        mapButton.raycastTarget = false;
        mapButton.rectTransform.Rotate(0, 0, 180);

        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            Map.transform.position = Vector2.Lerp(mapUpPos, mapDownPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Map.transform.position = mapDownPos;

        yield return new WaitForSeconds(0.2f);

        mapButton.raycastTarget = true;
    }
}
