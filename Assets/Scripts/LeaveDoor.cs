using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LeaveDoor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject LeaveMenu;
    [SerializeField] private DoorDisabler disabler;
    [SerializeField] private EndScreen ender;
    private RoomFade fadeEffect;

    private Color hoverColor = new Color(0.65f, 0.65f, 0.65f);
    private Color normalColor = new Color(1, 1, 1);

    private SpriteRenderer doorSprite;

    public void leaveMansion() {
        fadeEffect.fadeOut();
        StartCoroutine(LeaveMansionEndScreen());
    }

    public void stayMansion() {
        LeaveMenu.SetActive(false);
        disabler.enableDoors();
    }

    private void Start()
    {
        doorSprite = gameObject.GetComponent<SpriteRenderer>();
        fadeEffect = GameObject.Find("RoomTransition").GetComponent<RoomFade>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        doorSprite.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        doorSprite.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LeaveMenu.SetActive(true);
        disabler.disableDoors();
    }

    private IEnumerator LeaveMansionEndScreen() {

        yield return new WaitForSeconds(2);
        ender.goToEndScreen(false);

    }
}
