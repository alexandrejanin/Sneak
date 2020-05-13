using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float angle;
    [SerializeField] private float duration;

    private bool open;
    private float value;

    public bool Open => open;

    private void Awake() {
        value = transform.localEulerAngles.y;
    }

    private void Update() {
        var targetValue = open ? 1 : 0;

        if (Mathf.Abs(value - targetValue) < float.Epsilon)
            return;

        var diff = Mathf.Sign(targetValue - value) * Time.deltaTime / duration;

        value = Mathf.Clamp01(value + diff);

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            angle * curve.Evaluate(value),
            transform.localEulerAngles.z
        );
    }

    [Button]
    public void Interact(IInteracter interacter) {
        open = !open;

        if (open)
            angle = Mathf.Sign(Vector3.Dot(transform.forward, transform.position - interacter.Position)) *
                    Mathf.Abs(angle);
    }
}