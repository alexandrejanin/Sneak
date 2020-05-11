using UnityEngine;

public class RoomManager : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Room[] roomPrefabs;

    private static RoomManager instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        var room = Instantiate(RandomRoomPrefab());
        room.Spawn(0);
        Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
    }

    private static Room RandomRoomPrefab() => instance.roomPrefabs[Random.Range(0, instance.roomPrefabs.Length)];

    public static Room TrySpawnRandomRoom(Doorway doorway) {
        var roomPrefab = RandomRoomPrefab();
        return roomPrefab.TrySpawn(doorway);
    }
}