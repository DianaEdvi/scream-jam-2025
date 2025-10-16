using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    private AudioSource _enterRoomAudioSource;
    public Action OnRoomEnter;
    public Action OnRoomPlaySound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enterRoomAudioSource = GetComponent<AudioSource>();
        //OnRoomEnter += PlayEnterSound; 

        OnRoomPlaySound += PlayEnterSound;
    }

    private void Update()
    {
        // ======== TEMP ========
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            OnRoomEnter?.Invoke();
        }
    }

    /**
     * Plays the unique audio when entering each room
     * 
     */
    private void PlayEnterSound()
    {
        if (_enterRoomAudioSource != null)
        {
            _enterRoomAudioSource.Play();
        }
        else
        {
            throw new Exception("Enter sound not assigned!");
        }
    }

    /**
     * Returns an array of adjacent Rooms to the current Room
     */
    public Room[] GetAdjacentRooms()
    {
        // Scan the inspector for all doors in this Room.
        var doors = GetComponentsInChildren<Door>(true);
        
        // Assign and return an array of adjacent rooms to this room 
        var adjacentRooms = new Room[doors.Length];
        for (var i = 0; i < doors.Length; i++)
        {
            adjacentRooms[i] = doors[i].GetRoomBehindDoor();
        }
        return adjacentRooms;
    }
    
}
