using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public struct MonsterData : IComponentData
{
    public float hp;
    public float moveSpeed;

}

public readonly partial struct MonsterAspect : IAspect
{
    public readonly RefRW<MonsterData> monsterData;
 
    public readonly RefRW<LocalTransform> localTransform;

}


public partial struct TestSystem : ISystem
{
    void OnCreate(ref SystemState state) 
    { 
        //Entity monster = state.EntityManager.CreateEntity(typeof(MonsterData),typeof(LocalTransform));
        //state.EntityManager.SetComponentData(monster, new MonsterData()
        //{
        //    hp = 100,
        //});
    }

    void OnUpdate(ref SystemState state)
    {
        float3 dir = new float3(0, 0, 1);
        foreach (MonsterAspect monster in SystemAPI.Query<MonsterAspect>())
        {
            monster.localTransform.ValueRW.Position += dir * SystemAPI.Time.DeltaTime * monster.monsterData.ValueRW.moveSpeed;
            monster.monsterData.ValueRW.hp -= SystemAPI.Time.DeltaTime;
        }
    }
}
