using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Room[] roomPrefabs;

    private static MapGenerator instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        GenerateMap();
        Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
    }

    private void GenerateMap() {
        var queue = new Queue<DoorNode>();
        var firstRoom = Instantiate(roomPrefabs[0]);

        foreach (var node in firstRoom.GetNodes()) {
            queue.Enqueue(node);
        }

        var index = 0;

        while (queue.Count > 0) {
            var current = queue.Dequeue();

            if (!current.isCorridor && Random.value < index / 100f) {
                current.doorway.Disable();
                continue;
            }

            var newNodes = TryExpand(current);

            if (newNodes == null)
                current.doorway.Disable();
            else
                foreach (var node in newNodes)
                    queue.Enqueue(node);

            index++;
        }
    }

    private DoorNode[] TryExpand(DoorNode node) {
        for (var i = 0; i < 10; i++) {
            var roomPrefab = RandomRoomPrefab();
            if (node.isCorridor && roomPrefab.IsCorridor)
                continue;

            var doorwayIndex = Random.Range(0, roomPrefab.Doorways.Length);
            var doorway = roomPrefab.Doorways[doorwayIndex];

            var targetRotation = node.doorway.transform.eulerAngles.y + 180;
            var currentRotation = doorway.transform.eulerAngles.y;
            var rotation = Quaternion.Euler(0, targetRotation - currentRotation, 0);

            var doorwayToOrigin = transform.position - doorway.transform.position;
            var origin = node.doorway.transform.position + rotation * doorwayToOrigin;

            var size = Physics.BoxCastNonAlloc(origin + rotation * roomPrefab.Bounds.center, roomPrefab.Bounds.extents,
                -node.doorway.transform.forward, new RaycastHit[1], rotation);

            if (size > 0)
                continue;

            node.doorway.SetActive();
            var room = Instantiate(roomPrefab, origin, rotation);
            room.SetParentDoorway(doorwayIndex);

            return room.GetNodes();
        }

        return null;
    }

    private static Room RandomRoomPrefab() => instance.roomPrefabs[Random.Range(0, instance.roomPrefabs.Length)];
}

public readonly struct DoorNode {
    public readonly Doorway doorway;
    public readonly bool isCorridor;

    public DoorNode(Doorway doorway, bool isCorridor) {
        this.doorway = doorway;
        this.isCorridor = isCorridor;
    }
}