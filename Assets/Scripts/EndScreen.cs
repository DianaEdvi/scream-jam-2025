using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private EvidenceTracker evidenceTracker;

    private void Start()
    {
        evidenceTracker = GameObject.Find("EvidenceTracker").GetComponent<EvidenceTracker>();
    }

    public void goToEndScreen(bool wasKilled) { 
        
        //if killed, display the thing

        //

    }
}
