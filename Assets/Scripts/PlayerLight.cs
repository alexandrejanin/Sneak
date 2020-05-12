using UnityEngine;

public class PlayerLight : MonoBehaviour {
    [SerializeField] private float updateRate = 60;
    [SerializeField] private Transform[] pointsToCheck;

    public float LightLevel { get; private set; }

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        if (timer < 1 / updateRate)
            return;

        UpdateLightLevel();
        timer = 0;
    }

    private void UpdateLightLevel() {
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