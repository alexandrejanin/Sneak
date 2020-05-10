using UnityEngine;
using UnityEngine.UI;

public class LightLevelBar : MonoBehaviour {
    [SerializeField] private PlayerLight playerLight;
    [SerializeField] private Image bar;
    [SerializeField] private Gradient gradient;

    private void Update() {
        bar.fillAmount = playerLight.LightLevel;
        bar.color = gradient.Evaluate(playerLight.LightLevel);
    }
}