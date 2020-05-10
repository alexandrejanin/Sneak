using UnityEngine;

public class LightSource : MonoBehaviour {
    [SerializeField, Min(0)] private float minRange = 5f;
    [SerializeField, Min(0)] private float maxRange = 10f;

    public float LightLevel(Vector3 position) {
        var sqrDist = (transform.position - position).sqrMagnitude;
        if (sqrDist > maxRange * maxRange)
            return 0;
        if (sqrDist < minRange * minRange)
            return 1;

        return 1 - Mathf.InverseLerp(minRange * minRange, maxRange * maxRange, sqrDist);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}