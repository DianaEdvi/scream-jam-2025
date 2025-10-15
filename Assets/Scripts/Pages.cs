using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Pages : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int _currentPageIndex;
    
    /**
     * Flips the page either forward or backwards depending on the int passed
     */
    public void FlipPage(int nextPage)
    {
        // Hide current page
        pages[_currentPageIndex].gameObject.SetActive(false);

        // Calculate next page index
        var nextIndex = _currentPageIndex + nextPage;

        if (nextIndex >= pages.Length && nextIndex < pages.Length) return;
        
        // Show next page
        pages[nextIndex].gameObject.SetActive(true);

        // Update current index
        _currentPageIndex = nextIndex;
    }

    /**
     * Resets the book to page 1
     */
    public void ResetBook()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        _currentPageIndex = 0;
        pages[0].gameObject.SetActive(true);
    }
}