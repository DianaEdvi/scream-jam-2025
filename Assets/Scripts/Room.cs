using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    private Evidence _evidence;
    [SerializeField] private Room[] adjacentRooms;
    // [SerializeField] private Decoy[] decoys; // Potential add in
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Protect against adding Room as adjacent to itself
        if (adjacentRooms.Any(t => t == this))
        {
            throw new Exception("Can't add a Room as adjacent to itself!");
        }
        
        _evidence = GetComponentInChildren<Evidence>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
