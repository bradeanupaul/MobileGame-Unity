using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	internal class AnimationOutputWeightProcessor : ITimelineEvaluateCallback
	{
		private struct WeightInfo
		{
			public Playable mixer;

			public Playable parentMixer;

			public int port;
		}

		private AnimationPlayableOutput m_Output;

		private AnimationMotionXToDeltaPlayable m_MotionXPlayable;

		private readonly List<WeightInfo> m_Mixers = new List<WeightInfo>();

		public AnimationOutputWeightProcessor(AnimationPlayableOutput output)
		{
			m_Output = output;
			output.SetWeight(0f);
			FindMixers();
		}

		private void FindMixers()
		{
			Playable sourcePlayable = m_Output.GetSourcePlayable();
			int sourceOutputPort = m_Output.GetSourceOutputPort();
			m_Mixers.Clear();
			FindMixers(sourcePlayable, sourceOutputPort, sourcePlayable.GetInput(sourceOutputPort));
		}

		private void FindMixers(Playable parent, int port, Playable node)
		{
			if (!node.IsValid())
			{
				return;
			}
			Type playableType = node.GetPlayableType();
			if (playableType == typeof(AnimationMixerPlayable) || playableType == typeof(AnimationLayerMixerPlayable))
			{
				int inputCount = node.GetInputCount();
				for (int i = 0; i < inputCount; i++)
				{
					FindMixers(node, i, node.GetInput(i));
				}
				WeightInfo weightInfo = default(WeightInfo);
				weightInfo.parentMixer = parent;
				weightInfo.mixer = node;
				weightInfo.port = port;
				WeightInfo item = weightInfo;
				m_Mixers.Add(item);
			}
			else
			{
				int inputCount2 = node.GetInputCount();
				for (int j = 0; j < inputCount2; j++)
				{
					FindMixers(parent, port, node.GetInput(j));
				}
			}
		}

		public void Evaluate()
		{
			float num = 1f;
			m_Output.SetWeight(1f);
			for (int i = 0; i < m_Mixers.Count; i++)
			{
				WeightInfo weightInfo = m_Mixers[i];
				num = WeightUtility.NormalizeMixer(weightInfo.mixer);
				weightInfo.parentMixer.SetInputWeight(weightInfo.port, num);
			}
			if (Application.isPlaying)
			{
				m_Output.SetWeight(num);
			}
		}
	}
}
