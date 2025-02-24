using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
            ApplyDamage(collision.gameObject, settings.Damage);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyDamage(GameObject target, float dmg) 
    {
        EnemyShip enemy = target.GetComponent<EnemyShip>();
        if (enemy != null) enemy.currentHP -= dmg;

        PlayerShip player = target.GetComponent<PlayerShip>();
        if (player != null) player.currentHP -= dmg;
    }

    void SetUp() 
    {
        ChooseDirection();
        switch (shootingType)
        {
            case ShootingType.Classic:
                break;
            case ShootingType.Aim:
                Vector2 tempDirection = direction;
                tempDirection = PlayerShip.Instance.transform.position;
                tempDirection -= (Vector2)transform.position;
                tempDirection.Normalize();
                float angle = Mathf.Atan2(tempDirection.y, tempDirection.x) * Mathf.Rad2Deg;
                transform.rotation = angle < 90 && angle > -90? Quaternion.Euler(180, 0, -(180 + angle)) : Quaternion.Euler(0, 0, 180 + angle);

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
                direction = new Vector2(1, 0);
                break;
            case ShipType.Player:
                direction = new Vector2(-1,0);
                break;
            default:
                break;
        }
    }
}