using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : Action
{
    public int num;

    public override void OnStart()
    {
        Debug.Log("执行了ChangeSize脚本");
    }
    public override TaskStatus OnUpdate()
    {
        switch (num)
        {
            case 0:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case 1:
                transform.localScale = new Vector3(2, 2, 2);
                break;
            case 2:
                transform.localScale = new Vector3(3, 3, 3);
                break;
        }
        return TaskStatus.Success;
    }

    public override void OnReset()
    {
    }
}
