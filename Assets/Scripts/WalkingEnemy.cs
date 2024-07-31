using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : EnemyBahaviour

{
    public override float MaxDistance => 2f;
    public override float IdleTime => 4;

}
