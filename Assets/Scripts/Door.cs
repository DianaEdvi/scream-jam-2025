using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    [SerializeField] private Room roomBehindDoor;
    private Camera _camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Room GetRoomBehindDoor()
    {
        return roomBehindDoor;
    }
}
