using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MonsterBaker : MonoBehaviour
{
    public float hp;
    public float moveSpeed;
    public class MonsterBak : Baker<MonsterBaker>
    {
        public override void Bake(MonsterBaker authoring)
        {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<MonsterData>(entity , new MonsterData {
                hp = authoring.hp,
                moveSpeed = authoring.moveSpeed,
            });
        }
    }
}
