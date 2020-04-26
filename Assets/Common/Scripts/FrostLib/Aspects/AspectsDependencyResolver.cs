using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.FrostLib.Aspects.Attributes;
using UnityEngine.Assertions;

namespace Common.FrostLib.Aspects
{
    public class AspectsDependencyResolver
    {
        private readonly AspectedEntity _entity;
        private IReadOnlyCollection<Aspect> _aspects;

        private readonly Dictionary<Aspect, PropertyInfo[]> _binds = new Dictionary<Aspect, PropertyInfo[]>();

        public AspectsDependencyResolver(AspectedEntity entity) => _entity = entity;

        public void ResolveDependencies(IReadOnlyCollection<Aspect> aspects)
        {
            _aspects = aspects;

            BindProperties();
            ResolveAspects();
            NotifyOnResolved();
        }

        private void BindProperties()
        {
            foreach (var aspect in _aspects)
                _binds.Add(aspect, CollectProperties(aspect.GetType()));
        }

        private static PropertyInfo[] CollectProperties(Type type)
        {
            var infos = new List<PropertyInfo>();
            while (type != typeof(Aspect) && type != null)
            {
                infos.AddRange(type.GetProperties(
                    BindingFlags.Instance
                    | BindingFlags.NonPublic
                    | BindingFlags.Public));

                type = type.BaseType;
            }

            return infos.ToArray();
        }

        private void ResolveAspects()
        {
            foreach (var bind in _binds)
            {
                var aspect = bind.Key;
                var properties = bind.Value;

                Resolve(aspect, properties);
            }
        }

        private void Resolve(Aspect aspect, IEnumerable<PropertyInfo> properties)
        {
            foreach (var prop in properties)
            {
                HandleEntityDependency(aspect, prop);

                var isMandatoryDependency = GetAttribute<AspectDependencyAttribute>(prop);
                var isOptionalDependency = GetAttribute<OptionalAspectDependencyAttribute>(prop);

                if (!isMandatoryDependency && !isOptionalDependency)
                    continue;

                var isArray = prop.PropertyType.IsArray;
                var type = isArray ? prop.PropertyType.GetElementType() : prop.PropertyType;
                var dependencies = _aspects.Where(a =>
                {
                    var t = a.GetType();
                    return t == type || t.GetInterfaces().Contains(type);
                }).ToArray();

                if (isOptionalDependency && dependencies.Length == 0)
                    continue;

                Assert.IsTrue(dependencies.Length > 0,
                    $"No aspect found of type {prop.PropertyType} to inject in {aspect}");

                if (isArray)
                {
                    var typedArray = Array.CreateInstance(type, dependencies.Length);
                    Array.Copy(dependencies, typedArray, dependencies.Length);

                    prop.SetValue(aspect, typedArray, null);
                }
                else
                {
                    prop.SetValue(aspect, dependencies[0], null);
                }
            }
        }

        private void HandleEntityDependency(Aspect aspect, PropertyInfo prop)
        {
            var isEntityDependency = GetAttribute<EntityDependencyAttribute>(prop);
            if (!isEntityDependency)
                return;

            prop.SetValue(aspect, _entity, null);
        }

        public void HandleAspectAdded(Aspect asp, IReadOnlyCollection<Aspect> aspects)
        {
            _aspects = aspects;

            var props = CollectProperties(asp.GetType());
            Resolve(asp, props);
            asp.OnDependencyResolved();
        }

        private void NotifyOnResolved()
        {
            foreach (var aspect in _aspects)
                aspect.OnDependencyResolved();
        }

        private static bool GetAttribute<T>(MemberInfo prop) => Attribute.GetCustomAttribute(prop, typeof(T)) is T;
    }
}