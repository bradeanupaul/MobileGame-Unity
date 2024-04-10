using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	[Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		[SerializeField]
		private TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		[SerializeField]
		private TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;

		public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return m_FirstAdjustmentRecord;
			}
			set
			{
				m_FirstAdjustmentRecord = value;
			}
		}

		public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return m_SecondAdjustmentRecord;
			}
			set
			{
				m_SecondAdjustmentRecord = value;
			}
		}

		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return m_FeatureLookupFlags;
			}
			set
			{
				m_FeatureLookupFlags = value;
			}
		}

		public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = firstAdjustmentRecord;
			m_SecondAdjustmentRecord = secondAdjustmentRecord;
		}

		internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
		}
	}
}
