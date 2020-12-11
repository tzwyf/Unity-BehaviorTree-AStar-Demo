using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShape : Action
{
    public Material playerMa;

    private MeshRenderer renderer;

    public override void OnStart()
    {
        renderer = transform.GetComponent<MeshRenderer>();
        renderer.material = playerMa;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnReset()
    {
        renderer = null;
    }
}
