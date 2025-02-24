using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    float fireCooldown;
    ShipType target;

    public Gun(List<Transform> firePoints, GunSettings gun, ShipType targetType)
    {
        this.firePoints = firePoints;
        this.gun = gun;

        target = targetType == ShipType.Player ? ShipType.Enemy : ShipType.Player;
        fireCooldown = 1f / gun.FireRate;
    }

    public List<Transform> firePoints { get; private set; }
    public GunSettings gun { get; private set; }

    public void Tick() 
    {
        Cooldown();
    }

    private void Cooldown()
    {
        fireCooldown -= Time.deltaTime;

        if (GameManager.Instance.playing && fireCooldown <= 0f)
        {
            foreach (var item in firePoints)
            {
                Shoot(item);
            }
        }
    }

    private void Shoot(Transform firePoint) 
    {
        GameObject projectile = Object.Instantiate(gun.ProjectilePrefab, firePoint.position, firePoint.rotation);
        //GameObject projectile = Object.Instantiate(gun.ProjectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().target = target;
        projectile.GetComponent<Projectile>().shootingType = gun.ShootingType;
        fireCooldown = 1f / gun.FireRate;
    }
}
