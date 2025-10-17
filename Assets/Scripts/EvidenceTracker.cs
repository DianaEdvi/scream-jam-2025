using UnityEngine;
using System.Collections;

public class EvidenceTracker : MonoBehaviour
{
    private int evidenceRecovered = 0;
    private bool isHoldingEvidence = false;

    [SerializeField] private GameObject EvidenceSlot;
    [SerializeField] private GameObject EvidencePickupText;
 
    [SerializeField] private AudioSource evidenceNoise;
    [SerializeField] private AudioSource mothmanNoise;

    private GameObject lastEvidenceUI;

    public void onPickup(GameObject e) {

        StartCoroutine(EvidenceGrabbedText());
        evidenceNoise.Play();

        // register slot to avoid picking up others and activate respective evidence slot ui
        isHoldingEvidence = true;
        Events.OnChangeGameState("Chasing");
        mothmanNoise.Play();

        for (int i = 0; i < EvidenceSlot.transform.childCount; i++) {

            if (EvidenceSlot.transform.GetChild(i).name == e.name)
            {

                EvidenceSlot.transform.GetChild(i).gameObject.SetActive(true);
                lastEvidenceUI = EvidenceSlot.transform.GetChild(i).gameObject;

            }
        }
    }

    public void depositEvidence() {

        evidenceNoise.Play();
        lastEvidenceUI.SetActive(false); // null ref
        isHoldingEvidence = false;
        Events.OnChangeGameState("Searching");
        evidenceRecovered++;
    }

    private IEnumerator EvidenceGrabbedText()
    {

        EvidencePickupText.SetActive(true);

        yield return new WaitForSeconds(3);

        EvidencePickupText.SetActive(false);

    }

    public bool getIsHolding() {
        return isHoldingEvidence;
    }

    public int getRecovered() {
        return evidenceRecovered;
    }
}
