using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMPro
{
	[Serializable]
	public class KerningPair
	{
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		[SerializeField]
		private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		[SerializeField]
		private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		internal static KerningPair empty = new KerningPair(0u, default(GlyphValueRecord_Legacy), 0u, default(GlyphValueRecord_Legacy));

		[SerializeField]
		private bool m_IgnoreSpacingAdjustments;

		public uint firstGlyph
		{
			get
			{
				return m_FirstGlyph;
			}
			set
			{
				m_FirstGlyph = value;
			}
		}

		public GlyphValueRecord_Legacy firstGlyphAdjustments => m_FirstGlyphAdjustments;

		public uint secondGlyph
		{
			get
			{
				return m_SecondGlyph;
			}
			set
			{
				m_SecondGlyph = value;
			}
		}

		public GlyphValueRecord_Legacy secondGlyphAdjustments => m_SecondGlyphAdjustments;

		public bool ignoreSpacingAdjustments => m_IgnoreSpacingAdjustments;

		public KerningPair()
		{
			m_FirstGlyph = 0u;
			m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
			m_SecondGlyph = 0u;
			m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
		}

		public KerningPair(uint left, uint right, float offset)
		{
			firstGlyph = left;
			m_SecondGlyph = right;
			xOffset = offset;
		}

		public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			m_FirstGlyph = firstGlyph;
			m_FirstGlyphAdjustments = firstGlyphAdjustments;
			m_SecondGlyph = secondGlyph;
			m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		internal void ConvertLegacyKerningData()
		{
			m_FirstGlyphAdjustments.xAdvance = xOffset;
		}
	}
}
