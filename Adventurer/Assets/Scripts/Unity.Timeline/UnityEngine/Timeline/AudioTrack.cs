using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	[Serializable]
	[TrackClipType(typeof(AudioPlayableAsset), false)]
	[TrackBindingType(typeof(AudioSource))]
	[ExcludeFromPreset]
	public class AudioTrack : TrackAsset
	{
		[SerializeField]
		private AudioMixerProperties m_TrackProperties = new AudioMixerProperties();

		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
			}
		}

		public TimelineClip CreateClip(AudioClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			TimelineClip timelineClip = CreateDefaultClip();
			AudioPlayableAsset audioPlayableAsset = timelineClip.asset as AudioPlayableAsset;
			if (audioPlayableAsset != null)
			{
				audioPlayableAsset.clip = clip;
			}
			timelineClip.duration = clip.length;
			timelineClip.displayName = clip.name;
			return timelineClip;
		}

		internal override Playable CompileClips(PlayableGraph graph, GameObject go, IList<TimelineClip> timelineClips, IntervalTree<RuntimeElement> tree)
		{
			AudioMixerPlayable audioMixerPlayable = AudioMixerPlayable.Create(graph, timelineClips.Count);
			if (base.hasCurves)
			{
				audioMixerPlayable.GetHandle().SetScriptInstance(m_TrackProperties.Clone());
			}
			for (int i = 0; i < timelineClips.Count; i++)
			{
				TimelineClip timelineClip = timelineClips[i];
				PlayableAsset playableAsset = timelineClip.asset as PlayableAsset;
				if (playableAsset == null)
				{
					continue;
				}
				float num = 0.1f;
				AudioPlayableAsset audioPlayableAsset = timelineClip.asset as AudioPlayableAsset;
				if (audioPlayableAsset != null)
				{
					num = audioPlayableAsset.bufferingTime;
				}
				Playable playable = playableAsset.CreatePlayable(graph, go);
				if (playable.IsValid())
				{
					if (playable.IsPlayableOfType<AudioClipPlayable>())
					{
						AudioClipPlayable audioClipPlayable = (AudioClipPlayable)playable;
						AudioClipProperties @object = audioClipPlayable.GetHandle().GetObject<AudioClipProperties>();
						audioClipPlayable.SetVolume(Mathf.Clamp01(m_TrackProperties.volume * @object.volume));
						audioClipPlayable.SetStereoPan(Mathf.Clamp(m_TrackProperties.stereoPan, -1f, 1f));
						audioClipPlayable.SetSpatialBlend(Mathf.Clamp01(m_TrackProperties.spatialBlend));
					}
					tree.Add(new ScheduleRuntimeClip(timelineClip, playable, audioMixerPlayable, num));
					graph.Connect(playable, 0, audioMixerPlayable, i);
					playable.SetSpeed(timelineClip.timeScale);
					playable.SetDuration(timelineClip.extrapolatedDuration);
					audioMixerPlayable.SetInputWeight(playable, 1f);
				}
			}
			ConfigureTrackAnimation(tree, go, audioMixerPlayable);
			return audioMixerPlayable;
		}

		private void OnValidate()
		{
			m_TrackProperties.volume = Mathf.Clamp01(m_TrackProperties.volume);
			m_TrackProperties.stereoPan = Mathf.Clamp(m_TrackProperties.stereoPan, -1f, 1f);
			m_TrackProperties.spatialBlend = Mathf.Clamp01(m_TrackProperties.spatialBlend);
		}
	}
}
