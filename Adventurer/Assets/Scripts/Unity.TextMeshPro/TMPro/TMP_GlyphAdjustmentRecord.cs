using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	[Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		[SerializeField]
		private uint m_GlyphIndex;

		[SerializeField]
		private TMP_GlyphValueRecord m_GlyphValueRecord;

		public uint glyphIndex
		{
			get
			{
				return m_GlyphIndex;
			}
			set
			{
				m_GlyphIndex = value;
			}
		}

		public TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return m_GlyphValueRecord;
			}
			set
			{
				m_GlyphValueRecord = value;
			}
		}

		public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
		{
			m_GlyphIndex = glyphIndex;
			m_GlyphValueRecord = glyphValueRecord;
		}

		internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
		{
			m_GlyphIndex = adjustmentRecord.glyphIndex;
			m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}
	}
}
