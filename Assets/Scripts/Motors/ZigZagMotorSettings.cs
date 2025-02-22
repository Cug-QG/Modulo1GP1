using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Motor/ZigZagMotor", fileName = "ZigZagMotorData")]
public class ZigZagMotorSettings : MotorSettings
{
    [SerializeField] float frequency; // Velocità dell'oscillazione
    [SerializeField] float amplitude; // Ampiezza dello zigzag

    public float Frequency { get { return frequency; } }
    public float Amplitude { get { return amplitude; } }
}
