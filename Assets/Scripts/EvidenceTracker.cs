using UnityEngine;

public class EvidenceTracker : MonoBehaviour
{
    private int evidenceRecovered = 0;
    private bool isHoldingEvidence = false;

    [SerializeField] private GameObject EvidenceSlot;

    private string lastHeldEvidence = "";
    private GameObject lastEvidenceUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            depositEvidence();
        }
    }

    public void onPickup(GameObject e) {

        // register slot to avoid picking up others and activate respective evidence slot ui
        isHoldingEvidence = true;

        for (int i = 0; i < EvidenceSlot.transform.childCount; i++) {

            if (EvidenceSlot.transform.GetChild(i).name == e.name) {

                lastHeldEvidence = e.name;
                lastEvidenceUI = EvidenceSlot.transform.GetChild(i).gameObject;
                EvidenceSlot.transform.GetChild(i).gameObject.SetActive(true);

            }
        }
    }

    private void depositEvidence() {

        lastEvidenceUI.SetActive(false);
        isHoldingEvidence = false;
        evidenceRecovered++;

        Debug.Log(evidenceRecovered);

    }

    public bool getIsHolding() {
        return isHoldingEvidence;
    }
}
