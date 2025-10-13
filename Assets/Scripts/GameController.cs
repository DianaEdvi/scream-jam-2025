using UnityEngine;

public class GameController : MonoBehaviour
{
    // This will be the boss of the player and moth and room objects (and all other objects basically) 
    // Essentially, it will be the one moving the player/mothman from room to room so that those guys dont manage themselves 
    // It's basically the coordinator of the project 

    private Room _roomHoldingPlayer;
    private Room _roomHoldingMothman;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Events.OnInteract += MovePlayerBetweenRooms;
        
        // ========= TEMP ===========
        _roomHoldingPlayer = GameObject.Find("Room").GetComponent<Room>();

    }

    // Update is called once per frame
    void Update()
    {
        // Endgame: if rooms are equal, gameover
    }

    /**
     * Manages the logic for moving the player between the rooms
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
