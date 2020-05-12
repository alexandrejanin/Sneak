using UnityEngine;

public class Doorway : MonoBehaviour {
    [SerializeField] private GameObject fullWall;
    [SerializeField] private GameObject doorWall;
    [SerializeField] private GameObject door;

    public void Disable() {
        fullWall.SetActive(true);
        doorWall.SetActive(false);
        door.SetActive(false);
    }

    public void SetActive() {
        fullWall.SetActive(false);
        doorWall.SetActive(true);
        door.SetActive(true);
    }

    public void SetActiveWithoutDoor() {
        fullWall.SetActive(false);
        doorWall.SetActive(true);
        door.SetActive(false);
    }
}