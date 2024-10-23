using UnityEngine;

namespace Redbean
{
	public class SystemParent
	{
		private static Transform parent;

		public static Transform Parent
		{
			get
			{
				if (parent)
					return parent;

				var go = new GameObject("[System]");
				GameObject.DontDestroyOnLoad(go);

				parent = go.transform;
				return parent;
			}
		}
	}
}