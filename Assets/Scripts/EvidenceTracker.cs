using UnityEngine;

public class EvidenceTracker : MonoBehaviour
{
    private int evidenceRecovered = 0;
    private bool isHoldingEvidence = false;

    [SerializeField] private GameObject EvidenceSlot;

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
        Events.OnChangeGameState("Chasing");

        for (int i = 0; i < EvidenceSlot.transform.childCount; i++) {

            if (EvidenceSlot.transform.GetChild(i).name == e.name)
            {

                EvidenceSlot.transform.GetChild(i).gameObject.SetActive(true);
                lastEvidenceUI = EvidenceSlot.transform.GetChild(i).gameObject;

            }
            else {
                Debug.Log("Did not find");
            }
        }
    }

    private void depositEvidence() {

        lastEvidenceUI.SetActive(false);
        isHoldingEvidence = false;
        Events.OnChangeGameState("Searching");
        evidenceRecovered++;

        Debug.Log("Number of evidence recovered: " + evidenceRecovered);

    }

    public bool getIsHolding() {
        return isHoldingEvidence;
    }
}
