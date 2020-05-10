using UnityEngine;

public class Doorway : MonoBehaviour {
    [SerializeField] private float roomChance = 0.5f;
    [SerializeField] private GameObject fullWall;
    [SerializeField] private GameObject doorWall;
    [SerializeField] private GameObject door;

    public void SetActiveWithoutDoor() {
        fullWall.SetActive(false);
        doorWall.SetActive(true);
        door.SetActive(false);
    }

    public void TrySpawnRoom(int degree) {
        if (Random.value < roomChance - 0.1f * degree)
            SpawnRoom(degree);
        else
            Disable();
    }

    private void SpawnRoom(int degree) {
        var room = RoomManager.TrySpawnRandomRoom(this);
        if (room == null) {
            Disable();
            return;
        }

        room.Spawn(degree + 1);
        SetActive();
    }

    private void Disable() {
        fullWall.SetActive(true);
        doorWall.SetActive(false);
        door.SetActive(false);
    }

    private void SetActive() {
        fullWall.SetActive(false);
        doorWall.SetActive(true);
        door.SetActive(true);
    }
}