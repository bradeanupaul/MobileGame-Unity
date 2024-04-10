namespace TMPro
{
	public struct GlyphPairKey
	{
		public uint firstGlyphIndex;

		public uint secondGlyphIndex;

		public long key;

		public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			this.firstGlyphIndex = firstGlyphIndex;
			this.secondGlyphIndex = secondGlyphIndex;
			key = (long)(((ulong)secondGlyphIndex << 32) | firstGlyphIndex);
		}

		internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
		{
			firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
			secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
			key = (long)(((ulong)secondGlyphIndex << 32) | firstGlyphIndex);
		}
	}
}
