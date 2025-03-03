using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor
{
    public Motor(Transform ship, MotorSettings motor)
    {
        this.ship = ship;
        this.motor = motor;
    }

    public Transform ship { get; private set; }
    public MotorSettings motor { get; private set; }

    public virtual void Tick()
    {
        ship.Translate(-ship.right * Time.deltaTime * motor.Speed);
    }
}