using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Evidence : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string evidenceName;

    private SpriteRenderer evidenceSprite;
    private Color hoverColor = new Color(0.65f, 0.65f, 0.65f);
    private Color normalColor = new Color(1, 1, 1);

    private void Start()
    {
        evidenceSprite = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //pass evidence name through to collection event, then destroy (logic for moving item to UI and tracking in player manager is handled in event script?)
        Debug.Log("Collected the " +evidenceName);
        Events.OnInteract?.Invoke(gameObject);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        evidenceSprite.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        evidenceSprite.color = normalColor;
    }
}
