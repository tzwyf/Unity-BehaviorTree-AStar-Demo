using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEmptyRoom : Conditional
{
    private float waitLine = -10;
    private float doorX = -12;
    private float doorZ = -11;
    private SharedBool isAEmpty;
    private SharedBool isBEmpty;

    public override void OnStart()
    {
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log("transform.localEulerAngles.y:" + transform.localEulerAngles.y);
        if(transform.position.z < doorZ)//房间A
        {
            if(transform.position.x <= (waitLine+5))
            {
                isAEmpty = (SharedBool)GlobalVariables.Instance.GetVariable("isRoomAEmpty");
                if (isAEmpty.Value)
                {
                    return TaskStatus.Failure;
                }
                else
                {
                    if (transform.position.x > doorX)
                    {
                        if(transform.localEulerAngles.y < 180)//出门
                        {
                            Debug.Log("出门");
                            return TaskStatus.Failure;
                        }
                        else
                        {
                            Debug.Log("等待");
                            return TaskStatus.Success;
                        }
                    }
                    
                }
            }
        }
        else//房间B
        {
            if (transform.position.x <= (waitLine + 5))
            {
                isBEmpty = (SharedBool)GlobalVariables.Instance.GetVariable("isRoomBEmpty");
                if (isBEmpty.Value)
                {
                    return TaskStatus.Failure;
                }
                else
                {
                    if (transform.position.x > doorX)
                    {
                        if (transform.localEulerAngles.y < 180)//出门
                        {
                            Debug.Log("出门");
                            return TaskStatus.Failure;
                        }
                        else
                        {
                            Debug.Log("等待");
                            return TaskStatus.Success;
                        }
                    }

                }
            }
        }

        return TaskStatus.Failure;

    }

    public override void OnReset()
    {
        
    }
    
}
