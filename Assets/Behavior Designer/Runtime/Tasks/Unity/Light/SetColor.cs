using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
    [TaskCategory("Unity/Light")]
    [TaskDescription("Sets the color of the light.")]
    public class SetColor : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The color to set")]
        public SharedColor color;

        // cache the light component
        private Light light;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                light = currentGameObject.GetComponent<Light>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (light == null) {
                Debug.LogWarning("Light is null");
                return TaskStatus.Failure;
            }

            light.color = color.Value;
            //Debug.Log("color.Value:" + color.Value);
            //Debug.Log("light.color:" + light.color);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            color = Color.white;
        }
    }
}