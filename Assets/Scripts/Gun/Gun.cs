using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    float fireCooldown = 0f;
    ShipType target;

    public Gun(Transform firePoint, GunSettings gun, ShipType targetType)
    {
        this.firePoint = firePoint;
        this.gun = gun;

        target = targetType == ShipType.Player ? ShipType.Enemy : ShipType.Player;
    }

    public Transform firePoint { get; private set; }
    public GunSettings gun { get; private set; }

    public void Tick() 
    {
        Cooldown();
    }

    private void Cooldown()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        Object.Instantiate(gun.ProjectilePrefab, firePoint.position, Quaternion.identity);
        gun.ProjectilePrefab.GetComponent<Projectile>().target = target;
        gun.ProjectilePrefab.GetComponent<Projectile>().shootingType = gun.ShootingType;
        fireCooldown = 1f / gun.FireRate;
    }
}
