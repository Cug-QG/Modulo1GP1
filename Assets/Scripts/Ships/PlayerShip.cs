using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    private static PlayerShip instance;

    public static PlayerShip Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    private float _currentHP;
    public float currentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            UIManager.Instance.SetHealthBarValue(_currentHP / settings.HP);
            if (_currentHP <= 0) GameManager.Instance.GameOver();
        }
    }
    [SerializeField] ShipSettings settings;
    [SerializeField] int speed;
    [SerializeField] float fireRate;

    [SerializeField] Transform gun;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject beamPrefab;
    bool enableShoot = true;
    float fireCooldown = 0f;
    ShootType currentShootType;
    float _beamStrength = 0;
    float beamStrength
    {
        get => _beamStrength;
        set
        {
            _beamStrength = value;
            UIManager.Instance.SetBeamValue(Mathf.Clamp01(_beamStrength / maxBeamTime));
        }
    }
    float maxBeamTime = 1f;

    private Vector3 _screenBounds;
    private float _playerWidth;
    private float _playerHeight;

    private void Start()
    {
        currentHP = settings.HP;
        // screen limits
        _screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // half of the player sizes
        _playerWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        _playerHeight = GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
    }
    
    void Update()
    {
        Move();
        Shooting();
    }

    private void Move() 
    {
        KeepPlayerInBounds();
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        transform.Translate(transform.up * Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }

    private void KeepPlayerInBounds()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -_screenBounds.x + _playerWidth, _screenBounds.x - _playerWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -_screenBounds.y + _playerHeight, _screenBounds.y - _playerHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
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
        if (GameManager.Instance.playing && enableShoot && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) )
        {
            if (currentShootType == ShootType.Projectile && Input.GetMouseButton(0))
            {
                GameObject projectile = Instantiate(projectilePrefab, gun.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().target = ShipType.Enemy;
                fireCooldown = 1f / fireRate;
                beamStrength = 0;
                //UIManager.Instance.SetBeamValue(Mathf.Clamp01(0));
                enableShoot = false;
            }
            if (currentShootType == ShootType.Beam && Input.GetMouseButton(1))
            {
                beamStrength += Time.deltaTime;
                //UIManager.Instance.SetBeamValue(Mathf.Clamp01(beamStrength / maxBeamTime));
            }
        }
        if (GameManager.Instance.playing && enableShoot && currentShootType == ShootType.Beam && Input.GetMouseButtonUp(1))
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
        beam.GetComponent<Beam>().amplifier = Mathf.Clamp01(beamStrength / maxBeamTime);
        beam.GetComponent<Projectile>().target = ShipType.Enemy;
        //UIManager.Instance.SetBeamValue(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) currentHP = 0;
    }
}