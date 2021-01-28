using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx.Logging;

namespace Deli
{
	public abstract class ServiceCollection : IEnumerable
	{
		private readonly ManualLogSource _logger;

		protected Dictionary<Type, object> Services { get; } = new();

		protected ServiceCollection(ManualLogSource logger)
		{
			_logger = logger;
		}

		protected void Add(Type type, object service)
		{
			if (Services.ContainsKey(type))
			{
				_logger.LogWarning($"A reader for that type ({type}) already exists.");
				return;
			}

			Services.Add(type, service);
		}

		protected object Get(Type type)
		{
			if (!Services.TryGetValue(type, out var obj))
			{
				throw new KeyNotFoundException($"The reader for that type ({type}) was not found.");
			}

			return obj;
		}

		public Dictionary<Type,object>.ValueCollection.Enumerator GetEnumerator()
		{
			return Services.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}