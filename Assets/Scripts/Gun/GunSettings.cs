using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gun", fileName = "GunData")]
public class GunSettings : ScriptableObject
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectilePrefab;
    [Header("Only for enemies")]
    [SerializeField] ShootingType shootingType;

    public float FireRate { get { return fireRate; } }
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }
    public ShootingType ShootingType { get { return shootingType; } }
}
