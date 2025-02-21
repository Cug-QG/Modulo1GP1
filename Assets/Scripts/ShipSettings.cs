using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ship", fileName = "ShipData")]
public class ShipSettings : ScriptableObject
{
    [SerializeField] int speed;
    [SerializeField] int hp;

    public int Speed { get { return speed; } }
    public int HP { get { return hp; } }
}
