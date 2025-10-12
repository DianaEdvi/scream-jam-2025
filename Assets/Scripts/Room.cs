using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    [SerializeField] private Evidence evidence;
    [SerializeField] private Room[] adjacentRooms;
    // [SerializeField] private Decoy[] decoys; // Potential add in
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Protect against adding Room as adjacent to itself
        if (adjacentRooms.Any(t => t == this))
        {
            throw new Exception("Can't add room to itself");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
