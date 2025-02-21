using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    //[SerializeField] ShipSettings settings;
    [SerializeField] int speed;
    [SerializeField] float fireRate;

    [SerializeField] Transform gun;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject beamPrefab;
    bool enableShoot = true;
    float fireCooldown = 0f;
    ShootType currentShootType;
    float beamStrength = 0;

    void Update()
    {
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        transform.Translate(transform.up * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        Shooting();
    }

    void Shooting() 
    {
        if (Input.GetMouseButtonDown(0)) currentShootType = ShootType.Projectile;
        if (Input.GetMouseButtonDown(1)) currentShootType = ShootType.Beam;
        Cooldown();
        Shoot();
    }

    private void Cooldown()
    {
        if (enableShoot) return;
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            enableShoot = true;
        }
    }
    
    private void Shoot()
    {
        if (enableShoot && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) )
        {
            if (currentShootType == ShootType.Projectile && Input.GetMouseButton(0))
            {
                Instantiate(projectilePrefab, gun.position, Quaternion.identity);
                fireCooldown = 1f / fireRate;
                enableShoot = false;
            }
            if (currentShootType == ShootType.Beam && Input.GetMouseButton(1))
            {
                beamStrength += 1;
            }
        }
        if (enableShoot && currentShootType == ShootType.Beam && Input.GetMouseButtonUp(1))
        {
            ShootBeam();
            beamStrength = 0;
            fireCooldown = 1f / fireRate;
            enableShoot = false;
        }
    }

    void ShootBeam() 
    {
        GameObject beam = Instantiate(beamPrefab, gun.position, Quaternion.identity);
        beam.GetComponent<Beam>().amplifier = 1 + beamStrength / 200;
    }
}