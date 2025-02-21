using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gun", fileName = "GunData")]
public class GunSettings : ScriptableObject
{
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectilePrefab;

    public float FireRate { get { return fireRate; } }
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }
}
