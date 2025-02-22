using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile
{
    public float amplifier;

    protected override void Start()
    {
        base.Start();
        transform.localScale *= 1 + amplifier;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(target.ToString()))
        {
            ApplyDamage(collision.gameObject, settings.Damage * (1 + amplifier));
        }
    }
}
