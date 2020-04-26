using System.Collections;
using UnityEngine;

namespace Common.FrostLib.Coroutines
{
    public class RoutineRunner : MonoBehaviour, IRoutineRunner
    {
        public static RoutineRunner Create()
        {
            var instance = new GameObject("RoutineRunner").AddComponent<RoutineRunner>();
            DontDestroyOnLoad(instance.gameObject);
            return instance;
        }

        public Coroutine StartRoutine(IEnumerator routine) => StartCoroutine(routine);

        public void StopRoutine(IEnumerator routine)
        {
            if (routine == null || gameObject == null)
                return;

            StopCoroutine(routine);
        }

        public void StopRoutine(Coroutine routine)
        {
            if(routine == null || gameObject == null)
                return;

            StopCoroutine(routine);
        }

        private void OnDestroy() => StopAllCoroutines();
    }
}