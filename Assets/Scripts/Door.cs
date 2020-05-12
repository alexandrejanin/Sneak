using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private float closedAngle;
    [SerializeField] private float openAngle = 90;
    [SerializeField] private float duration;

    private float Speed => Mathf.Abs(openAngle - closedAngle) / duration;

    private bool open;

    private void Update() {
        var currentAngle = transform.localEulerAngles.y;
        var targetAngle = open ? openAngle : closedAngle;

        if (Mathf.Abs(currentAngle - targetAngle) < float.Epsilon)
            return;

        var newAngle = currentAngle < targetAngle
            ? Mathf.Min(currentAngle + Speed * Time.deltaTime, targetAngle)
            : Mathf.Max(currentAngle - Speed * Time.deltaTime, targetAngle);

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            newAngle,
            transform.localEulerAngles.z
        );
    }

    [Button]
    public void Interact() {
        StopAllCoroutines();
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