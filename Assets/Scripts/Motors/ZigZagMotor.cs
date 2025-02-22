using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMotor : Motor
{
    private float time = 1.5f;

    public ZigZagMotor(Transform ship, MotorSettings motor) : base(ship, motor) { }

    public override void Tick()
    {
        base.Tick();

        time += Time.deltaTime * ((ZigZagMotorSettings)motor).Frequency;
        float offset = Mathf.Sin(time) * ((ZigZagMotorSettings)motor).Amplitude; // Calcola lo spostamento laterale

        ship.Translate(ship.up * offset * Time.deltaTime);
    }
}