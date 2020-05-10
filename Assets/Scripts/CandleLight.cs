using UnityEngine;

public class CandleLight : MonoBehaviour {
    [SerializeField] private new Transform light;
    [SerializeField] private float radius = 0.01f;
    [SerializeField] private float speed = 1f;

    private Vector3 basePosition;

    private void Awake() {
        basePosition = light.localPosition;
    }

    private void Update() {
        var xOffset = Mathf.PerlinNoise(-0.132f, Time.time * speed);
        var yOffset = Mathf.PerlinNoise(Time.time * speed, 0.93f);
        var zOffset = Mathf.PerlinNoise(-0.777f, Time.time * speed);

        light.localPosition = basePosition + radius * new Vector3(xOffset, yOffset, zOffset);
    }
}