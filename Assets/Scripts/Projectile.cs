using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileSettings settings;

    void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * settings.Speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            ApplyDamage(settings.Damage);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyDamage(float dmg) { print(dmg); }
}