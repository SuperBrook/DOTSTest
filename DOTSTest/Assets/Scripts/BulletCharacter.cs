using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    public float speed;
    public class BulletBaker : Baker<BulletCharacter>
    {
        public override void Bake(BulletCharacter authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MoveData {
                moveSpeed = authoring.speed,
            });
        }
    }
}
