using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] ShipSettings shipSettings;
    [SerializeField] Transform firePoint;
    [SerializeField] GunSettings gunSettings;
    [SerializeField] MotorSettings motorSettings;

    Gun gun;
    Motor motor;
    private float _currentHP;
    public float currentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            if (_currentHP <= 0) Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHP = shipSettings.HP;
        gun = new Gun(firePoint, gunSettings, shipSettings.Type);

        switch (motorSettings.Type)
        {
            case MotorType.Base:
                motor = new Motor(transform, motorSettings);
                break;
            case MotorType.ZigZag:
                motor = new ZigZagMotor(transform, motorSettings);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        motor.Tick();
        gun.Tick();
    }
}
