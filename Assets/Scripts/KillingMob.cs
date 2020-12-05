using UnityEngine;

public class KillingMob : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Player")
        {
            other.gameObject.GetComponent<Player>().Die();
        }
    }
}
