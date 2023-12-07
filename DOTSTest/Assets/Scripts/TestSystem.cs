using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public struct MonsterData : IComponentData
{
    public float hp;

    public Entity bullet;

    public float createBulletTime;

    public float createBulletInterval;
}

public readonly partial struct MonsterAspect : IAspect
{
    public readonly RefRW<MonsterData> monsterData;
 
    public readonly RefRW<LocalTransform> localTransform;
}

public readonly partial struct MoveAspect : IAspect
{
    public readonly RefRW<MoveData> moveData;

    public readonly RefRW<LocalTransform> localTransform;
}

public struct MoveData : IComponentData
{
    public float moveSpeed;
}

//[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct MonsterSystem : ISystem
{
    void OnUpdate(ref SystemState state)
    {
        foreach (MonsterAspect monster in SystemAPI.Query<MonsterAspect>())
        {
            monster.monsterData.ValueRW.hp -= SystemAPI.Time.DeltaTime;
            monster.monsterData.ValueRW.createBulletTime -= SystemAPI.Time.DeltaTime;
            if (monster.monsterData.ValueRO.createBulletTime <= 0)
            {
                monster.monsterData.ValueRW.createBulletTime = monster.monsterData.ValueRO.createBulletInterval;
                Entity entity = state.EntityManager.Instantiate(monster.monsterData.ValueRO.bullet);
                state.EntityManager.SetComponentData<LocalTransform>(entity, new LocalTransform {
                    Position = monster.localTransform.ValueRO.Position,
                    Scale = 1,
                });
            }
        }
    }
}

// [UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial struct MoveSystem : ISystem
{
    void OnUpdate(ref SystemState state)
    {
        float3 dir = new float3(0, 0, 1);
        MoveJob moveJob = new() {
            dir = dir,
            time = SystemAPI.Time.DeltaTime
        };
        moveJob.Schedule();
    }
}

public partial struct MoveJob : IJobEntity
{
    public float3 dir;
    public float time;
    public void Execute(MoveAspect mover)
    {
        mover.localTransform.ValueRW.Position += dir * time * mover.moveData.ValueRW.moveSpeed;
    }
}
