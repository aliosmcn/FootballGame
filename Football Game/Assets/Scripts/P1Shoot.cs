using UnityEngine;

public class P1Shoot : ShootSystem
{
    protected override KeyCode FireKey => KeyCode.A;
    protected override float MinAngle => 0f;
    protected override float MaxAngle => 110f;
}
