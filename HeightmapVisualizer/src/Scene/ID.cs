
using HeightmapVisualizer.src.Components;

namespace HeightmapVisualizer.src.Scene
{
	internal interface IIdentifiable
	{
		Guid ID { get; }
	}

	internal static class IDManager
	{
		private static readonly Dictionary<Guid, IIdentifiable> _objectsById = new Dictionary<Guid, IIdentifiable>();
		private static readonly Dictionary<Type, List<IIdentifiable>> _objectsByType = new Dictionary<Type, List<IIdentifiable>>();

		public static void Register(IIdentifiable obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));
			if (_objectsById.ContainsKey(obj.ID)) throw new InvalidOperationException("Object is already registered.");

			_objectsById[obj.ID] = obj;

			Type type = obj.GetType();
			if (!_objectsByType.ContainsKey(type))
			{
				_objectsByType[type] = new List<IIdentifiable>();
			}
			_objectsByType[type].Add(obj);

			Type? parent = type.BaseType;
			while (parent != null) {
                if (!_objectsByType.ContainsKey(parent))
                {
                    _objectsByType[parent] = new List<IIdentifiable>();
                }
                _objectsByType[parent].Add(obj);

				parent = parent.BaseType;
            }
        }

		public static IIdentifiable GetObjectById(Guid id) => _objectsById.TryGetValue(id, out var obj) ? obj : null;

		public static List<T> GetObjectsByType<T>() where T : IIdentifiable
		{
			Type type = typeof(T);
			return _objectsByType.TryGetValue(type, out var list) ? list.Cast<T>().ToList() : new List<T>();
		}

		/// <summary>
		/// Returns all children types as well
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
        public static List<IIdentifiable> DeepGetObjectsByType<T>() where T : IIdentifiable
        {
            Type targetType = typeof(T);
            var results = new List<IIdentifiable>();

            // Iterate over all types in the dictionary
            foreach (var kvp in _objectsByType)
            {
                Type type = kvp.Key;
                if (targetType.IsAssignableFrom(type)) // Checks if `type` is `targetType` or inherits from it
                {
                    results.AddRange(kvp.Value); // Add all instances of this type
                }
            }

            return results;
        }
    }
}
