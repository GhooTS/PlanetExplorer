using UnityEngine;

public interface ITargetSelector
{
    EnemyData Target { get; }
    float TargetSpeed { get; }
    Vector3 TargetMoveDirection { get; }
}
