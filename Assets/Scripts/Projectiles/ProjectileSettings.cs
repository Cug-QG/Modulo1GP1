using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Projectile", fileName = "ProjectileData")]
public class ProjectileSettings : ScriptableObject
{
    [SerializeField] int speed;
    [SerializeField] float damage;

    public int Speed { get { return speed; } }
    public float Damage { get { return damage; } }
}
