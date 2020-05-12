using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Transform startPoint;
    [SerializeField, Min(0)] private float maxDist;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
            Interact();
    }

    private void Interact() {
        RaycastHit hit;
        if (!Physics.Raycast(startPoint.position, startPoint.forward, out hit, maxDist))
            return;

        var interactable = hit.transform.GetComponent<IInteractable>();
        interactable?.Interact();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawRay(startPoint.position, startPoint.forward * maxDist);
    }
}