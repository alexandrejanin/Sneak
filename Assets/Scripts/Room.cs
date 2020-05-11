using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] private Bounds bounds;
    [SerializeField] private Doorway[] doorways;

    private Doorway originDoorway;

    public void Generate(int degree) {
        foreach (var d in doorways)
            if (d == originDoorway)
                d.SetActiveWithoutDoor();
            else
                d.TrySpawnRoom(degree);

        name = "Room " + degree;
    }

    public Room TrySpawn(Doorway parent) {
        var doorwayIndex = Random.Range(0, doorways.Length);
        var doorway = doorways[doorwayIndex];

        var targetRotation = parent.transform.eulerAngles.y + 180;
        var currentRotation = doorway.transform.eulerAngles.y;
        var rotation = Quaternion.Euler(0, targetRotation - currentRotation, 0);

        var doorwayToOrigin = transform.position - doorway.transform.position;
        var origin = parent.transform.position + rotation * doorwayToOrigin;

        var hits = new RaycastHit[10];

        var size = Physics.BoxCastNonAlloc(origin + rotation * bounds.center, bounds.extents,
            -parent.transform.forward, hits, rotation);

        for (var index = 0; index < size; index++) {
            var hit = hits[index];
            Debug.Log(hit.collider.gameObject.name);
        }

        if (size > 0)
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