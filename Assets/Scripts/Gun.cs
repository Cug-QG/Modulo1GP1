using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    float fireCooldown = 0f;

    public Gun(Transform firePoint, GunSettings gun)
    {
        this.firePoint = firePoint;
        this.gun = gun;
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
        fireCooldown = 1f / gun.FireRate;
    }
}
