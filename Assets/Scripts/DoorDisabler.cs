using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private Room roomHoldingPlayer;

    public void disableDoors() {

        roomHoldingPlayer = gameController.getRoomHoldingPlayer();

        Door[] doors = roomHoldingPlayer.GetComponentsInChildren<Door>(true);

        foreach (Door door in doors) {
            if (door.tag == "Door") {
                door.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void enableDoors() {

        roomHoldingPlayer = gameController.getRoomHoldingPlayer();

        Door[] doors = roomHoldingPlayer.GetComponentsInChildren<Door>(true);

        foreach (Door door in doors)
        {
            if (door.tag == "Door")
            {
                door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

        }

    }
}
