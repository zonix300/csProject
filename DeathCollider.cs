using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.TakeDamage(3);
        }
    }
}
