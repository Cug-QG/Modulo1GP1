using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile
{
    public float amplifier;

    private void Start()
    {
        transform.localScale *= amplifier;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            ApplyDamage(settings.Damage * amplifier);
        }
    }
}
