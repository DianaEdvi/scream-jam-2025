using UnityEngine;
using System.Collections;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private AudioSource Riser;
    [SerializeField] private AudioSource Scream;

    [SerializeField] private GameObject killScreen1;
    [SerializeField] private GameObject killScreen2;

    [SerializeField] private GameObject darkScreen;
    private CanvasGroup canv;

    private KillscreenShake shaker;

    private void Start()
    {
        canv = darkScreen.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            StartCoroutine(ActivateKillScreen());
        }
    }

    private IEnumerator ActivateKillScreen() {

        //Play riser and wait for it to resolve
        Riser.Play();
        yield return new WaitForSeconds(Riser.clip.length);

        //randomly pick a kill screen to activate and play scream
        int killScreenChoice = Random.Range(1,3);

        if (killScreenChoice == 1)
        {
            killScreen1.SetActive(true);
            shaker = killScreen1.transform.GetComponentInChildren<KillscreenShake>();
            shaker.StartShake();
        }
        else if(killScreenChoice == 2){
            killScreen2.SetActive(true);
            shaker = killScreen2.transform.GetComponentInChildren<KillscreenShake>();
            shaker.StartShake();
        }

        Scream.Play();

        darkScreen.SetActive(true);
        StartCoroutine(fadeToBlack());

        yield return new WaitForSeconds(3f);

        killScreen1.SetActive(false);
        killScreen2.SetActive(false);
    }

    private IEnumerator fadeToBlack() {

        float counter = 0f;

        while (counter < 3f) {

            counter += Time.deltaTime;
            canv.alpha = Mathf.Lerp(0, 1, counter / 3f);

            yield return null;
        }

    }
}
