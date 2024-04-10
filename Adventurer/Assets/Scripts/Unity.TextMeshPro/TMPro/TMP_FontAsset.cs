using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	[Serializable]
	public class TMP_FontAsset : TMP_Asset
	{
		[SerializeField]
		private string m_Version;

		[SerializeField]
		internal string m_SourceFontFileGUID;

		[SerializeField]
		private Font m_SourceFontFile;

		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		[SerializeField]
		private FaceInfo m_FaceInfo;

		[SerializeField]
		private List<Glyph> m_GlyphTable = new List<Glyph>();

		private Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		[SerializeField]
		private List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

		private Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

		private Texture2D m_AtlasTexture;

		[SerializeField]
		private Texture2D[] m_AtlasTextures;

		[SerializeField]
		internal int m_AtlasTextureIndex;

		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		[SerializeField]
		private FaceInfo_Legacy m_fontInfo;

		[SerializeField]
		public Texture2D atlas;

		[SerializeField]
		private int m_AtlasWidth;

		[SerializeField]
		private int m_AtlasHeight;

		[SerializeField]
		private int m_AtlasPadding;

		[SerializeField]
		private GlyphRenderMode m_AtlasRenderMode;

		[SerializeField]
		internal List<TMP_Glyph> m_glyphInfoList;

		[SerializeField]
		[FormerlySerializedAs("m_kerningInfo")]
		internal KerningTable m_KerningTable = new KerningTable();

		[SerializeField]
		private TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

		[SerializeField]
		private List<TMP_FontAsset> fallbackFontAssets;

		[SerializeField]
		public List<TMP_FontAsset> m_FallbackFontAssetTable;

		[SerializeField]
		internal FontAssetCreationSettings m_CreationSettings;

		[SerializeField]
		private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

		[SerializeField]
		private TMP_FontWeightPair[] fontWeights;

		public float normalStyle;

		public float normalSpacingOffset;

		public float boldStyle = 0.75f;

		public float boldSpacing = 7f;

		public byte italicStyle = 35;

		public byte tabSize = 10;

		private byte m_oldTabSize;

		internal bool m_IsFontAssetLookupTablesDirty;

		private List<Glyph> m_GlyphsToPack = new List<Glyph>();

		private List<Glyph> m_GlyphsPacked = new List<Glyph>();

		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		private List<uint> m_GlyphIndexList = new List<uint>();

		private List<TMP_Character> m_CharactersToAdd = new List<TMP_Character>();

		internal static uint[] s_GlyphIndexArray = new uint[16];

		internal static List<uint> s_MissingCharacterList = new List<uint>(16);

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

		public Font sourceFontFile
		{
			get
			{
				return m_SourceFontFile;
			}
			internal set
			{
				m_SourceFontFile = value;
			}
		}

		public AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return m_AtlasPopulationMode;
			}
			set
			{
				m_AtlasPopulationMode = value;
			}
		}

		public FaceInfo faceInfo
		{
			get
			{
				return m_FaceInfo;
			}
			internal set
			{
				m_FaceInfo = value;
			}
		}

		public List<Glyph> glyphTable
		{
			get
			{
				return m_GlyphTable;
			}
			internal set
			{
				m_GlyphTable = value;
			}
		}

		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				if (m_GlyphLookupDictionary == null)
				{
					ReadFontAssetDefinition();
				}
				return m_GlyphLookupDictionary;
			}
		}

		public List<TMP_Character> characterTable
		{
			get
			{
				return m_CharacterTable;
			}
			internal set
			{
				m_CharacterTable = value;
			}
		}

		public Dictionary<uint, TMP_Character> characterLookupTable
		{
			get
			{
				if (m_CharacterLookupDictionary == null)
				{
					ReadFontAssetDefinition();
				}
				return m_CharacterLookupDictionary;
			}
		}

		public Texture2D atlasTexture
		{
			get
			{
				if (m_AtlasTexture == null)
				{
					m_AtlasTexture = atlasTextures[0];
				}
				return m_AtlasTexture;
			}
		}

		public Texture2D[] atlasTextures
		{
			get
			{
				_ = m_AtlasTextures;
				return m_AtlasTextures;
			}
			set
			{
				m_AtlasTextures = value;
			}
		}

		internal List<GlyphRect> usedGlyphRects
		{
			get
			{
				return m_UsedGlyphRects;
			}
			set
			{
				m_UsedGlyphRects = value;
			}
		}

		internal List<GlyphRect> freeGlyphRects
		{
			get
			{
				return m_FreeGlyphRects;
			}
			set
			{
				m_FreeGlyphRects = value;
			}
		}

		[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public FaceInfo_Legacy fontInfo => m_fontInfo;

		public int atlasWidth
		{
			get
			{
				return m_AtlasWidth;
			}
			internal set
			{
				m_AtlasWidth = value;
			}
		}

		public int atlasHeight
		{
			get
			{
				return m_AtlasHeight;
			}
			internal set
			{
				m_AtlasHeight = value;
			}
		}

		public int atlasPadding
		{
			get
			{
				return m_AtlasPadding;
			}
			internal set
			{
				m_AtlasPadding = value;
			}
		}

		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return m_AtlasRenderMode;
			}
			internal set
			{
				m_AtlasRenderMode = value;
			}
		}

		public TMP_FontFeatureTable fontFeatureTable
		{
			get
			{
				return m_FontFeatureTable;
			}
			internal set
			{
				m_FontFeatureTable = value;
			}
		}

		public List<TMP_FontAsset> fallbackFontAssetTable
		{
			get
			{
				return m_FallbackFontAssetTable;
			}
			set
			{
				m_FallbackFontAssetTable = value;
			}
		}

		public FontAssetCreationSettings creationSettings
		{
			get
			{
				return m_CreationSettings;
			}
			set
			{
				m_CreationSettings = value;
			}
		}

		public TMP_FontWeightPair[] fontWeightTable
		{
			get
			{
				return m_FontWeightTable;
			}
			internal set
			{
				m_FontWeightTable = value;
			}
		}

		public static TMP_FontAsset CreateFontAsset(Font font)
		{
			return CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024);
		}

		public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic)
		{
			TMP_FontAsset tMP_FontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
			tMP_FontAsset.m_Version = "1.1.0";
			FontEngine.InitializeFontEngine();
			FontEngine.LoadFontFace(font, samplingPointSize);
			tMP_FontAsset.faceInfo = FontEngine.GetFaceInfo();
			if (atlasPopulationMode == AtlasPopulationMode.Dynamic)
			{
				tMP_FontAsset.sourceFontFile = font;
			}
			tMP_FontAsset.atlasPopulationMode = atlasPopulationMode;
			tMP_FontAsset.atlasWidth = atlasWidth;
			tMP_FontAsset.atlasHeight = atlasHeight;
			tMP_FontAsset.atlasPadding = atlasPadding;
			tMP_FontAsset.atlasRenderMode = renderMode;
			tMP_FontAsset.atlasTextures = new Texture2D[1];
			Texture2D texture2D = new Texture2D(0, 0, TextureFormat.Alpha8, mipChain: false);
			tMP_FontAsset.atlasTextures[0] = texture2D;
			int num;
			if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
			{
				num = 0;
				Material material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(ShaderUtilities.ID_TextureWidth, atlasWidth);
				material.SetFloat(ShaderUtilities.ID_TextureHeight, atlasHeight);
				tMP_FontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(ShaderUtilities.ID_TextureWidth, atlasWidth);
				material2.SetFloat(ShaderUtilities.ID_TextureHeight, atlasHeight);
				material2.SetFloat(ShaderUtilities.ID_GradientScale, atlasPadding + num);
				material2.SetFloat(ShaderUtilities.ID_WeightNormal, tMP_FontAsset.normalStyle);
				material2.SetFloat(ShaderUtilities.ID_WeightBold, tMP_FontAsset.boldStyle);
				tMP_FontAsset.material = material2;
			}
			tMP_FontAsset.freeGlyphRects = new List<GlyphRect>
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			tMP_FontAsset.usedGlyphRects = new List<GlyphRect>();
			tMP_FontAsset.ReadFontAssetDefinition();
			return tMP_FontAsset;
		}

		private void Awake()
		{
			if (material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeFontAsset();
			}
		}

		internal void InitializeDictionaryLookupTables()
		{
			if (m_GlyphLookupDictionary == null)
			{
				m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				m_GlyphLookupDictionary.Clear();
			}
			int count = m_GlyphTable.Count;
			if (m_GlyphIndexList == null)
			{
				m_GlyphIndexList = new List<uint>();
			}
			else
			{
				m_GlyphIndexList.Clear();
			}
			for (int i = 0; i < count; i++)
			{
				Glyph glyph = m_GlyphTable[i];
				uint index = glyph.index;
				if (!m_GlyphLookupDictionary.ContainsKey(index))
				{
					m_GlyphLookupDictionary.Add(index, glyph);
					m_GlyphIndexList.Add(index);
				}
			}
			if (m_CharacterLookupDictionary == null)
			{
				m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
			}
			else
			{
				m_CharacterLookupDictionary.Clear();
			}
			for (int j = 0; j < m_CharacterTable.Count; j++)
			{
				TMP_Character tMP_Character = m_CharacterTable[j];
				uint unicode = tMP_Character.unicode;
				uint glyphIndex = tMP_Character.glyphIndex;
				if (!m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					m_CharacterLookupDictionary.Add(unicode, tMP_Character);
				}
				if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (m_KerningTable != null && m_KerningTable.kerningPairs != null && m_KerningTable.kerningPairs.Count > 0)
			{
				UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary == null)
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<long, TMP_GlyphPairAdjustmentRecord>();
			}
			else
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Clear();
			}
			List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords != null)
			{
				for (int k = 0; k < glyphPairAdjustmentRecords.Count; k++)
				{
					TMP_GlyphPairAdjustmentRecord tMP_GlyphPairAdjustmentRecord = glyphPairAdjustmentRecords[k];
					long key = new GlyphPairKey(tMP_GlyphPairAdjustmentRecord).key;
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tMP_GlyphPairAdjustmentRecord);
				}
			}
		}

		public void ReadFontAssetDefinition()
		{
			if (material != null && string.IsNullOrEmpty(m_Version))
			{
				UpgradeFontAsset();
			}
			InitializeDictionaryLookupTables();
			if (!m_CharacterLookupDictionary.ContainsKey(9u))
			{
				Glyph glyph = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, m_FaceInfo.tabWidth * (float)(int)tabSize), GlyphRect.zero, 1f, 0);
				m_CharacterLookupDictionary.Add(9u, new TMP_Character(9u, glyph));
			}
			if (!m_CharacterLookupDictionary.ContainsKey(10u))
			{
				Glyph glyph2 = new Glyph(0u, new GlyphMetrics(10f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				m_CharacterLookupDictionary.Add(10u, new TMP_Character(10u, glyph2));
				if (!m_CharacterLookupDictionary.ContainsKey(13u))
				{
					m_CharacterLookupDictionary.Add(13u, new TMP_Character(13u, glyph2));
				}
			}
			if (!m_CharacterLookupDictionary.ContainsKey(8203u))
			{
				Glyph glyph3 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				m_CharacterLookupDictionary.Add(8203u, new TMP_Character(8203u, glyph3));
			}
			if (!m_CharacterLookupDictionary.ContainsKey(8288u))
			{
				Glyph glyph4 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				m_CharacterLookupDictionary.Add(8288u, new TMP_Character(8288u, glyph4));
			}
			if (!m_CharacterLookupDictionary.ContainsKey(8209u) && m_CharacterLookupDictionary.TryGetValue(45u, out var value))
			{
				m_CharacterLookupDictionary.Add(8209u, new TMP_Character(8209u, value.glyph));
			}
			if (m_FaceInfo.capLine == 0f && m_CharacterLookupDictionary.ContainsKey(72u))
			{
				uint glyphIndex = m_CharacterLookupDictionary[72u].glyphIndex;
				m_FaceInfo.capLine = m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (m_FaceInfo.scale == 0f)
			{
				m_FaceInfo.scale = 1f;
			}
			if (m_FaceInfo.strikethroughOffset == 0f)
			{
				m_FaceInfo.strikethroughOffset = m_FaceInfo.capLine / 2.5f;
			}
			if (m_AtlasPadding == 0 && material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				m_AtlasPadding = (int)material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
			}
			hashCode = TMP_TextUtilities.GetSimpleHashCode(base.name);
			materialHashCode = TMP_TextUtilities.GetSimpleHashCode(material.name);
			m_IsFontAssetLookupTablesDirty = false;
		}

		internal void SortCharacterTable()
		{
			if (m_CharacterTable != null && m_CharacterTable.Count > 0)
			{
				m_CharacterTable = m_CharacterTable.OrderBy((TMP_Character c) => c.unicode).ToList();
			}
		}

		internal void SortGlyphTable()
		{
			if (m_GlyphTable != null && m_GlyphTable.Count > 0)
			{
				m_GlyphTable = m_GlyphTable.OrderBy((Glyph c) => c.index).ToList();
			}
		}

		internal void SortGlyphAndCharacterTables()
		{
			SortGlyphTable();
			SortCharacterTable();
		}

		public bool HasCharacter(int character)
		{
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
			if (m_CharacterLookupDictionary.ContainsKey((uint)character))
			{
				return true;
			}
			return false;
		}

		public bool HasCharacter(char character)
		{
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
			if (m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			return false;
		}

		public bool HasCharacter(char character, bool searchFallbacks)
		{
			if (m_CharacterLookupDictionary == null)
			{
				ReadFontAssetDefinition();
				if (m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			if (m_AtlasPopulationMode == AtlasPopulationMode.Dynamic && TryAddCharacterInternal(character, out var _))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
				{
					for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
					{
						if (fallbackFontAssetTable[i].HasCharacter_Internal(character, searchFallbacks))
						{
							return true;
						}
					}
				}
				if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					for (int j = 0; j < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[j] != null; j++)
					{
						if (TMP_Settings.fallbackFontAssets[j].m_CharacterLookupDictionary == null)
						{
							TMP_Settings.fallbackFontAssets[j].ReadFontAssetDefinition();
						}
						if (TMP_Settings.fallbackFontAssets[j].m_CharacterLookupDictionary != null && TMP_Settings.fallbackFontAssets[j].HasCharacter_Internal(character, searchFallbacks))
						{
							return true;
						}
					}
				}
				if (TMP_Settings.defaultFontAsset != null)
				{
					if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary == null)
					{
						TMP_Settings.defaultFontAsset.ReadFontAssetDefinition();
					}
					if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary != null && TMP_Settings.defaultFontAsset.HasCharacter_Internal(character, searchFallbacks))
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool HasCharacter_Internal(char character, bool searchFallbacks)
		{
			if (m_CharacterLookupDictionary == null)
			{
				ReadFontAssetDefinition();
				if (m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			if (searchFallbacks && fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
			{
				for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
				{
					if (fallbackFontAssetTable[i].HasCharacter_Internal(character, searchFallbacks))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			if (m_CharacterLookupDictionary == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
				{
					missingCharacters.Add(text[i]);
				}
			}
			if (missingCharacters.Count == 0)
			{
				return true;
			}
			return false;
		}

		public bool HasCharacters(string text)
		{
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static string GetCharacters(TMP_FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += (char)fontAsset.characterTable[i].unicode;
			}
			return text;
		}

		public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		public bool TryAddCharacters(uint[] unicodes)
		{
			uint[] missingUnicodes;
			return TryAddCharacters(unicodes, out missingUnicodes);
		}

		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes)
		{
			s_MissingCharacterList.Clear();
			if (unicodes == null || unicodes.Length == 0 || m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = unicodes.ToArray();
				return false;
			}
			if (FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize) != 0)
			{
				missingUnicodes = unicodes.ToArray();
				return false;
			}
			m_GlyphIndexList.Clear();
			m_CharactersToAdd.Clear();
			bool flag = false;
			int num = unicodes.Length;
			for (int i = 0; i < num; i++)
			{
				uint num2 = unicodes[i];
				if (m_CharacterLookupDictionary.ContainsKey(num2))
				{
					continue;
				}
				uint glyphIndex = FontEngine.GetGlyphIndex(num2);
				if (glyphIndex == 0)
				{
					flag = true;
					continue;
				}
				TMP_Character tMP_Character = new TMP_Character(num2, glyphIndex);
				if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
					m_CharacterTable.Add(tMP_Character);
					m_CharacterLookupDictionary.Add(num2, tMP_Character);
				}
				else
				{
					m_GlyphIndexList.Add(glyphIndex);
					m_CharactersToAdd.Add(tMP_Character);
				}
			}
			if (m_GlyphIndexList.Count == 0)
			{
				missingUnicodes = unicodes.ToArray();
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			Glyph[] glyphs;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphIndexList, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
			foreach (Glyph glyph in glyphs)
			{
				uint index = glyph.index;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(index, glyph);
			}
			for (int k = 0; k < m_CharactersToAdd.Count; k++)
			{
				TMP_Character tMP_Character2 = m_CharactersToAdd[k];
				if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
				{
					s_MissingCharacterList.Add(tMP_Character2.unicode);
					continue;
				}
				tMP_Character2.glyph = value;
				m_CharacterTable.Add(tMP_Character2);
				m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
			}
			missingUnicodes = null;
			if (s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = s_MissingCharacterList.ToArray();
			}
			if (flag2)
			{
				return !flag;
			}
			return false;
		}

		public bool TryAddCharacters(string characters)
		{
			string missingCharacters;
			return TryAddCharacters(characters, out missingCharacters);
		}

		public bool TryAddCharacters(string characters, out string missingCharacters)
		{
			if (string.IsNullOrEmpty(characters) || m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize) != 0)
			{
				missingCharacters = characters;
				return false;
			}
			m_GlyphIndexList.Clear();
			m_CharactersToAdd.Clear();
			bool flag = false;
			int length = characters.Length;
			for (int i = 0; i < length; i++)
			{
				uint num = characters[i];
				if (m_CharacterLookupDictionary.ContainsKey(num))
				{
					continue;
				}
				uint glyphIndex = FontEngine.GetGlyphIndex(num);
				if (glyphIndex == 0)
				{
					flag = true;
					continue;
				}
				TMP_Character tMP_Character = new TMP_Character(num, glyphIndex);
				if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
					m_CharacterTable.Add(tMP_Character);
					m_CharacterLookupDictionary.Add(num, tMP_Character);
				}
				else
				{
					m_GlyphIndexList.Add(glyphIndex);
					m_CharactersToAdd.Add(tMP_Character);
				}
			}
			if (m_GlyphIndexList.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			Glyph[] glyphs;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphIndexList, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
			foreach (Glyph glyph in glyphs)
			{
				uint index = glyph.index;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(index, glyph);
			}
			missingCharacters = string.Empty;
			for (int k = 0; k < m_CharactersToAdd.Count; k++)
			{
				TMP_Character tMP_Character2 = m_CharactersToAdd[k];
				if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
				{
					missingCharacters += (char)tMP_Character2.unicode;
					continue;
				}
				tMP_Character2.glyph = value;
				m_CharacterTable.Add(tMP_Character2);
				m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
			}
			if (flag2)
			{
				return !flag;
			}
			return false;
		}

		internal bool TryAddCharacter_Internal(uint unicode)
		{
			TMP_Character tMP_Character = null;
			if (m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return true;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0)
			{
				return false;
			}
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				tMP_Character = new TMP_Character(unicode, m_GlyphLookupDictionary[glyphIndex]);
				m_CharacterTable.Add(tMP_Character);
				m_CharacterLookupDictionary.Add(unicode, tMP_Character);
				return true;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out var glyph))
			{
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				tMP_Character = new TMP_Character(unicode, glyph);
				m_CharacterTable.Add(tMP_Character);
				m_CharacterLookupDictionary.Add(unicode, tMP_Character);
				return true;
			}
			return false;
		}

		internal TMP_Character AddCharacter_Internal(uint unicode, Glyph glyph)
		{
			if (m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return m_CharacterLookupDictionary[unicode];
			}
			uint index = glyph.index;
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			if (!m_GlyphLookupDictionary.ContainsKey(index))
			{
				if (glyph.glyphRect.width == 0 || glyph.glyphRect.width == 0)
				{
					m_GlyphTable.Add(glyph);
				}
				else
				{
					if (!FontEngine.TryPackGlyphInAtlas(glyph, m_AtlasPadding, GlyphPackingMode.ContactPointRule, m_AtlasRenderMode, m_AtlasWidth, m_AtlasHeight, m_FreeGlyphRects, m_UsedGlyphRects))
					{
						return null;
					}
					m_GlyphsToRender.Add(glyph);
				}
			}
			TMP_Character tMP_Character = new TMP_Character(unicode, glyph);
			m_CharacterTable.Add(tMP_Character);
			m_CharacterLookupDictionary.Add(unicode, tMP_Character);
			UpdateAtlasTexture();
			return tMP_Character;
		}

		internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
		{
			character = null;
			if (FontEngine.LoadFontFace(sourceFontFile, m_FaceInfo.pointSize) != 0)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0)
			{
				return false;
			}
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, m_GlyphLookupDictionary[glyphIndex]);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				if (TMP_Settings.getFontFeaturesAtRuntime)
				{
					UpdateGlyphAdjustmentRecords(unicode, glyphIndex);
				}
				return true;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
			{
				if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
				{
					Debug.LogWarning("Unable to add the requested character to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
					return false;
				}
				m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out var glyph))
			{
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, glyph);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				m_GlyphIndexList.Add(glyphIndex);
				if (TMP_Settings.getFontFeaturesAtRuntime)
				{
					UpdateGlyphAdjustmentRecords(unicode, glyphIndex);
				}
				return true;
			}
			return false;
		}

		internal uint GetGlyphIndex(uint unicode)
		{
			if (FontEngine.LoadFontFace(sourceFontFile, m_FaceInfo.pointSize) != 0)
			{
				return 0u;
			}
			return FontEngine.GetGlyphIndex(unicode);
		}

		internal void UpdateAtlasTexture()
		{
			if (m_GlyphsToRender.Count != 0)
			{
				if (m_AtlasTextures[m_AtlasTextureIndex].width == 0 || m_AtlasTextures[m_AtlasTextureIndex].height == 0)
				{
					m_AtlasTextures[m_AtlasTextureIndex].Resize(m_AtlasWidth, m_AtlasHeight);
					FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
				}
				FontEngine.RenderGlyphsToTexture(m_GlyphsToRender, m_AtlasPadding, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex]);
				m_AtlasTextures[m_AtlasTextureIndex].Apply(updateMipmaps: false, makeNoLongerReadable: false);
				for (int i = 0; i < m_GlyphsToRender.Count; i++)
				{
					Glyph glyph = m_GlyphsToRender[i];
					glyph.atlasIndex = m_AtlasTextureIndex;
					m_GlyphTable.Add(glyph);
					m_GlyphLookupDictionary.Add(glyph.index, glyph);
				}
				m_GlyphsPacked.Clear();
				m_GlyphsToRender.Clear();
				_ = m_GlyphsToPack.Count;
				_ = 0;
			}
		}

		internal void UpdateGlyphAdjustmentRecords(uint unicode, uint glyphIndex)
		{
			int count = m_GlyphIndexList.Count;
			if (s_GlyphIndexArray.Length <= count)
			{
				s_GlyphIndexArray = new uint[Mathf.NextPowerOfTwo(count + 1)];
			}
			for (int i = 0; i < count; i++)
			{
				s_GlyphIndexArray[i] = m_GlyphIndexList[i];
			}
			Array.Clear(s_GlyphIndexArray, count, s_GlyphIndexArray.Length - count);
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(s_GlyphIndexArray);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			for (int j = 0; j < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[j].firstAdjustmentRecord.glyphIndex != 0; j++)
			{
				long key = (long)(((ulong)glyphPairAdjustmentTable[j].secondAdjustmentRecord.glyphIndex << 32) | glyphPairAdjustmentTable[j].firstAdjustmentRecord.glyphIndex);
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
				{
					TMP_GlyphPairAdjustmentRecord tMP_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(glyphPairAdjustmentTable[j]);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tMP_GlyphPairAdjustmentRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tMP_GlyphPairAdjustmentRecord);
				}
			}
		}

		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			if (m_GlyphTable != null)
			{
				m_GlyphTable.Clear();
			}
			if (m_CharacterTable != null)
			{
				m_CharacterTable.Clear();
			}
			if (m_UsedGlyphRects != null)
			{
				m_UsedGlyphRects.Clear();
			}
			if (m_FreeGlyphRects != null)
			{
				int num = (((m_AtlasRenderMode & (GlyphRenderMode)16) != (GlyphRenderMode)16) ? 1 : 0);
				m_FreeGlyphRects = new List<GlyphRect>
				{
					new GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num)
				};
			}
			if (m_GlyphsToPack != null)
			{
				m_GlyphsToPack.Clear();
			}
			if (m_GlyphsPacked != null)
			{
				m_GlyphsPacked.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
			}
			m_AtlasTextureIndex = 0;
			if (m_AtlasTextures != null)
			{
				for (int i = 0; i < m_AtlasTextures.Length; i++)
				{
					Texture2D texture2D = m_AtlasTextures[i];
					if (i > 0)
					{
						UnityEngine.Object.DestroyImmediate(texture2D, allowDestroyingAssets: true);
					}
					if (texture2D == null)
					{
						continue;
					}
					if (!m_AtlasTextures[i].isReadable)
					{
						Debug.LogWarning("Unable to reset font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[i].name + "] readable.", m_AtlasTextures[i]);
						continue;
					}
					if (setAtlasSizeToZero)
					{
						texture2D.Resize(0, 0, TextureFormat.Alpha8, hasMipMap: false);
					}
					else if (texture2D.width != m_AtlasWidth || texture2D.height != m_AtlasHeight)
					{
						texture2D.Resize(m_AtlasWidth, m_AtlasHeight, TextureFormat.Alpha8, hasMipMap: false);
					}
					FontEngine.ResetAtlasTexture(texture2D);
					texture2D.Apply();
					if (i == 0)
					{
						m_AtlasTexture = texture2D;
					}
					m_AtlasTextures[i] = texture2D;
				}
			}
			ReadFontAssetDefinition();
		}

		private void UpgradeFontAsset()
		{
			m_Version = "1.1.0";
			Debug.Log("Upgrading font asset [" + base.name + "] to version " + m_Version + ".", this);
			m_FaceInfo.familyName = m_fontInfo.Name;
			m_FaceInfo.styleName = string.Empty;
			m_FaceInfo.pointSize = (int)m_fontInfo.PointSize;
			m_FaceInfo.scale = m_fontInfo.Scale;
			m_FaceInfo.lineHeight = m_fontInfo.LineHeight;
			m_FaceInfo.ascentLine = m_fontInfo.Ascender;
			m_FaceInfo.capLine = m_fontInfo.CapHeight;
			m_FaceInfo.meanLine = m_fontInfo.CenterLine;
			m_FaceInfo.baseline = m_fontInfo.Baseline;
			m_FaceInfo.descentLine = m_fontInfo.Descender;
			m_FaceInfo.superscriptOffset = m_fontInfo.SuperscriptOffset;
			m_FaceInfo.superscriptSize = m_fontInfo.SubSize;
			m_FaceInfo.subscriptOffset = m_fontInfo.SubscriptOffset;
			m_FaceInfo.subscriptSize = m_fontInfo.SubSize;
			m_FaceInfo.underlineOffset = m_fontInfo.Underline;
			m_FaceInfo.underlineThickness = m_fontInfo.UnderlineThickness;
			m_FaceInfo.strikethroughOffset = m_fontInfo.strikethrough;
			m_FaceInfo.strikethroughThickness = m_fontInfo.strikethroughThickness;
			m_FaceInfo.tabWidth = m_fontInfo.TabWidth;
			if (m_AtlasTextures == null || m_AtlasTextures.Length == 0)
			{
				m_AtlasTextures = new Texture2D[1];
			}
			m_AtlasTextures[0] = atlas;
			m_AtlasWidth = (int)m_fontInfo.AtlasWidth;
			m_AtlasHeight = (int)m_fontInfo.AtlasHeight;
			m_AtlasPadding = (int)m_fontInfo.Padding;
			switch (m_CreationSettings.renderMode)
			{
			case 0:
				m_AtlasRenderMode = GlyphRenderMode.SMOOTH_HINTED;
				break;
			case 1:
				m_AtlasRenderMode = GlyphRenderMode.SMOOTH;
				break;
			case 2:
				m_AtlasRenderMode = GlyphRenderMode.RASTER_HINTED;
				break;
			case 3:
				m_AtlasRenderMode = GlyphRenderMode.RASTER;
				break;
			case 6:
				m_AtlasRenderMode = GlyphRenderMode.SDF16;
				break;
			case 7:
				m_AtlasRenderMode = GlyphRenderMode.SDF32;
				break;
			}
			if (fontWeights != null)
			{
				m_FontWeightTable[4] = fontWeights[4];
				m_FontWeightTable[7] = fontWeights[7];
			}
			if (fallbackFontAssets != null && fallbackFontAssets.Count > 0)
			{
				if (m_FallbackFontAssetTable == null)
				{
					m_FallbackFontAssetTable = new List<TMP_FontAsset>(fallbackFontAssets.Count);
				}
				for (int i = 0; i < fallbackFontAssets.Count; i++)
				{
					m_FallbackFontAssetTable.Add(fallbackFontAssets[i]);
				}
			}
			if (m_CreationSettings.sourceFontFileGUID != null || m_CreationSettings.sourceFontFileGUID != string.Empty)
			{
				m_SourceFontFileGUID = m_CreationSettings.sourceFontFileGUID;
			}
			else
			{
				Debug.LogWarning("Font asset [" + base.name + "] doesn't have a reference to its source font file. Please assign the appropriate source font file for this asset in the Font Atlas & Material section of font asset inspector.", this);
			}
			m_GlyphTable.Clear();
			m_CharacterTable.Clear();
			bool flag = false;
			for (int j = 0; j < m_glyphInfoList.Count; j++)
			{
				TMP_Glyph tMP_Glyph = m_glyphInfoList[j];
				Glyph glyph = new Glyph();
				uint index = (uint)(j + 1);
				glyph.index = index;
				glyph.glyphRect = new GlyphRect((int)tMP_Glyph.x, m_AtlasHeight - (int)(tMP_Glyph.y + tMP_Glyph.height + 0.5f), (int)(tMP_Glyph.width + 0.5f), (int)(tMP_Glyph.height + 0.5f));
				glyph.metrics = new GlyphMetrics(tMP_Glyph.width, tMP_Glyph.height, tMP_Glyph.xOffset, tMP_Glyph.yOffset, tMP_Glyph.xAdvance);
				glyph.scale = tMP_Glyph.scale;
				glyph.atlasIndex = 0;
				m_GlyphTable.Add(glyph);
				TMP_Character item = new TMP_Character((uint)tMP_Glyph.id, glyph);
				if (tMP_Glyph.id == 32)
				{
					flag = true;
				}
				m_CharacterTable.Add(item);
			}
			if (!flag)
			{
				Debug.Log("Synthesizing Space for [" + base.name + "]");
				Glyph glyph2 = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, m_FaceInfo.ascentLine / 5f), GlyphRect.zero, 1f, 0);
				m_GlyphTable.Add(glyph2);
				m_CharacterTable.Add(new TMP_Character(32u, glyph2));
			}
			ReadFontAssetDefinition();
		}

		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int count = m_KerningTable.kerningPairs.Count;
			m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>(count);
			for (int i = 0; i < count; i++)
			{
				KerningPair kerningPair = m_KerningTable.kerningPairs[i];
				uint glyphIndex = 0u;
				if (m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out var value))
				{
					glyphIndex = value.glyphIndex;
				}
				uint glyphIndex2 = 0u;
				if (m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out var value2))
				{
					glyphIndex2 = value2.glyphIndex;
				}
				TMP_GlyphAdjustmentRecord firstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex, new TMP_GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
				TMP_GlyphAdjustmentRecord secondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex2, new TMP_GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
				TMP_GlyphPairAdjustmentRecord item = new TMP_GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);
				m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(item);
			}
			m_KerningTable.kerningPairs = null;
			m_KerningTable = null;
		}
	}
}
