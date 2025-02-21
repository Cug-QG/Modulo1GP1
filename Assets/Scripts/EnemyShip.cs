using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] ShipSettings shipSettings;
    [SerializeField] Transform firePoint;
    [SerializeField] GunSettings gunSettings;

    Gun gun;

    private void Start()
    {
        gun = new Gun(firePoint, gunSettings);
    }

    private void Update()
    {
        gun.Tick();
    }
}
