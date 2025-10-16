using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ActivateBook : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject handBookUI;
    [SerializeField] private Pages pages;
    [SerializeField] private DoorDisabler doorDisabler;
    [SerializeField] private AudioSource pickUpSound;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Reset the book and activate the handbook UI objects
        pages.ResetBook();
        doorDisabler.disableDoors();
        pickUpSound.Play();
        foreach (Transform child in handBookUI.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
}