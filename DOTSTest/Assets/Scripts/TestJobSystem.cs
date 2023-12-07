using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TestJobSystem : MonoBehaviour
{
    //NativeArray<int> nums;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    nums = new NativeArray<int> (100, Allocator.Persistent);
    //    //nums[0] = 1;

    //    //Debug.Log("主线程：" + System.Threading.Thread.CurrentThread.ManagedThreadId);
    //    MyJob job = new() {temp= nums };
    //    JobHandle jobHandle = job.ScheduleParallel(100,10,default);
    //    jobHandle.Complete();
    //    Test();
    //    //job.Run(100);
    //    //job.Schedule(100,default);
        
    //}

    //void Test()
    //{
    //    for (int i = 0; i < nums.Length; i++)
    //    {
    //        Debug.Log(nums[i]);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

public struct MyJob : IJobFor
{
    public NativeArray<int> temp;
    public void Execute(int index)
    {
        temp[index] = index;
        //Debug.Log("线程：" + System.Threading.Thread.CurrentThread.ManagedThreadId + " index: " + index);
    }
}
