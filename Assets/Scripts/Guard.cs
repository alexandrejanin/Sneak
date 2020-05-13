using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IInteracter {
    [SerializeField] private Transform rayStartPoint;
    [SerializeField, Min(0)] private float rayDist = 2f;

    public Vector3 Position => rayStartPoint.position;

    private Transform pointA, pointB;

    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(rayStartPoint.position, rayStartPoint.forward, out hit, rayDist)) {
            var door = hit.transform.GetComponent<Door>();
            if (door && !door.Open)
                door.Interact(this);
        }
    }

    public void SetPatrol(Transform pointA, Transform pointB) {
        this.pointA = pointA;
        this.pointB = pointB;

        agent.SetDestination(pointB.transform.position);
    }
}