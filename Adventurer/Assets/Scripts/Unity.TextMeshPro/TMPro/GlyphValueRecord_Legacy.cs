using System;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	[Serializable]
	public struct GlyphValueRecord_Legacy
	{
		public float xPlacement;

		public float yPlacement;

		public float xAdvance;

		public float yAdvance;

		internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
		{
			xPlacement = valueRecord.xPlacement;
			yPlacement = valueRecord.yPlacement;
			xAdvance = valueRecord.xAdvance;
			yAdvance = valueRecord.yAdvance;
		}

		public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
		{
			GlyphValueRecord_Legacy result = default(GlyphValueRecord_Legacy);
			result.xPlacement = a.xPlacement + b.xPlacement;
			result.yPlacement = a.yPlacement + b.yPlacement;
			result.xAdvance = a.xAdvance + b.xAdvance;
			result.yAdvance = a.yAdvance + b.yAdvance;
			return result;
		}
	}
}
