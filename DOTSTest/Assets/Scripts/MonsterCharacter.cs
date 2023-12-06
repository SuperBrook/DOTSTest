using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MonsterCharacter : MonoBehaviour
{
    public float hp;
 
    public float moveSpeed;
    
    public GameObject bulletObj;

    public float createBulletInterval;

    public class MonsterBak : Baker<MonsterCharacter>
    {
        public override void Bake(MonsterCharacter authoring)
        {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<MonsterData>(entity , new MonsterData {
                hp = authoring.hp,
                bullet = GetEntity(authoring.bulletObj, TransformUsageFlags.Dynamic),
                createBulletInterval = authoring.createBulletInterval,
            });

            AddComponent<MoveData>(entity, new MoveData
            {
                moveSpeed = authoring.moveSpeed,
            }) ;
        }
    }
}
