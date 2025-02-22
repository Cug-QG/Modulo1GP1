using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ship", fileName = "ShipData")]
public class ShipSettings : ScriptableObject
{
    [SerializeField] int hp;
    [SerializeField] ShipType type;

    public int HP { get { return hp; } }
    public ShipType Type { get { return type; } }
}
