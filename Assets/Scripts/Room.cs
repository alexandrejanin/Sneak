using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] private Bounds bounds;
    [SerializeField] private Doorway[] doorways;

    private Doorway originDoorway;

    public void Spawn(int degree) {
        foreach (var d in doorways)
            if (d == originDoorway)
                d.SetActiveWithoutDoor();
            else
                d.TrySpawnRoom(degree);

        name = "Room " + degree;
    }

    public Room TrySpawn(Doorway oldDoorway) {
        var doorwayIndex = Random.Range(0, doorways.Length);
        var doorway = doorways[doorwayIndex];

        //todo: fix upside-down rooms
        var rotation = Quaternion.FromToRotation(doorway.transform.forward, -oldDoorway.transform.forward);

        var doorwayToOrigin = transform.position - doorway.transform.position;
        var origin = oldDoorway.transform.position + rotation * doorwayToOrigin;

        var obstacleHit = Physics.BoxCast(
            origin,
            bounds.extents,
            -oldDoorway.transform.forward,
            rotation,
            LayerMask.NameToLayer("Terrain")
        );

        if (obstacleHit)
            return null;

        var room = Instantiate(this, origin, rotation);
        room.originDoorway = room.doorways[doorwayIndex];
        return room;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + bounds.center, bounds.size);
    }
}