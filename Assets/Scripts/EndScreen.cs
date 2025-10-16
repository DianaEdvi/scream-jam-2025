using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Animator deathScreen;
    [SerializeField] private Animator earlyExitScreen;
    [SerializeField] private Animator technicalWinScreen;
    [SerializeField] private Animator allEvidenceScreen;

    private EvidenceTracker evidenceTracker;

    private void Start()
    {
        evidenceTracker = GameObject.Find("EvidenceTracker").GetComponent<EvidenceTracker>();
    }

    public void goToEndScreen(bool wasKilled) {

        if (wasKilled)
        {

        }
        else { 
            
            

        }
    }
}
