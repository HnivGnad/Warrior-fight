using System;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;

    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.5f;
    [Range(0, 2)]
    public float moveAnimSpeedMutiplier = 1;
}
