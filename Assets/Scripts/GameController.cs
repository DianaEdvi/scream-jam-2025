using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    // This will be the boss of the player and moth and room objects (and all other objects basically) 
    // Essentially, it will be the one moving the player/mothman from room to room so that those guys dont manage themselves 
    // It's basically the coordinator of the project 

    private Room _roomHoldingPlayer;
    private Room _roomHoldingMothman;
    private string[] _gamePhases = { "Searching", "Chasing"};
    private string _gamePhase;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gamePhase = "Searching";
        Debug.Log(_gamePhase);
        
        Events.OnInteract += MovePlayerBetweenRooms;
        Events.onTick += MoveMothmanSearchingPhase;
        Events.onTick += MoveMothmanChasingPhase;
        
        _roomHoldingPlayer = GameObject.Find("EntranceRoom").GetComponent<Room>();
        _roomHoldingMothman = GameObject.Find("ConservatoryRoom").GetComponent<Room>();

        var rooms = _roomHoldingPlayer.GetAdjacentRooms();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // ========== TEMP ===========
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Events.onTick.Invoke();
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            _gamePhase = "Searching";
            Debug.Log(_gamePhase);
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            _gamePhase = "Chasing";
            Debug.Log(_gamePhase);
        }
        
        
        // Endgame: if rooms are equal, gameover
    }

    /**
     * Manages the logic for moving the player between the rooms
     * Subscribed to OnInteract event 
     */
    private void MovePlayerBetweenRooms(GameObject passedDoor)
    {
        // If the invoking gameobject is not a door, don't deal 
        if (!passedDoor.CompareTag("Door")) return;
        
        var door = passedDoor.GetComponent<Door>();
        var nextRoom = door.GetRoomBehindDoor();
        
        // Set all children of current room to inactive 
        foreach (Transform child in _roomHoldingPlayer.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Set all children of next room to active 
        foreach (Transform child in nextRoom.transform)
        {
            child.gameObject.SetActive(true);
        }
        
        // Play audio 
        nextRoom.OnRoomEnter?.Invoke();
        // Assign current room
        _roomHoldingPlayer = nextRoom;
    }

    /**
     * During the searching phase, mothman moves between adjacent rooms randomly
     * Subscribed to OnTick event 
     */
    private void MoveMothmanSearchingPhase()
    {
        if  (_gamePhase != "Searching") return;
        var roomIndex = Random.Range(0, _roomHoldingMothman.GetAdjacentRooms().Length);
        var nextRoom =  _roomHoldingMothman.GetAdjacentRooms()[roomIndex];

        if (nextRoom == _roomHoldingPlayer)
        {
            Events.OnMothmanIsNear?.Invoke();
            return;
        }
        
        Events.OnMothmanIsFar?.Invoke();
        
        _roomHoldingMothman = nextRoom;
        _roomHoldingMothman.OnRoomEnter?.Invoke();
        Debug.Log("Searching phase: Mothman is in " + _roomHoldingMothman.gameObject.name);
    }

    /**
     * During the chasing phase, the mothman chooses the shortest path to the player and makes his way over there
     * When the mothman is in the same room as the player, game over
     * Subscribed to OnTick event
     */
    private void MoveMothmanChasingPhase()
    {
        if   (_gamePhase != "Chasing") return;
        // Calculate shortest path 
        var path = BreadthFirst(_roomHoldingMothman, _roomHoldingPlayer);
        
        // Get next room 
        // This code is ugly and needs to be refractored in the morning 
        var nextRoom = path.ElementAtOrDefault(1);
        if (nextRoom == null) return;
        
        _roomHoldingMothman = nextRoom;
        _roomHoldingMothman.OnRoomEnter?.Invoke();
        
        nextRoom = path.ElementAtOrDefault(2);
        if (nextRoom == _roomHoldingPlayer)
        {
            Events.OnMothmanIsNear?.Invoke();
        }
        else
        {
            Events.OnMothmanIsFar?.Invoke();
        }
        
        // If the player is found, game over
        if (_roomHoldingMothman == _roomHoldingPlayer)
        {
            Debug.Log("Player was found in " + _roomHoldingMothman.gameObject.name);
            Events.OnGameOver?.Invoke();
            return;
        }
        Debug.Log("Chasing phase: Mothman is in " + _roomHoldingMothman.gameObject.name);
    }

    /**
     * Calculates the shortest path between two rooms
     * Stolen from the internet hehe 
     */
    private List<Room> BreadthFirst(Room start, Room goal)
    {
        var queue = new Queue<Room>();
        var visited = new HashSet<Room>();
        var previous = new Dictionary<Room, Room>();
        var rand = new System.Random();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == goal)
            {
                // reconstruct path
                var path = new List<Room>();
                for (var r = goal; r != null; r = previous.ContainsKey(r) ? previous[r] : null)
                    path.Insert(0, r);
                return path;
            }

            // Shuffle neighbors to randomize BFS traversal
            var neighbors = current.GetAdjacentRooms().OrderBy(x => rand.Next()).ToList();

            foreach (var neighbor in neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    previous[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return null; // no path found        
    }

    // Listener(s) for Interact: 
    // Get the tag of the object.
    // if it is a door
        // get the room behind that door
        // "move" player to that room (aka swap the displayed room? deactivate the rest? i dont wanna use new scenes for each room tbh, its a waste of resources)
        // play enter sound of that room (invoke OnRoomEnter for that room) 
    // if it is evidence
        // check if you are already holding evidence 
        // if not, then you can pick it up (destroy sprite? set currently holding evidence) 
    // if it is the table
        // place evidence on the table (can be hidden and just reveal when interact is invoked) 
        // check off that object from the checklist 
    // if it is the book 
        // display tutorial pages 
        
    // much of this logic will be in other scripts as events and will simply be invoked/called here. we love decoupling our code <3
}
