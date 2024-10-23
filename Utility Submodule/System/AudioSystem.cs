using UnityEngine;

namespace Redbean
{
	public class AudioSystem : MonoBehaviour
	{
		public static AudioSystem Audio;
		
		public static void OnInitialize()
		{
			var go = new GameObject("[Audio System]", 
			                             typeof(AudioSource),
			                             typeof(AudioSource),
			                             typeof(AudioSource),
			                             typeof(AudioSource),
			                             typeof(AudioSystem));
			go.transform.SetParent(SystemParent.Parent);
			
			Audio = go.GetComponent<AudioSystem>();
		}

		public AudioSource[] GetAudioSources() => 
			gameObject.GetComponents<AudioSource>();
		
		public AudioSource AddAudioSource() => 
			gameObject.AddComponent<AudioSource>();
	}
}