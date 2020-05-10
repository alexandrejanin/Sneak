using UnityEngine;

public class SceneLoader : MonoBehaviour {
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private int radius = 2;

    private void Start() {
        SpawnRooms();
    }

    private void SpawnRooms() {
        for (var x = -radius; x <= radius; x++)
        for (var z = -radius; z <= radius; z++)
            Instantiate(roomPrefab, new Vector3(x * 10, 0, z * 10), Quaternion.identity);
    }
}