using System;

namespace TMPro
{
	[Serializable]
	public struct FontAssetCreationSettings
	{
		public string sourceFontFileName;

		public string sourceFontFileGUID;

		public int pointSizeSamplingMode;

		public int pointSize;

		public int padding;

		public int packingMode;

		public int atlasWidth;

		public int atlasHeight;

		public int characterSetSelectionMode;

		public string characterSequence;

		public string referencedFontAssetGUID;

		public string referencedTextAssetGUID;

		public int fontStyle;

		public float fontStyleModifier;

		public int renderMode;

		public bool includeFontFeatures;

		internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			sourceFontFileName = string.Empty;
			this.sourceFontFileGUID = sourceFontFileGUID;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			characterSequence = characterSet;
			characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			referencedFontAssetGUID = string.Empty;
			referencedTextAssetGUID = string.Empty;
			fontStyle = 0;
			fontStyleModifier = 0f;
			includeFontFeatures = false;
		}
	}
}
