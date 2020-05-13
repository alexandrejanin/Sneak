using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IInteracter {
    [SerializeField] private Transform startPoint;
    [SerializeField, Min(0)] private float maxDist;

    public Vector3 Position => startPoint.position;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
            TryInteract();
    }

    private void TryInteract() {
        RaycastHit hit;
        if (!Physics.Raycast(startPoint.position, startPoint.forward, out hit, maxDist))
            return;

        var interactable = hit.transform.GetComponent<IInteractable>();
        interactable?.Interact(this);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawRay(startPoint.position, startPoint.forward * maxDist);
    }
}