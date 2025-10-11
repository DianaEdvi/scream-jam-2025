using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Evidence : MonoBehaviour, IPointerClickHandler
{
    private Image evidenceImage;

    private void Start()
    {
        evidenceImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if not holding evidence, collect evidence and trigger the moth



        //switch statement to make code reusable for every piece of evidence, just add the images as we go and make sure the names line up.
        switch (evidenceImage.sprite.name) {

            case "Square":
                Debug.Log("Collected the square");
                break;

        }

        Destroy(gameObject);
    }

}
