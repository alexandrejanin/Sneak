using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;

    private bool open;
    private float value;

    private void Awake() {
        value = transform.localEulerAngles.y;
    }

    private void Update() {
        var targetValue = open ? 1 : 0;

        if (Mathf.Abs(value - targetValue) < float.Epsilon)
            return;

        var diff = Mathf.Sign(targetValue - value) * Time.deltaTime / duration;

        value = Mathf.Clamp01(value + diff);

        var angle = curve.Evaluate(value);

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            angle,
            transform.localEulerAngles.z
        );
    }

    [Button]
    public void Interact(IInteracter interacter) {
        if (open)
            Close();
        else
            Open();
    }

    private void Open() {
        open = true;
    }

    private void Close() {
        open = false;
    }
}