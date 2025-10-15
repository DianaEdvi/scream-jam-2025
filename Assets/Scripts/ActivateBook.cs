using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Book : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject handBookUI;
    [SerializeField] private Pages pages;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Reset the book and activate the handbook UI objects
        pages.ResetBook();
        foreach (Transform child in handBookUI.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
}
