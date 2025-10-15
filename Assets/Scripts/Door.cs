using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class Door : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Room roomBehindDoor;

    private Color hoverColor = new Color(0.65f, 0.65f, 0.65f);
    private Color normalColor = new Color(1, 1, 1);

    private SpriteRenderer doorSprite;
    private AudioSource doorOpen;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        doorOpen = GameObject.FindGameObjectWithTag("Rooms").GetComponent<AudioSource>();
    }

    /**
     * Returns the room that is behind the current door object
     */
    public Room GetRoomBehindDoor()
    {
        return roomBehindDoor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        doorSprite.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        doorSprite.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        doorOpen.Play();
    }
}
