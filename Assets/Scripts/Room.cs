using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    [SerializeField] private Room[] adjacentRooms;
    private AudioSource _enterRoomAudioSource;
    public Action OnRoomEnter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Protect against adding Room as adjacent to itself
        if (adjacentRooms.Any(t => t == this))
        {
            throw new Exception("Can't add a Room as adjacent to itself!");
        }

        _enterRoomAudioSource = GetComponent<AudioSource>();
        OnRoomEnter += PlayEnterSound;
        
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
    
    
}
