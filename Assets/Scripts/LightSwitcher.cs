using UnityEngine;

public class LightSwitcher : MonoBehaviour {
    [SerializeField] private float updateRate = 10f;
    [SerializeField] private float activationRadius = 30f;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        if (timer < 1 / updateRate)
            return;

        UpdateLights();
        timer = 0;
    }

    private void UpdateLights() {
        foreach (var light in FindObjectsOfType<Light>()) {
            light.enabled = (light.transform.position - transform.position).sqrMagnitude
                            < activationRadius * activationRadius;
        }
    }
}