using System.Collections.Generic;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	public class TMP_FontAssetUtilities
	{
		private static readonly TMP_FontAssetUtilities s_Instance;

		private static List<int> k_SearchedFontAssets;

		private static bool k_IsFontEngineInitialized;

		public static TMP_FontAssetUtilities instance => s_Instance;

		static TMP_FontAssetUtilities()
		{
			s_Instance = new TMP_FontAssetUtilities();
		}

		public static TMP_Character GetCharacterFromFontAsset(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			if (includeFallbacks)
			{
				if (k_SearchedFontAssets == null)
				{
					k_SearchedFontAssets = new List<int>();
				}
				else
				{
					k_SearchedFontAssets.Clear();
				}
			}
			return GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
		}

		private static TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			isAlternativeTypeface = false;
			TMP_Character value = null;
			bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			if (flag || fontWeight != FontWeight.Regular)
			{
				TMP_FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int num = 4;
				switch (fontWeight)
				{
				case FontWeight.Thin:
					num = 1;
					break;
				case FontWeight.ExtraLight:
					num = 2;
					break;
				case FontWeight.Light:
					num = 3;
					break;
				case FontWeight.Regular:
					num = 4;
					break;
				case FontWeight.Medium:
					num = 5;
					break;
				case FontWeight.SemiBold:
					num = 6;
					break;
				case FontWeight.Bold:
					num = 7;
					break;
				case FontWeight.Heavy:
					num = 8;
					break;
				case FontWeight.Black:
					num = 9;
					break;
				}
				fontAsset = (flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface);
				if (fontAsset != null)
				{
					if (fontAsset.characterLookupTable.TryGetValue(unicode, out value))
					{
						isAlternativeTypeface = true;
						return value;
					}
					if (fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && fontAsset.TryAddCharacterInternal(unicode, out value))
					{
						isAlternativeTypeface = true;
						return value;
					}
				}
			}
			if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out value))
			{
				fontAsset = sourceFontAsset;
				return value;
			}
			if (sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && sourceFontAsset.TryAddCharacterInternal(unicode, out value))
			{
				fontAsset = sourceFontAsset;
				return value;
			}
			if (value == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
			{
				List<TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
				int count = fallbackFontAssetTable.Count;
				if (fallbackFontAssetTable != null && count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						if (value != null)
						{
							break;
						}
						TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[i];
						if (tMP_FontAsset == null)
						{
							continue;
						}
						int instanceID = tMP_FontAsset.GetInstanceID();
						if (!k_SearchedFontAssets.Contains(instanceID))
						{
							k_SearchedFontAssets.Add(instanceID);
							value = GetCharacterFromFontAsset_Internal(unicode, tMP_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
							if (value != null)
							{
								return value;
							}
						}
					}
				}
			}
			return null;
		}

		public static TMP_Character GetCharacterFromFontAssets(uint unicode, List<TMP_FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			isAlternativeTypeface = false;
			if (fontAssets == null || fontAssets.Count == 0)
			{
				fontAsset = null;
				return null;
			}
			if (includeFallbacks)
			{
				if (k_SearchedFontAssets == null)
				{
					k_SearchedFontAssets = new List<int>();
				}
				else
				{
					k_SearchedFontAssets.Clear();
				}
			}
			int count = fontAssets.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(fontAssets[i] == null))
				{
					TMP_Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, fontAssets[i], includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
			}
			fontAsset = null;
			return null;
		}

		private static bool TryGetCharacterFromFontFile(uint unicode, TMP_FontAsset fontAsset, out TMP_Character character)
		{
			character = null;
			if (!k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
			{
				k_IsFontEngineInitialized = true;
			}
			FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
			Glyph value = null;
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (fontAsset.glyphLookupTable.TryGetValue(glyphIndex, out value))
			{
				character = fontAsset.AddCharacter_Internal(unicode, value);
				return true;
			}
			GlyphLoadFlags flags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : ((GlyphLoadFlags)6));
			if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out value))
			{
				character = fontAsset.AddCharacter_Internal(unicode, value);
				return true;
			}
			return false;
		}

		public static bool TryGetGlyphFromFontFile(uint glyphIndex, TMP_FontAsset fontAsset, out Glyph glyph)
		{
			glyph = null;
			if (!k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
			{
				k_IsFontEngineInitialized = true;
			}
			FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
			GlyphLoadFlags flags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : ((GlyphLoadFlags)6));
			if (FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph))
			{
				return true;
			}
			return false;
		}
	}
}
