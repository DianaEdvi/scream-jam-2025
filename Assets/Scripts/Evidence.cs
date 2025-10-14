using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Evidence : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string evidenceName;

    private EvidenceTracker tracker;

    private SpriteRenderer evidenceSprite;
    private Color hoverColor = new Color(0.65f, 0.65f, 0.65f);
    private Color normalColor = new Color(1, 1, 1);

    private void Start()
    {
        evidenceSprite = GetComponent<SpriteRenderer>();
        tracker = GameObject.FindGameObjectWithTag("EvidenceTracker").GetComponent<EvidenceTracker>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!tracker.getIsHolding())
        {
            //pass evidence name through to collection event, then destroy
            Debug.Log("Collected the " + evidenceName);

            tracker.onPickup(gameObject);

            Destroy(gameObject);
        }
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
