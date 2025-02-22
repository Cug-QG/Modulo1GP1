using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileSettings settings;
    Vector2 direction;
    public ShipType target;
    public ShootingType shootingType;

    protected virtual void Start()
    {
        SetUp();
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * settings.Speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(target.ToString()))
        {
            ApplyDamage(settings.Damage);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyDamage(float dmg) { print(dmg); }

    void SetUp() 
    {
        switch (shootingType)
        {
            case ShootingType.Classic:
                ChooseDirection();
                break;
            case ShootingType.Aim:
                direction = PlayerShip.Instance.transform.position;
                direction -= (Vector2)transform.position;
                direction.Normalize();
                break;
            default:
                break;
        }
    }

    void ChooseDirection()
    {
        switch (target)
        {
            case ShipType.Enemy:
                direction = transform.right;
                break;
            case ShipType.Player:
                direction = -transform.right;
                break;
            default:
                break;
        }
    }
}