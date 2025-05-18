using UnityEngine;

public class PuckPickupTrigger : MonoBehaviour
{
    [SerializeField]
    private Player skater;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Puck") && collider.TryGetComponent(out Puck puck))
            skater.TryPickupPuck(puck);
    }
}
