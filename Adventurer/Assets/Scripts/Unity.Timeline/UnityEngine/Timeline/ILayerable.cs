using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	public interface ILayerable
	{
		Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount);
	}
}
