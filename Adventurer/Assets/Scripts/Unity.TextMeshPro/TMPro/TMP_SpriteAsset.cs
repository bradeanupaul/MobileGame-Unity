using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	public class TMP_SpriteAsset : TMP_Asset
	{
		internal Dictionary<uint, int> m_UnicodeLookup;

		internal Dictionary<int, int> m_NameLookup;

		internal Dictionary<uint, int> m_GlyphIndexLookup;

		[SerializeField]
		private string m_Version;

		public Texture spriteSheet;

		[SerializeField]
		private List<TMP_SpriteCharacter> m_SpriteCharacterTable = new List<TMP_SpriteCharacter>();

		[SerializeField]
		private List<TMP_SpriteGlyph> m_SpriteGlyphTable = new List<TMP_SpriteGlyph>();

		public List<TMP_Sprite> spriteInfoList;

		[SerializeField]
		public List<TMP_SpriteAsset> fallbackSpriteAssets;

		internal bool m_IsSpriteAssetLookupTablesDirty;

		private static List<int> k_searchedSpriteAssets;

		public string version
		{
			get
			{
				return m_Version;
			}
			internal set
			{
				m_Version = value;
			}
		}

		public List<TMP_SpriteCharacter> spriteCharacterTable
		{
			get
			{
				if (m_GlyphIndexLookup == null)
				{
					UpdateLookupTables();
				}
				return m_SpriteCharacterTable;
			}
			internal set
			{
				m_SpriteCharacterTable = value;
			}
		}

		public List<TMP_SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return m_SpriteGlyphTable;
			}
			internal set
			{
				m_SpriteGlyphTable = value;
			}
		}

		private void Awake()
		{
			if (material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeSpriteAsset();
			}
		}

		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Material obj = new Material(Shader.Find("TextMeshPro/Sprite"));
			obj.SetTexture(ShaderUtilities.ID_MainTex, spriteSheet);
			obj.hideFlags = HideFlags.HideInHierarchy;
			return obj;
		}

		public void UpdateLookupTables()
		{
			if (material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeSpriteAsset();
			}
			if (m_GlyphIndexLookup == null)
			{
				m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				m_GlyphIndexLookup.Clear();
			}
			for (int i = 0; i < m_SpriteGlyphTable.Count; i++)
			{
				uint index = m_SpriteGlyphTable[i].index;
				if (!m_GlyphIndexLookup.ContainsKey(index))
				{
					m_GlyphIndexLookup.Add(index, i);
				}
			}
			if (m_NameLookup == null)
			{
				m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				m_NameLookup.Clear();
			}
			if (m_UnicodeLookup == null)
			{
				m_UnicodeLookup = new Dictionary<uint, int>();
			}
			else
			{
				m_UnicodeLookup.Clear();
			}
			for (int j = 0; j < m_SpriteCharacterTable.Count; j++)
			{
				int key = m_SpriteCharacterTable[j].hashCode;
				if (!m_NameLookup.ContainsKey(key))
				{
					m_NameLookup.Add(key, j);
				}
				uint unicode = m_SpriteCharacterTable[j].unicode;
				if (!m_UnicodeLookup.ContainsKey(unicode))
				{
					m_UnicodeLookup.Add(unicode, j);
				}
				uint glyphIndex = m_SpriteCharacterTable[j].glyphIndex;
				if (m_GlyphIndexLookup.TryGetValue(glyphIndex, out var value))
				{
					m_SpriteCharacterTable[j].glyph = m_SpriteGlyphTable[value];
				}
			}
			m_IsSpriteAssetLookupTablesDirty = false;
		}

		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			if (m_NameLookup == null)
			{
				UpdateLookupTables();
			}
			if (m_NameLookup.TryGetValue(hashCode, out var value))
			{
				return value;
			}
			return -1;
		}

		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			if (m_UnicodeLookup == null)
			{
				UpdateLookupTables();
			}
			if (m_UnicodeLookup.TryGetValue(unicode, out var value))
			{
				return value;
			}
			return -1;
		}

		public int GetSpriteIndexFromName(string name)
		{
			if (m_NameLookup == null)
			{
				UpdateLookupTables();
			}
			int simpleHashCode = TMP_TextUtilities.GetSimpleHashCode(name);
			return GetSpriteIndexFromHashcode(simpleHashCode);
		}

		public static TMP_SpriteAsset SearchForSpriteByUnicode(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (k_searchedSpriteAssets == null)
			{
				k_searchedSpriteAssets = new List<int>();
			}
			k_searchedSpriteAssets.Clear();
			int instanceID = spriteAsset.GetInstanceID();
			k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, unicode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(List<TMP_SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tMP_SpriteAsset = spriteAssets[i];
				if (tMP_SpriteAsset == null)
				{
					continue;
				}
				int instanceID = tMP_SpriteAsset.GetInstanceID();
				if (!k_searchedSpriteAssets.Contains(instanceID))
				{
					k_searchedSpriteAssets.Add(instanceID);
					tMP_SpriteAsset = SearchForSpriteByUnicodeInternal(tMP_SpriteAsset, unicode, includeFallbacks, out spriteIndex);
					if (tMP_SpriteAsset != null)
					{
						return tMP_SpriteAsset;
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		public static TMP_SpriteAsset SearchForSpriteByHashCode(TMP_SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (k_searchedSpriteAssets == null)
			{
				k_searchedSpriteAssets = new List<int>();
			}
			k_searchedSpriteAssets.Clear();
			int instanceID = spriteAsset.GetInstanceID();
			k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, includeFallbacks, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return SearchForSpriteByHashCodeInternal(TMP_Settings.defaultSpriteAsset, hashCode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(List<TMP_SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tMP_SpriteAsset = spriteAssets[i];
				if (tMP_SpriteAsset == null)
				{
					continue;
				}
				int instanceID = tMP_SpriteAsset.GetInstanceID();
				if (!k_searchedSpriteAssets.Contains(instanceID))
				{
					k_searchedSpriteAssets.Add(instanceID);
					tMP_SpriteAsset = SearchForSpriteByHashCodeInternal(tMP_SpriteAsset, hashCode, searchFallbacks, out spriteIndex);
					if (tMP_SpriteAsset != null)
					{
						return tMP_SpriteAsset;
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(TMP_SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		public void SortGlyphTable()
		{
			if (m_SpriteGlyphTable != null && m_SpriteGlyphTable.Count != 0)
			{
				m_SpriteGlyphTable = m_SpriteGlyphTable.OrderBy((TMP_SpriteGlyph item) => item.index).ToList();
			}
		}

		internal void SortCharacterTable()
		{
			if (m_SpriteCharacterTable != null && m_SpriteCharacterTable.Count > 0)
			{
				m_SpriteCharacterTable = m_SpriteCharacterTable.OrderBy((TMP_SpriteCharacter c) => c.unicode).ToList();
			}
		}

		internal void SortGlyphAndCharacterTables()
		{
			SortGlyphTable();
			SortCharacterTable();
		}

		private void UpgradeSpriteAsset()
		{
			m_Version = "1.1.0";
			Debug.Log("Upgrading sprite asset [" + base.name + "] to version " + m_Version + ".", this);
			m_SpriteCharacterTable.Clear();
			m_SpriteGlyphTable.Clear();
			for (int i = 0; i < spriteInfoList.Count; i++)
			{
				TMP_Sprite tMP_Sprite = spriteInfoList[i];
				TMP_SpriteGlyph tMP_SpriteGlyph = new TMP_SpriteGlyph();
				tMP_SpriteGlyph.index = (uint)i;
				tMP_SpriteGlyph.sprite = tMP_Sprite.sprite;
				tMP_SpriteGlyph.metrics = new GlyphMetrics(tMP_Sprite.width, tMP_Sprite.height, tMP_Sprite.xOffset, tMP_Sprite.yOffset, tMP_Sprite.xAdvance);
				tMP_SpriteGlyph.glyphRect = new GlyphRect((int)tMP_Sprite.x, (int)tMP_Sprite.y, (int)tMP_Sprite.width, (int)tMP_Sprite.height);
				tMP_SpriteGlyph.scale = 1f;
				tMP_SpriteGlyph.atlasIndex = 0;
				m_SpriteGlyphTable.Add(tMP_SpriteGlyph);
				TMP_SpriteCharacter tMP_SpriteCharacter = new TMP_SpriteCharacter((uint)tMP_Sprite.unicode, tMP_SpriteGlyph);
				tMP_SpriteCharacter.name = tMP_Sprite.name;
				tMP_SpriteCharacter.scale = tMP_Sprite.scale;
				m_SpriteCharacterTable.Add(tMP_SpriteCharacter);
			}
			UpdateLookupTables();
		}
	}
}
