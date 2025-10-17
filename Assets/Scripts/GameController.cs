using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using System.Collections;

public class GameController : MonoBehaviour
{
    private Room _roomHoldingPlayer;
    private Room _roomHoldingMothman;
    private string _gamePhase;
    private TickManager _tickManager;
    private EvidenceTracker _evidenceTracker;
    private RoomFade roomFader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Obsolete("Obsolete")]
    void Start()
    {
        _gamePhase = "Searching";
        
        // Netflix, Spotify, Disney++
        Events.OnInteract += MovePlayerWithTransition;
        Events.onTick += MoveMothmanSearchingPhase;
        Events.onTick += MoveMothmanChasingPhase;
        Events.OnChangeGameState += ManageGameState;
        
        _roomHoldingPlayer = GameObject.Find("EntranceRoom").GetComponent<Room>();
        _roomHoldingMothman = GameObject.Find("ConservatoryRoom").GetComponent<Room>();
        roomFader = GameObject.Find("RoomTransition").GetComponent<RoomFade>();

        _tickManager = FindFirstObjectByType<TickManager>();
        _evidenceTracker = FindFirstObjectByType<EvidenceTracker>();
        
        // Make sure every room entry (by player and mothman) triggers a search for mothman 
        var rooms = FindObjectsOfType<Room>();
        foreach (var room in rooms)
        {
            room.OnRoomEnter += WhereIsMothman;
        }
    }

    /**
     * Updates gamestate based on evidence pickup/deposit
     * Invoked in EvidenceTracker 
     */
    private void ManageGameState(string gamePhase)
    {
        _gamePhase = gamePhase == "Searching" ? "Searching" : "Chasing";
    }

    /**
     * Manages the logic for moving the player between the rooms.
     * Subscribed to OnInteract event.
     */

    private void MovePlayerWithTransition(GameObject passedDoor) {
        StartCoroutine(TransitionPlayerCoroutine(passedDoor));

    }

    private IEnumerator TransitionPlayerCoroutine(GameObject passedDoor) {

        if (passedDoor.CompareTag("Door") && passedDoor != null)
        {

            roomFader.fadeOut();
            roomFader.playFootsteps();

            yield return new WaitForSeconds(1.5f);

            MovePlayerBetweenRooms(passedDoor);

            roomFader.fadeIn();

        }
    }

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
        
        // Assign current room and notify the room that it has been entered 
        _roomHoldingPlayer = nextRoom;
        _roomHoldingPlayer.OnRoomEnter?.Invoke(); 
    }

    /**
     * During the searching phase, mothman moves between adjacent rooms randomly.
     * The mothman will never enter the room that the player is in during this phase. 
     * Subscribed to OnTick event.
     */
    private void MoveMothmanSearchingPhase()
    {
        if  (_gamePhase != "Searching") return;

        if (_tickManager.Ticks % 6 != 0) return;
        
        // Choose a random adjacent room and move to it
        var adjacentRooms = _roomHoldingMothman.GetAdjacentRooms();
        var roomIndex = Random.Range(0, adjacentRooms.Length);
        var nextRoom =  adjacentRooms[roomIndex];

        // Prevent mothman from entering room with the player or entrance room
        while (nextRoom == _roomHoldingPlayer || nextRoom.gameObject.name == "EntranceRoom")
        {
            roomIndex = Random.Range(0, adjacentRooms.Length);
            nextRoom =  adjacentRooms[roomIndex];
        
            // Mothman stays in his room if his only path is blocked by the player (prevents infinite loop)
            if (adjacentRooms.Length != 1) continue;
            nextRoom = _roomHoldingMothman;
            break;
        }
        
        // Move mothman to the next room 
        _roomHoldingMothman = nextRoom;
        _roomHoldingMothman.OnRoomEnter?.Invoke();
        _roomHoldingMothman.OnRoomPlaySound?.Invoke();
    }

    /**
     * During the chasing phase, the mothman chooses the shortest path to the player and makes his way over there.
     * Subscribed to OnTick event.
     */
    private void MoveMothmanChasingPhase()
    {
        if   (_gamePhase != "Chasing") return;
        
        if (_tickManager.Ticks % 6 != 0) return;

        // Calculate shortest path 
        var path = BreadthFirst(_roomHoldingMothman, _roomHoldingPlayer);
        
        // Get next room 
        var nextRoom = path.ElementAtOrDefault(1);
        if (nextRoom == null) return;
        
        if (nextRoom.gameObject.name == "EntranceRoom") return;
        
        // Move mothman to the next room 
        _roomHoldingMothman = nextRoom;
        _roomHoldingMothman.OnRoomEnter?.Invoke();
        _roomHoldingMothman.OnRoomPlaySound?.Invoke();
    }

    /**
     * Checks where the Mothman is in relation to the player.
     * Subscribed to each Room's OnRoomEnter event
     */
    private void WhereIsMothman()
    {
        // Calculate the nearest path to player and check if there are no rooms between them
        var path = BreadthFirst(_roomHoldingMothman, _roomHoldingPlayer);
        var roomAudio = _roomHoldingMothman.GetComponent<AudioSource>();

        // Check where mothman is and behave accordingly 
        switch (path.Count)
        {
            case 2:
                Events.OnMothmanIsNear?.Invoke();
                if (roomAudio != null)
                {
                    roomAudio.volume = 1f;
                }
                break;
            case 1:
                // If the Mothman is in the same room as the player, then tis game over 
                Events.OnGameOver?.Invoke();
                break;
            default:
                Events.OnMothmanIsFar?.Invoke();
                if (roomAudio != null)
                {
                    roomAudio.volume = 0.2f;
                }
                break;
        }
    }

    /**
     * Calculates the shortest path between two rooms.
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

    public Room getRoomHoldingPlayer() {
        return _roomHoldingPlayer;
    }
    
    private void OnDestroy()
    {
        Events.OnInteract -= MovePlayerWithTransition;
        Events.onTick -= MoveMothmanSearchingPhase;
        Events.onTick -= MoveMothmanChasingPhase;
        Events.OnChangeGameState -= ManageGameState;

        var rooms = FindObjectsOfType<Room>();
        foreach (var room in rooms)
        {
            room.OnRoomEnter -= WhereIsMothman;
        }
    }
}
