using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EvidenceTable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EvidenceTracker tracker;
    [SerializeField] private Transform evidenceSlot;

    [SerializeField] private GameObject depositText;

    private string evidenceHeld;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tracker.getIsHolding()) {

            for (int i = 0; i < evidenceSlot.transform.childCount; i++) {

                if (evidenceSlot.transform.GetChild(i).gameObject.activeSelf == true) {
                    evidenceHeld = evidenceSlot.transform.GetChild(i).gameObject.name;
                }
            }

            Debug.Log(evidenceHeld + " deposited");

            for (int i = 0; i < transform.childCount; i++) {

                if (transform.GetChild(i).name == evidenceHeld) {

                    transform.GetChild(i).gameObject.SetActive(true);

                }
            }

            tracker.depositEvidence();
            depositText.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tracker.getIsHolding()) { 
            depositText.SetActive(true);  
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        depositText.SetActive(false);
    }
}
