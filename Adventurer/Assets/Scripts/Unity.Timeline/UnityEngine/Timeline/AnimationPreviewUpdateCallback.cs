using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Experimental.Animations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	internal class AnimationPreviewUpdateCallback : ITimelineEvaluateCallback
	{
		private AnimationPlayableOutput m_Output;

		private PlayableGraph m_Graph;

		private List<IAnimationWindowPreview> m_PreviewComponents;

		public AnimationPreviewUpdateCallback(AnimationPlayableOutput output)
		{
			m_Output = output;
			Playable sourcePlayable = m_Output.GetSourcePlayable();
			if (sourcePlayable.IsValid())
			{
				m_Graph = sourcePlayable.GetGraph();
			}
		}

		public void Evaluate()
		{
			if (!m_Graph.IsValid())
			{
				return;
			}
			if (m_PreviewComponents == null)
			{
				FetchPreviewComponents();
			}
			foreach (IAnimationWindowPreview previewComponent in m_PreviewComponents)
			{
				previewComponent?.UpdatePreviewGraph(m_Graph);
			}
		}

		private void FetchPreviewComponents()
		{
			m_PreviewComponents = new List<IAnimationWindowPreview>();
			Animator target = m_Output.GetTarget();
			if (!(target == null))
			{
				GameObject gameObject = target.gameObject;
				m_PreviewComponents.AddRange(gameObject.GetComponents<IAnimationWindowPreview>());
			}
		}
	}
}
