using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Motor/Motor", fileName = "MotorData")]
public class MotorSettings : ScriptableObject
{
    [SerializeField] int speed;
    [SerializeField] MotorType type;

    public int Speed { get { return speed; } }
    public MotorType Type { get { return type; } }
}
