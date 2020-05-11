using UnityEngine;
using UnityEngine.UI;

public class LightLevelBar : MonoBehaviour {
    [SerializeField] private Image bar;
    [SerializeField] private Gradient gradient;

    private PlayerLight playerLight;

    private void Update() {
        if (!playerLight) playerLight = FindObjectOfType<PlayerLight>();
        if (!playerLight) return;

        bar.fillAmount = playerLight.LightLevel;
        bar.color = gradient.Evaluate(playerLight.LightLevel);
    }
}