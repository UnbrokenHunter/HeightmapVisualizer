
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
		}

		public static IIdentifiable GetObjectById(Guid id) => _objectsById.TryGetValue(id, out var obj) ? obj : null;

		public static List<IIdentifiable> GetObjectsByType<T>() where T : IIdentifiable
		{
			Type type = typeof(T);
			return _objectsByType.TryGetValue(type, out var list) ? list : new List<IIdentifiable>();
		}
	}
}
