using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] ShipSettings shipSettings;
    [SerializeField] List<Transform> firePoints;
    [SerializeField] GunSettings gunSettings;
    [SerializeField] MotorSettings motorSettings;

    private Action OnUpdateAction;

    Gun gun;
    Motor motor;
    private float _currentHP;
    public float currentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            if (_currentHP <= 0) Die();
        }
    }

    private void Start()
    {
        currentHP = shipSettings.HP;

        // Creazione della gun solo se firePoint e gunSettings non sono null
        if (firePoints != null && gunSettings != null)
        {
            gun = new Gun(firePoints, gunSettings, shipSettings.Type);
            OnUpdateAction += gun.Tick; // Aggiungiamo solo se gun esiste
        }

        // Creazione del motore solo se motorSettings non è null
        if (motorSettings != null)
        {
            switch (motorSettings.Type)
            {
                case MotorType.Base:
                    motor = new Motor(transform, motorSettings);
                    break;
                case MotorType.ZigZag:
                    motor = new ZigZagMotor(transform, motorSettings);
                    break;
            }
            OnUpdateAction += motor.Tick; // Aggiungiamo solo se motor esiste
        }
    }

    private void Update()
    {
        OnUpdateAction?.Invoke(); // Esegue solo se ci sono azioni assegnate
    }

    private void Die() 
    {
        StageManager.Instance.livingEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
