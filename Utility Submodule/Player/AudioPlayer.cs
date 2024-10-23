using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Redbean
{
	public class AudioPlayer
	{
		private readonly bool isTemporary;

		public AudioSource AudioSource;
		
		public AudioPlayer()
		{
			AudioSource = AudioSystem.Audio.GetAudioSources().FirstOrDefault(_ => !_.isPlaying);
			if (AudioSource)
				return;

			AudioSource = AudioSystem.Audio.AddAudioSource();
			isTemporary = true;
		}

		public async void Play(AudioClip clip)
		{
			await PlayAudioSource(clip);
			
			if (isTemporary)
				Object.Destroy(AudioSource);
		}
		
		private async Task PlayAudioSource(AudioClip clip)
		{
			AudioSource.clip = clip;
			AudioSource.Play();

			while (AudioSource && AudioSource.isPlaying)
				await Task.Yield();

			if (!AudioSource)
				return;
			
			AudioSource.clip = null;
		}
	}
}