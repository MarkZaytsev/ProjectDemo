using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.FrostLib.Aspects
{
    public class AspectedEntity : IDisposable
    {
        private readonly List<Aspect> _aspects;
        private readonly AspectsDependencyResolver _resolver;

        public AspectedEntity(params Aspect[] aspects)
        {
            _aspects = new List<Aspect>(aspects);
            _resolver = new AspectsDependencyResolver(this);

            ResolveAspects();
        }

        private void ResolveAspects() => _resolver.ResolveDependencies(_aspects);

        public T GetAspect<T>() where T : class => _aspects.FirstOrDefault(a => a is T) as T;

        public T[] Filter<T>() => _aspects.Where(a => a is T).Cast<T>().ToArray();

        public void Dispose()
        {
            foreach (var aspect in Filter<IDisposable>())
                aspect.Dispose();
        }

        public void AddAspect(Aspect asp)
        {
            _aspects.Add(asp);
            _resolver.HandleAspectAdded(asp, _aspects);
        }
    }
}