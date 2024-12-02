using UnityEngine;
using UnityEngine.AI;

public class HealItemScript : CoinScript
{    new public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerScript>().TakeDamage(-value);
            Destroy(gameObject);
        }
    }
}
