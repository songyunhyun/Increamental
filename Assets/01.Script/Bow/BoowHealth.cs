using System;
using UnityEngine;

public class BoowHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Slime slime = collision.GetComponentInParent<Slime>();
        if (slime == null) return;

        TakeDamage(slime.Damage);
    }

    private void TakeDamage(int damage)
    {
        HPManager.Instance.HealthUpdate(-damage);
    }
}