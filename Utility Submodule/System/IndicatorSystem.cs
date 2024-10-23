using System;
using UnityEngine;

namespace Redbean
{
	public class IndicatorScope : IDisposable
	{
		public IndicatorScope()
		{
			if (IndicatorSystem.Indicator)
				IndicatorSystem.Indicator.Active(true);
		}
		
		public void Dispose()
		{
			if (IndicatorSystem.Indicator)
				IndicatorSystem.Indicator.Active(false);
		}
	}
	
	public class IndicatorSystem : MonoBehaviour
	{
		public static IndicatorSystem Indicator;
		private static GameObject target;

		public static void OnInitialize()
		{
			var resource = Resources.Load<GameObject>("Indicator");
			target = Instantiate(resource, SystemParent.Parent);
			target.name = "[Indicator System]";
				
			Indicator = target.AddComponent<IndicatorSystem>();
			Indicator.Active(false);
		}

		public void Active(bool value) => target.SetActive(value);
	}
}