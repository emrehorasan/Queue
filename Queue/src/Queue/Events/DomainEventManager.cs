using System;
using System.Collections.Generic;

namespace Queue.Events
{
    public static class DomainEventManager
    {
        #region Test Only

        [ThreadStatic]
        private static List<Delegate> _actions;

        public static void Register<T>(Action<T> callback) where T : DomainEventBase
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }
        public static void ClearCallbacks()
        {
            _actions = null;
        }

        #endregion
        public static void Raise<TDomainEvent>(TDomainEvent args) where TDomainEvent : class, IDomainEvent, new()
        {
          //if (ContainerManager.Instance.Container != null)
          //    foreach (var handler in ContainerManager.Instance.Container.ResolveAll<IDomainEventHandler<TDomainEvent>>())
          //     handler.Handle(args);
     
          if (_actions != null)
              foreach (var action in _actions)
                  if (action is Action<TDomainEvent> parseAction)
                      parseAction(args);
        }
    } 
}
