using UnityEngine;
using System.Collections;

public class RoomFade : MonoBehaviour
{
    private CanvasGroup canv;
    private DoorDisabler doorkiller;
    private AudioSource footstepsSound;

    private void Start()
    {
        canv = GetComponent<CanvasGroup>();
        doorkiller = GameObject.Find("DoorDisabler").GetComponent<DoorDisabler>();
        footstepsSound = GetComponent<AudioSource>();
    }

    public void playFootsteps() {
        footstepsSound.Play();
    }

    public void fadeOut() {
        StartCoroutine(fadeOutCoroutine());
    }

    public void fadeIn()
    {
        StartCoroutine(fadeInCoroutine());
    }

    private IEnumerator fadeOutCoroutine()
    {
        doorkiller.disableDoors();
        float counter = 0f;

        while (counter < 1.5f)
        {

            counter += Time.deltaTime;
            canv.alpha = Mathf.Lerp(0, 1, counter / 1.5f);

            yield return null;
        }

        doorkiller.enableDoors();
    }

    private IEnumerator fadeInCoroutine()
    {
        doorkiller.disableDoors();
        float counter = 0f;

        while (counter < 1.5f)
        {

            counter += Time.deltaTime;
            canv.alpha = Mathf.Lerp(1, 0, counter / 1.5f);

            yield return null;
        }

        doorkiller.enableDoors();
    }
}
