using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Evidence : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string evidenceName;

    [SerializeField]private GameObject denialText;

    private EvidenceTracker tracker;

    private SpriteRenderer evidenceSprite;
    private Color hoverColor = new Color(0.65f, 0.65f, 0.65f);
    private Color normalColor = new Color(1, 1, 1);

    private void Start()
    {
        evidenceSprite = GetComponent<SpriteRenderer>();
        tracker = GameObject.FindGameObjectWithTag("EvidenceTracker").GetComponent<EvidenceTracker>();
        denialText = GameObject.FindGameObjectWithTag("DenialText");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!tracker.getIsHolding())
        {

            //pass evidence name through to collection event, then destroy
            tracker.onPickup(gameObject);

            Destroy(gameObject);
        }
        else {
            StartCoroutine(EvidenceDenied());
        }
    }

    private IEnumerator EvidenceDenied() {

        denialText.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        denialText.transform.GetChild(0).gameObject.SetActive(false);

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
