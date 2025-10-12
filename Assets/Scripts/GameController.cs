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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Endgame: if rooms are equal, gameover
    }
    
    // Listener(s) for Interact: 
    // Get the tag of the object.
    // if it is a door
        // get the room behind that door
        // "move" player to that room (aka swap the displayed room? deactivate the rest? i dont wanna use new scenes for each room tbh, its a waste of resources)
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
