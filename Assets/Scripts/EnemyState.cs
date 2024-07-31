using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState, IMove
{
    [field: SerializeField, Range(0, 10)] public float speed { get; set; }

    protected override void Awake()
    {
        maxHP = 50;
        base.Awake();
    }
}
