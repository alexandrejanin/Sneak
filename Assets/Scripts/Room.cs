using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField] private Bounds bounds;
    [SerializeField] private Doorway[] doorways;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private bool isCorridor;

    public Bounds Bounds => bounds;
    public Doorway[] Doorways => doorways;
    public Transform[] PatrolPoints => patrolPoints;
    public bool IsCorridor => isCorridor;

    private Doorway parent;

    public DoorNode[] GetNodes() =>
        doorways.Where(d => d != parent).Select(d => new DoorNode(d, isCorridor)).ToArray();

    public void SetParentDoorway(int doorwayIndex) {
        parent = doorways[doorwayIndex];
        parent.SetActiveWithoutDoor();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + bounds.center, bounds.size);
    }
}