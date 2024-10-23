using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Redbean
{
	public class InteractionScope : IDisposable
	{
		public InteractionScope()
		{
			if (InteractionSystem.Interaction)
				InteractionSystem.Interaction.Active(true);
		}
		
		public void Dispose()
		{
			if (InteractionSystem.Interaction)
				InteractionSystem.Interaction.Active(false);
		}
	}
	
	public class InteractionSystem : MonoBehaviour
	{
		public static InteractionSystem Interaction;
		private static GameObject target;

		public static void OnInitialize()
		{
			target = new GameObject("[Interaction System]", 
			                        typeof(EventSystem),
			                        typeof(StandaloneInputModule),
			                        typeof(InteractionSystem));
			target.transform.SetParent(SystemParent.Parent);
				
			Interaction = target.GetComponent<InteractionSystem>();
		}
		
		public void Active(bool value) => target.SetActive(value);
	}
}