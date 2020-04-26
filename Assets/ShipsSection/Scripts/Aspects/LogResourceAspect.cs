using System;
using Common.FrostLib.Aspects;
using Common.FrostLib.Aspects.Attributes;
using UnityEngine;

namespace ShipsSection.Scripts.Aspects
{
    public class LogResourceAspect<T> : Aspect, IDisposable where T : ResourceAspect
    {
        [AspectDependency]
        private IdentityAspect Identity { get; set; }

        [AspectDependency]
        private T Resource { get; set; }

        private readonly string _prefix;

        public LogResourceAspect(string prefix) => _prefix = prefix;

        public override void OnDependencyResolved() => Resource.OnValueChangedSignal.AddListener(Handle);

        private void Handle(float newValue, float diff) =>
            Debug.Log($"[{Identity}] {_prefix}: {newValue - diff} -> {newValue} [{diff}]");

        public void Dispose() => Resource.OnValueChangedSignal.RemoveListener(Handle);
    }
}