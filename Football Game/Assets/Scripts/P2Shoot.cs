using UnityEngine;

public class P2Shoot : ShootSystem
{
    protected override KeyCode FireKey => KeyCode.L;
    protected override float MinAngle => 70f;
    protected override float MaxAngle => 180f;
}
