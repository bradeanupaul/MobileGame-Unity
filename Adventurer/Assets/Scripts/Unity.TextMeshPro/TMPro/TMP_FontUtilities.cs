using System.Collections.Generic;

namespace TMPro
{
	public static class TMP_FontUtilities
	{
		private static List<int> k_searchedFontAssets;

		public static TMP_FontAsset SearchForCharacter(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			if (k_searchedFontAssets == null)
			{
				k_searchedFontAssets = new List<int>();
			}
			k_searchedFontAssets.Clear();
			return SearchForCharacterInternal(font, unicode, out character);
		}

		public static TMP_FontAsset SearchForCharacter(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			return SearchForCharacterInternal(fonts, unicode, out character);
		}

		private static TMP_FontAsset SearchForCharacterInternal(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			character = null;
			if (font == null)
			{
				return null;
			}
			if (font.characterLookupTable.TryGetValue(unicode, out character))
			{
				return font;
			}
			if (font.fallbackFontAssetTable != null && font.fallbackFontAssetTable.Count > 0)
			{
				for (int i = 0; i < font.fallbackFontAssetTable.Count; i++)
				{
					if (character != null)
					{
						break;
					}
					TMP_FontAsset tMP_FontAsset = font.fallbackFontAssetTable[i];
					if (tMP_FontAsset == null)
					{
						continue;
					}
					int instanceID = tMP_FontAsset.GetInstanceID();
					if (!k_searchedFontAssets.Contains(instanceID))
					{
						k_searchedFontAssets.Add(instanceID);
						tMP_FontAsset = SearchForCharacterInternal(tMP_FontAsset, unicode, out character);
						if (tMP_FontAsset != null)
						{
							return tMP_FontAsset;
						}
					}
				}
			}
			return null;
		}

		private static TMP_FontAsset SearchForCharacterInternal(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			character = null;
			if (fonts != null && fonts.Count > 0)
			{
				for (int i = 0; i < fonts.Count; i++)
				{
					TMP_FontAsset tMP_FontAsset = SearchForCharacterInternal(fonts[i], unicode, out character);
					if (tMP_FontAsset != null)
					{
						return tMP_FontAsset;
					}
				}
			}
			return null;
		}
	}
}
