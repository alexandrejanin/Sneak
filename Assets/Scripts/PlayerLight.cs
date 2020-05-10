using UnityEngine;

public class PlayerLight : MonoBehaviour {
    [SerializeField] private float updateRate = 60;
    [SerializeField] private Transform[] pointsToCheck;

    public float LightLevel { get; private set; }

    private void Awake() {
        InvokeRepeating(nameof(UpdateLight), 0, 1 / updateRate);
    }

    private void UpdateLight() {
        LightLevel = 0;
        foreach (var lightSource in FindObjectsOfType<LightSource>()) {
            if (!lightSource || !lightSource.isActiveAndEnabled) continue;

            foreach (var point in pointsToCheck) {
                var lightLevel = lightSource.LightLevel(point.position);
                if (lightLevel > LightLevel && !Physics.Linecast(lightSource.transform.position, point.position))
                    LightLevel = lightLevel;
            }
        }
    }
}