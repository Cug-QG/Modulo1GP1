using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyShip>().currentHP = 0;
        }
    }
}
