using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    [SerializeField] private Room roomBehindDoor;
 
    /**
     * Returns the room that is behind the current door object
     */
    public Room GetRoomBehindDoor()
    {
        return roomBehindDoor;
    }
}
