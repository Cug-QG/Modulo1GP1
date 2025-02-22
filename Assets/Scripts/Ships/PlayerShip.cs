using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerShip : MonoBehaviour
{

    private static PlayerShip _instance;

    public static PlayerShip Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerShip>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



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
                GameObject projectile = Instantiate(projectilePrefab, gun.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().target = ShipType.Enemy;
                fireCooldown = 1f / fireRate;
                enableShoot = false;
            }
            if (currentShootType == ShootType.Beam && Input.GetMouseButton(1))
            {
                beamStrength += Time.deltaTime;
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
        beam.GetComponent<Beam>().amplifier = Mathf.Clamp01(beamStrength / 2);
        beam.GetComponent<Projectile>().target = ShipType.Enemy;
    }
}