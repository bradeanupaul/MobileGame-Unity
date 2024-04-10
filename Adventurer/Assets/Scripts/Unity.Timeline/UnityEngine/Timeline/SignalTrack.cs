using System;

namespace UnityEngine.Timeline
{
	[Serializable]
	[TrackBindingType(typeof(SignalReceiver))]
	[TrackColor(0.25f, 0.25f, 0.25f)]
	[ExcludeFromPreset]
	public class SignalTrack : MarkerTrack
	{
	}
}
