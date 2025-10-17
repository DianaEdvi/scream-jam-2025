using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject earlyExitScreen;
    [SerializeField] private GameObject technicalWinScreen;
    [SerializeField] private GameObject allEvidenceScreen;

    [SerializeField] private GameObject gameManager;

    private EvidenceTracker evidenceTracker;

    private void Start()
    {
        evidenceTracker = GameObject.Find("EvidenceTracker").GetComponent<EvidenceTracker>();
        gameManager = GameObject.Find("GameController");
    }

    public void goToEndScreen(bool wasKilled) {

        if (wasKilled)
        {
            blackBackground.SetActive(true);
            hintText.SetActive(true);
            deathScreen.SetActive(true);
        }
        else {

            gameManager.SetActive(false);

            if (evidenceTracker.getRecovered() <= 3) {

                blackBackground.SetActive(true);
                earlyExitScreen.SetActive(true);

            } else if (evidenceTracker.getRecovered() <= 9 && evidenceTracker.getRecovered() > 3) {

                blackBackground.SetActive(true);
                technicalWinScreen.SetActive(true);

            } else if (evidenceTracker.getRecovered() == 10) {

                blackBackground.SetActive(true);
                allEvidenceScreen.SetActive(true);

            }
        }
    }
}
