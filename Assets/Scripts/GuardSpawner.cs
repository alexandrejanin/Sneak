using System.Linq;
using UnityEngine;

public class GuardSpawner : MonoBehaviour {
    [SerializeField] private Guard guardPrefab;
    [SerializeField, Min(0)] private int guardCount = 10;

    private Transform[] patrolPoints;

    public void SpawnGuards() {
        patrolPoints = FindObjectsOfType<Room>().SelectMany(room => room.PatrolPoints).ToArray();
        for (var i = 0; i < guardCount; i++) {
            SpawnGuard();
        }
    }

    private void SpawnGuard() {
        var pointA = RandomPatrolPoint();
        var pointB = RandomPatrolPoint();

        var guard = Instantiate(guardPrefab, pointA.position, pointA.rotation);

        guard.SetPatrol(pointA, pointB);
    }

    private Transform RandomPatrolPoint() => patrolPoints[Random.Range(0, patrolPoints.Length)];
}