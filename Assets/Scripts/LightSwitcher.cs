using UnityEngine;
using UnityEngine.UI;

public class LightSwitcher : MonoBehaviour {
    [SerializeField] private float updateRate = 10f;
    [SerializeField] private float activationRadius = 30f;
    [SerializeField] private Text text;

    private float timer;

    private void Awake() {
        text = GameObject.Find("ActiveLightsCount").GetComponent<Text>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer < 1 / updateRate)
            return;

        UpdateLights();
        timer = 0;
    }

    private void UpdateLights() {
        var activeCount = 0;
        foreach (var light in FindObjectsOfType<Light>()) {
            light.enabled = (light.transform.position - transform.position).sqrMagnitude
                            < activationRadius * activationRadius;
            if (light.enabled)
                activeCount++;
        }

        if (text)
            text.text = $"{activeCount} active lights";
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}