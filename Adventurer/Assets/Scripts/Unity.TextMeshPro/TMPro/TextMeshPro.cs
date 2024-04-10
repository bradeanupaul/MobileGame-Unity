using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[AddComponentMenu("Mesh/TextMeshPro - Text")]
	[ExecuteAlways]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		[SerializeField]
		private bool m_hasFontAssetChanged;

		private float m_previousLossyScaleY = -1f;

		[SerializeField]
		private Renderer m_renderer;

		private MeshFilter m_meshFilter;

		private bool m_isFirstAllocation;

		private int m_max_characters = 8;

		private int m_max_numberOfLines = 4;

		[SerializeField]
		protected TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

		private bool m_isMaskingEnabled;

		private bool isMaskUpdateRequired;

		[SerializeField]
		private MaskingTypes m_maskType;

		private Matrix4x4 m_EnvMapMatrix;

		private Vector3[] m_RectTransformCorners = new Vector3[4];

		[NonSerialized]
		private bool m_isRegisteredForEvents;

		private int loopCountA;

		private bool m_currentAutoSizeMode;

		public int sortingLayerID
		{
			get
			{
				return m_renderer.sortingLayerID;
			}
			set
			{
				m_renderer.sortingLayerID = value;
			}
		}

		public int sortingOrder
		{
			get
			{
				return m_renderer.sortingOrder;
			}
			set
			{
				m_renderer.sortingOrder = value;
			}
		}

		public override bool autoSizeTextContainer
		{
			get
			{
				return m_autoSizeTextContainer;
			}
			set
			{
				if (m_autoSizeTextContainer != value)
				{
					m_autoSizeTextContainer = value;
					if (m_autoSizeTextContainer)
					{
						TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
						SetLayoutDirty();
					}
				}
			}
		}

		[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
		public TextContainer textContainer => null;

		public new Transform transform
		{
			get
			{
				if (m_transform == null)
				{
					m_transform = GetComponent<Transform>();
				}
				return m_transform;
			}
		}

		public Renderer renderer
		{
			get
			{
				if (m_renderer == null)
				{
					m_renderer = GetComponent<Renderer>();
				}
				return m_renderer;
			}
		}

		public override Mesh mesh
		{
			get
			{
				if (m_mesh == null)
				{
					m_mesh = new Mesh();
					m_mesh.hideFlags = HideFlags.HideAndDontSave;
					meshFilter.mesh = m_mesh;
				}
				return m_mesh;
			}
		}

		public MeshFilter meshFilter
		{
			get
			{
				if (m_meshFilter == null)
				{
					m_meshFilter = GetComponent<MeshFilter>();
				}
				return m_meshFilter;
			}
		}

		public MaskingTypes maskType
		{
			get
			{
				return m_maskType;
			}
			set
			{
				m_maskType = value;
				SetMask(m_maskType);
			}
		}

		protected override void Awake()
		{
			m_renderer = GetComponent<Renderer>();
			if (m_renderer == null)
			{
				m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			if (base.canvasRenderer != null)
			{
				base.canvasRenderer.hideFlags = HideFlags.HideInInspector;
			}
			else
			{
				base.gameObject.AddComponent<CanvasRenderer>().hideFlags = HideFlags.HideInInspector;
			}
			m_rectTransform = base.rectTransform;
			m_transform = transform;
			m_meshFilter = GetComponent<MeshFilter>();
			if (m_meshFilter == null)
			{
				m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
				m_meshFilter.mesh = m_mesh;
				m_textInfo = new TMP_TextInfo(this);
			}
			m_meshFilter.hideFlags = HideFlags.HideInInspector;
			LoadDefaultSettings();
			LoadFontAsset();
			TMP_StyleSheet.LoadDefaultStyleSheet();
			if (m_TextParsingBuffer == null)
			{
				m_TextParsingBuffer = new UnicodeChar[m_max_characters];
			}
			m_cached_TextElement = new TMP_Character();
			m_isFirstAllocation = true;
			if (m_fontAsset == null)
			{
				Debug.LogWarning("Please assign a Font Asset to this " + transform.name + " gameobject.", this);
				return;
			}
			TMP_SubMesh[] componentsInChildren = GetComponentsInChildren<TMP_SubMesh>();
			if (componentsInChildren.Length != 0)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					m_subTextObjects[i + 1] = componentsInChildren[i];
				}
			}
			m_isInputParsingRequired = true;
			m_havePropertiesChanged = true;
			m_isCalculateSizeRequired = true;
			m_isAwake = true;
		}

		protected override void OnEnable()
		{
			if (m_isAwake)
			{
				if (!m_isRegisteredForEvents)
				{
					m_isRegisteredForEvents = true;
				}
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
				meshFilter.sharedMesh = mesh;
				SetActiveSubMeshes(state: true);
				ComputeMarginSize();
				m_isInputParsingRequired = true;
				m_havePropertiesChanged = true;
				m_verticesAlreadyDirty = false;
				SetVerticesDirty();
			}
		}

		protected override void OnDisable()
		{
			if (m_isAwake)
			{
				TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
				TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
				m_meshFilter.sharedMesh = null;
				SetActiveSubMeshes(state: false);
			}
		}

		protected override void OnDestroy()
		{
			if (m_mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(m_mesh);
			}
			m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		}

		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (m_fontAsset == null)
			{
				if (TMP_Settings.defaultFontAsset != null)
				{
					m_fontAsset = TMP_Settings.defaultFontAsset;
				}
				else
				{
					m_fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
				}
				if (m_fontAsset == null)
				{
					Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (m_fontAsset.characterLookupTable == null)
				{
					Debug.Log("Dictionary is Null!");
				}
				m_renderer.sharedMaterial = m_fontAsset.material;
				m_sharedMaterial = m_fontAsset.material;
				m_sharedMaterial.SetFloat("_CullMode", 0f);
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				m_renderer.receiveShadows = false;
				m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (m_fontAsset.characterLookupTable == null)
				{
					m_fontAsset.ReadFontAssetDefinition();
				}
				if (m_renderer.sharedMaterial == null || m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || m_fontAsset.atlasTexture.GetInstanceID() != m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					m_renderer.sharedMaterial = m_fontAsset.material;
					m_sharedMaterial = m_fontAsset.material;
				}
				else
				{
					m_sharedMaterial = m_renderer.sharedMaterial;
				}
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				if (m_sharedMaterial.passCount == 1)
				{
					m_renderer.receiveShadows = false;
					m_renderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			m_padding = GetPaddingForMaterial();
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			GetSpecialCharacters(m_fontAsset);
		}

		private void UpdateEnvMapMatrix()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) && !(m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) == null))
			{
				Vector3 euler = m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
				m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(euler), Vector3.one);
				m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, m_EnvMapMatrix);
			}
		}

		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			case MaskingTypes.MaskSoft:
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			case MaskingTypes.MaskHard:
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			}
		}

		private void SetMaskCoordinates(Vector4 coords)
		{
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		private void EnableMasking()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				m_isMaskingEnabled = true;
				UpdateMask();
			}
		}

		private void DisableMasking()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				m_isMaskingEnabled = false;
				UpdateMask();
			}
		}

		private void UpdateMask()
		{
			if (m_isMaskingEnabled && m_isMaskingEnabled && m_fontMaterial == null)
			{
				CreateMaterialInstance();
			}
		}

		protected override Material GetMaterial(Material mat)
		{
			if (m_fontMaterial == null || m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				m_fontMaterial = CreateMaterialInstance(mat);
			}
			m_sharedMaterial = m_fontMaterial;
			m_padding = GetPaddingForMaterial();
			SetVerticesDirty();
			SetMaterialDirty();
			return m_sharedMaterial;
		}

		protected override Material[] GetMaterials(Material[] mats)
		{
			int materialCount = m_textInfo.materialCount;
			if (m_fontMaterials == null)
			{
				m_fontMaterials = new Material[materialCount];
			}
			else if (m_fontMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize(ref m_fontMaterials, materialCount, isBlockAllocated: false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					m_fontMaterials[i] = base.fontMaterial;
				}
				else
				{
					m_fontMaterials[i] = m_subTextObjects[i].material;
				}
			}
			m_fontSharedMaterials = m_fontMaterials;
			return m_fontMaterials;
		}

		protected override void SetSharedMaterial(Material mat)
		{
			m_sharedMaterial = mat;
			m_padding = GetPaddingForMaterial();
			SetMaterialDirty();
		}

		protected override Material[] GetSharedMaterials()
		{
			int materialCount = m_textInfo.materialCount;
			if (m_fontSharedMaterials == null)
			{
				m_fontSharedMaterials = new Material[materialCount];
			}
			else if (m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					m_fontSharedMaterials[i] = m_sharedMaterial;
				}
				else
				{
					m_fontSharedMaterials[i] = m_subTextObjects[i].sharedMaterial;
				}
			}
			return m_fontSharedMaterials;
		}

		protected override void SetSharedMaterials(Material[] materials)
		{
			int materialCount = m_textInfo.materialCount;
			if (m_fontSharedMaterials == null)
			{
				m_fontSharedMaterials = new Material[materialCount];
			}
			else if (m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize(ref m_fontSharedMaterials, materialCount, isBlockAllocated: false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				Texture texture = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
				if (i == 0)
				{
					if (!(texture == null) && texture.GetInstanceID() == m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						m_sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
						m_padding = GetPaddingForMaterial(m_sharedMaterial);
					}
				}
				else if (!(texture == null) && texture.GetInstanceID() == m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && m_subTextObjects[i].isDefaultMaterial)
				{
					m_subTextObjects[i].sharedMaterial = (m_fontSharedMaterials[i] = materials[i]);
				}
			}
		}

		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_fontMaterial = m_renderer.material;
			m_sharedMaterial = m_fontMaterial;
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFaceColor(Color32 color)
		{
			m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_sharedMaterial = m_fontMaterial;
		}

		protected override void SetOutlineColor(Color32 color)
		{
			m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_sharedMaterial = m_fontMaterial;
		}

		private void CreateMaterialInstance()
		{
			Material material = new Material(m_sharedMaterial);
			material.shaderKeywords = m_sharedMaterial.shaderKeywords;
			material.name += " Instance";
			m_fontMaterial = material;
		}

		protected override void SetShaderDepth()
		{
			if (m_isOverlay)
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
				m_renderer.material.renderQueue = 4000;
				m_sharedMaterial = m_renderer.material;
			}
			else
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				m_renderer.material.renderQueue = -1;
				m_sharedMaterial = m_renderer.material;
			}
		}

		protected override void SetCulling()
		{
			if (m_isCullingEnabled)
			{
				m_renderer.material.SetFloat("_CullMode", 2f);
				for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
				{
					Renderer renderer = m_subTextObjects[i].renderer;
					if (renderer != null)
					{
						renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
					}
				}
				return;
			}
			m_renderer.material.SetFloat("_CullMode", 0f);
			for (int j = 1; j < m_subTextObjects.Length && m_subTextObjects[j] != null; j++)
			{
				Renderer renderer2 = m_subTextObjects[j].renderer;
				if (renderer2 != null)
				{
					renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
				}
			}
		}

		private void SetPerspectiveCorrection()
		{
			if (m_isOrthographic)
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
			}
			else
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
			}
		}

		protected override float GetPaddingForMaterial(Material mat)
		{
			m_padding = ShaderUtilities.GetPadding(mat, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
			return m_padding;
		}

		protected override float GetPaddingForMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (m_sharedMaterial == null)
			{
				return 0f;
			}
			m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_isSDFShader = m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
			return m_padding;
		}

		protected override int SetArraySizes(UnicodeChar[] chars)
		{
			int num = 0;
			m_totalCharacterCount = 0;
			m_isUsingBold = false;
			m_isParsingText = false;
			tag_NoParsing = false;
			m_FontStyleInternal = m_fontStyle;
			m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
			m_FontWeightStack.SetDefault(m_FontWeightInternal);
			m_currentFontAsset = m_fontAsset;
			m_currentMaterial = m_sharedMaterial;
			m_currentMaterialIndex = 0;
			m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, null, m_currentMaterial, m_padding));
			m_materialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
			if (m_textInfo == null)
			{
				m_textInfo = new TMP_TextInfo();
			}
			m_textElementType = TMP_TextElementType.Character;
			if (m_linkedTextComponent != null)
			{
				m_linkedTextComponent.text = string.Empty;
				m_linkedTextComponent.ForceMeshUpdate();
			}
			for (int i = 0; i < chars.Length && chars[i].unicode != 0; i++)
			{
				if (m_textInfo.characterInfo == null || m_totalCharacterCount >= m_textInfo.characterInfo.Length)
				{
					TMP_TextInfo.Resize(ref m_textInfo.characterInfo, m_totalCharacterCount + 1, isBlockAllocated: true);
				}
				int num2 = chars[i].unicode;
				if (m_isRichText && num2 == 60)
				{
					int currentMaterialIndex = m_currentMaterialIndex;
					if (ValidateHtmlTag(chars, i + 1, out var endIndex))
					{
						int stringIndex = chars[i].stringIndex;
						i = endIndex;
						if ((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
						{
							m_isUsingBold = true;
						}
						if (m_textElementType == TMP_TextElementType.Sprite)
						{
							m_materialReferences[m_currentMaterialIndex].referenceCount++;
							m_textInfo.characterInfo[m_totalCharacterCount].character = (char)(57344 + m_spriteIndex);
							m_textInfo.characterInfo[m_totalCharacterCount].spriteIndex = m_spriteIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].spriteAsset = m_currentSpriteAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].textElement = m_currentSpriteAsset.spriteCharacterTable[m_spriteIndex];
							m_textInfo.characterInfo[m_totalCharacterCount].elementType = m_textElementType;
							m_textInfo.characterInfo[m_totalCharacterCount].index = stringIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].stringIndex - stringIndex + 1;
							m_textElementType = TMP_TextElementType.Character;
							m_currentMaterialIndex = currentMaterialIndex;
							num++;
							m_totalCharacterCount++;
						}
						continue;
					}
				}
				bool isAlternativeTypeface = false;
				bool flag = false;
				TMP_FontAsset currentFontAsset = m_currentFontAsset;
				Material currentMaterial = m_currentMaterial;
				int currentMaterialIndex2 = m_currentMaterialIndex;
				if (m_textElementType == TMP_TextElementType.Character)
				{
					if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num2))
						{
							num2 = char.ToUpper((char)num2);
						}
					}
					else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num2))
						{
							num2 = char.ToLower((char)num2);
						}
					}
					else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num2))
					{
						num2 = char.ToUpper((char)num2);
					}
				}
				TMP_FontAsset fontAsset;
				TMP_Character tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num2, m_currentFontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				if (tMP_Character == null && m_currentFontAsset.fallbackFontAssetTable != null && m_currentFontAsset.fallbackFontAssetTable.Count > 0)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num2, m_currentFontAsset.fallbackFontAssetTable, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null)
				{
					TMP_SpriteAsset tMP_SpriteAsset = base.spriteAsset;
					if (tMP_SpriteAsset != null)
					{
						int spriteIndex = -1;
						tMP_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicode(tMP_SpriteAsset, (uint)num2, includeFallbacks: true, out spriteIndex);
						if (spriteIndex != -1)
						{
							m_textElementType = TMP_TextElementType.Sprite;
							m_textInfo.characterInfo[m_totalCharacterCount].elementType = m_textElementType;
							m_currentMaterialIndex = MaterialReference.AddMaterialReference(tMP_SpriteAsset.material, tMP_SpriteAsset, m_materialReferences, m_materialReferenceIndexLookup);
							m_materialReferences[m_currentMaterialIndex].referenceCount++;
							m_textInfo.characterInfo[m_totalCharacterCount].character = (char)num2;
							m_textInfo.characterInfo[m_totalCharacterCount].spriteIndex = spriteIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].spriteAsset = tMP_SpriteAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].textElement = tMP_SpriteAsset.spriteCharacterTable[m_spriteIndex];
							m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].index = chars[i].stringIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].length;
							m_textElementType = TMP_TextElementType.Character;
							m_currentMaterialIndex = currentMaterialIndex2;
							num++;
							m_totalCharacterCount++;
							continue;
						}
					}
				}
				if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num2, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
				{
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num2, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
				}
				if (tMP_Character == null)
				{
					TMP_SpriteAsset defaultSpriteAsset = TMP_Settings.defaultSpriteAsset;
					if (defaultSpriteAsset != null)
					{
						int spriteIndex2 = -1;
						defaultSpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicode(defaultSpriteAsset, (uint)num2, includeFallbacks: true, out spriteIndex2);
						if (spriteIndex2 != -1)
						{
							m_textElementType = TMP_TextElementType.Sprite;
							m_textInfo.characterInfo[m_totalCharacterCount].elementType = m_textElementType;
							m_currentMaterialIndex = MaterialReference.AddMaterialReference(defaultSpriteAsset.material, defaultSpriteAsset, m_materialReferences, m_materialReferenceIndexLookup);
							m_materialReferences[m_currentMaterialIndex].referenceCount++;
							m_textInfo.characterInfo[m_totalCharacterCount].character = (char)num2;
							m_textInfo.characterInfo[m_totalCharacterCount].spriteIndex = spriteIndex2;
							m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].spriteAsset = defaultSpriteAsset;
							m_textInfo.characterInfo[m_totalCharacterCount].textElement = defaultSpriteAsset.spriteCharacterTable[m_spriteIndex];
							m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].index = chars[i].stringIndex;
							m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].length;
							m_textElementType = TMP_TextElementType.Character;
							m_currentMaterialIndex = currentMaterialIndex2;
							num++;
							m_totalCharacterCount++;
							continue;
						}
					}
				}
				if (tMP_Character == null)
				{
					int num3 = num2;
					num2 = (chars[i].unicode = ((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
					tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num2, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
					if (tMP_Character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
					{
						tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAssets((uint)num2, TMP_Settings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
					}
					if (tMP_Character == null && TMP_Settings.defaultFontAsset != null)
					{
						tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num2, TMP_Settings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
					}
					if (tMP_Character == null)
					{
						num2 = (chars[i].unicode = 32);
						tMP_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)num2, m_currentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface, out fontAsset);
						if (!TMP_Settings.warningsDisabled)
						{
							Debug.LogWarning("Character with ASCII value of " + num3 + " was not found in the Font Asset Glyph Table. It was replaced by a space.", this);
						}
					}
				}
				if (fontAsset != null && fontAsset.GetInstanceID() != m_currentFontAsset.GetInstanceID())
				{
					flag = true;
					m_currentFontAsset = fontAsset;
				}
				m_textInfo.characterInfo[m_totalCharacterCount].elementType = TMP_TextElementType.Character;
				m_textInfo.characterInfo[m_totalCharacterCount].textElement = tMP_Character;
				m_textInfo.characterInfo[m_totalCharacterCount].isUsingAlternateTypeface = isAlternativeTypeface;
				m_textInfo.characterInfo[m_totalCharacterCount].character = (char)num2;
				m_textInfo.characterInfo[m_totalCharacterCount].fontAsset = m_currentFontAsset;
				m_textInfo.characterInfo[m_totalCharacterCount].index = chars[i].stringIndex;
				m_textInfo.characterInfo[m_totalCharacterCount].stringLength = chars[i].length;
				if (flag)
				{
					if (TMP_Settings.matchMaterialPreset)
					{
						m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(m_currentMaterial, m_currentFontAsset.material);
					}
					else
					{
						m_currentMaterial = m_currentFontAsset.material;
					}
					m_currentMaterialIndex = MaterialReference.AddMaterialReference(m_currentMaterial, m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
				}
				if (!char.IsWhiteSpace((char)num2) && num2 != 8203)
				{
					if (m_materialReferences[m_currentMaterialIndex].referenceCount < 16383)
					{
						m_materialReferences[m_currentMaterialIndex].referenceCount++;
					}
					else
					{
						m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(m_currentMaterial), m_currentFontAsset, m_materialReferences, m_materialReferenceIndexLookup);
						m_materialReferences[m_currentMaterialIndex].referenceCount++;
					}
				}
				m_textInfo.characterInfo[m_totalCharacterCount].material = m_currentMaterial;
				m_textInfo.characterInfo[m_totalCharacterCount].materialReferenceIndex = m_currentMaterialIndex;
				m_materialReferences[m_currentMaterialIndex].isFallbackMaterial = flag;
				if (flag)
				{
					m_materialReferences[m_currentMaterialIndex].fallbackMaterial = currentMaterial;
					m_currentFontAsset = currentFontAsset;
					m_currentMaterial = currentMaterial;
					m_currentMaterialIndex = currentMaterialIndex2;
				}
				m_totalCharacterCount++;
			}
			if (m_isCalculatingPreferredValues)
			{
				m_isCalculatingPreferredValues = false;
				m_isInputParsingRequired = true;
				return m_totalCharacterCount;
			}
			m_textInfo.spriteCount = num;
			int num4 = (m_textInfo.materialCount = m_materialReferenceIndexLookup.Count);
			if (num4 > m_textInfo.meshInfo.Length)
			{
				TMP_TextInfo.Resize(ref m_textInfo.meshInfo, num4, isBlockAllocated: false);
			}
			if (num4 > m_subTextObjects.Length)
			{
				TMP_TextInfo.Resize(ref m_subTextObjects, Mathf.NextPowerOfTwo(num4 + 1));
			}
			if (m_textInfo.characterInfo.Length - m_totalCharacterCount > 256)
			{
				TMP_TextInfo.Resize(ref m_textInfo.characterInfo, Mathf.Max(m_totalCharacterCount + 1, 256), isBlockAllocated: true);
			}
			for (int j = 0; j < num4; j++)
			{
				if (j > 0)
				{
					if (m_subTextObjects[j] == null)
					{
						m_subTextObjects[j] = TMP_SubMesh.AddSubTextObject(this, m_materialReferences[j]);
						m_textInfo.meshInfo[j].vertices = null;
					}
					if (m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetInstanceID() != m_materialReferences[j].material.GetInstanceID())
					{
						bool isDefaultMaterial = m_materialReferences[j].isDefaultMaterial;
						m_subTextObjects[j].isDefaultMaterial = isDefaultMaterial;
						if (!isDefaultMaterial || m_subTextObjects[j].sharedMaterial == null || m_subTextObjects[j].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() != m_materialReferences[j].material.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
						{
							m_subTextObjects[j].sharedMaterial = m_materialReferences[j].material;
							m_subTextObjects[j].fontAsset = m_materialReferences[j].fontAsset;
							m_subTextObjects[j].spriteAsset = m_materialReferences[j].spriteAsset;
						}
					}
					if (m_materialReferences[j].isFallbackMaterial)
					{
						m_subTextObjects[j].fallbackMaterial = m_materialReferences[j].material;
						m_subTextObjects[j].fallbackSourceMaterial = m_materialReferences[j].fallbackMaterial;
					}
				}
				int referenceCount = m_materialReferences[j].referenceCount;
				if (m_textInfo.meshInfo[j].vertices == null || m_textInfo.meshInfo[j].vertices.Length < referenceCount * ((!m_isVolumetricText) ? 4 : 8))
				{
					if (m_textInfo.meshInfo[j].vertices == null)
					{
						if (j == 0)
						{
							m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_mesh, referenceCount + 1, m_isVolumetricText);
						}
						else
						{
							m_textInfo.meshInfo[j] = new TMP_MeshInfo(m_subTextObjects[j].mesh, referenceCount + 1, m_isVolumetricText);
						}
					}
					else
					{
						m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), m_isVolumetricText);
					}
				}
				else if (m_VertexBufferAutoSizeReduction && referenceCount > 0 && m_textInfo.meshInfo[j].vertices.Length - referenceCount * ((!m_isVolumetricText) ? 4 : 8) > 1024)
				{
					m_textInfo.meshInfo[j].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1), m_isVolumetricText);
				}
			}
			for (int k = num4; k < m_subTextObjects.Length && m_subTextObjects[k] != null; k++)
			{
				if (k < m_textInfo.meshInfo.Length)
				{
					m_textInfo.meshInfo[k].ClearUnusedVertices(0, updateMesh: true);
				}
			}
			return m_totalCharacterCount;
		}

		public override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				m_marginWidth = m_rectTransform.rect.width - m_margin.x - m_margin.z;
				m_marginHeight = m_rectTransform.rect.height - m_margin.y - m_margin.w;
				m_RectTransformCorners = GetTextContainerLocalCorners();
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			m_havePropertiesChanged = true;
			isMaskUpdateRequired = true;
			SetVerticesDirty();
		}

		protected override void OnTransformParentChanged()
		{
			SetVerticesDirty();
			SetLayoutDirty();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			ComputeMarginSize();
			SetVerticesDirty();
			SetLayoutDirty();
		}

		internal override void InternalUpdate()
		{
			if (!m_havePropertiesChanged)
			{
				float y = m_rectTransform.lossyScale.y;
				if (y != m_previousLossyScaleY && m_text != string.Empty && m_text != null)
				{
					float scaleDelta = y / m_previousLossyScaleY;
					UpdateSDFScale(scaleDelta);
					m_previousLossyScaleY = y;
				}
			}
			if (m_isUsingLegacyAnimationComponent)
			{
				m_havePropertiesChanged = true;
				OnPreRenderObject();
			}
		}

		private void OnPreRenderObject()
		{
			if (!m_isAwake || (!IsActive() && !m_ignoreActiveState))
			{
				return;
			}
			loopCountA = 0;
			if (m_havePropertiesChanged || m_isLayoutDirty)
			{
				if (isMaskUpdateRequired)
				{
					UpdateMask();
					isMaskUpdateRequired = false;
				}
				if (checkPaddingRequired)
				{
					UpdateMeshPadding();
				}
				if (m_isInputParsingRequired || m_isTextTruncated)
				{
					ParseInputText();
				}
				if (m_enableAutoSizing)
				{
					m_fontSize = Mathf.Clamp(m_fontSizeBase, m_fontSizeMin, m_fontSizeMax);
				}
				m_maxFontSize = m_fontSizeMax;
				m_minFontSize = m_fontSizeMin;
				m_lineSpacingDelta = 0f;
				m_charWidthAdjDelta = 0f;
				m_isCharacterWrappingEnabled = false;
				m_isTextTruncated = false;
				m_havePropertiesChanged = false;
				m_isLayoutDirty = false;
				m_ignoreActiveState = false;
				GenerateTextMesh();
			}
		}

		protected override void GenerateTextMesh()
		{
			if (m_fontAsset == null || m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + GetInstanceID());
				return;
			}
			if (m_textInfo != null)
			{
				m_textInfo.Clear();
			}
			if (m_TextParsingBuffer == null || m_TextParsingBuffer.Length == 0 || m_TextParsingBuffer[0].unicode == 0)
			{
				ClearMesh(updateMesh: true);
				m_preferredWidth = 0f;
				m_preferredHeight = 0f;
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			m_currentFontAsset = m_fontAsset;
			m_currentMaterial = m_sharedMaterial;
			m_currentMaterialIndex = 0;
			m_materialReferenceStack.SetDefault(new MaterialReference(m_currentMaterialIndex, m_currentFontAsset, null, m_currentMaterial, m_padding));
			m_currentSpriteAsset = m_spriteAsset;
			if (m_spriteAnimator != null)
			{
				m_spriteAnimator.StopAllAnimations();
			}
			int totalCharacterCount = m_totalCharacterCount;
			float num = (m_fontScale = m_fontSize / (float)m_fontAsset.faceInfo.pointSize * m_fontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f));
			float num2 = num;
			m_fontScaleMultiplier = 1f;
			m_currentFontSize = m_fontSize;
			m_sizeStack.SetDefault(m_currentFontSize);
			float num3 = 0f;
			int num4 = 0;
			m_FontStyleInternal = m_fontStyle;
			m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : m_fontWeight);
			m_FontWeightStack.SetDefault(m_FontWeightInternal);
			m_fontStyleStack.Clear();
			m_lineJustification = m_textAlignment;
			m_lineJustificationStack.SetDefault(m_lineJustification);
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			m_baselineOffset = 0f;
			m_baselineOffsetStack.Clear();
			bool flag = false;
			Vector3 start = Vector3.zero;
			Vector3 zero = Vector3.zero;
			bool flag2 = false;
			Vector3 start2 = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			bool flag3 = false;
			Vector3 start3 = Vector3.zero;
			Vector3 vector = Vector3.zero;
			m_fontColor32 = m_fontColor;
			m_htmlColor = m_fontColor32;
			m_underlineColor = m_htmlColor;
			m_strikethroughColor = m_htmlColor;
			m_colorStack.SetDefault(m_htmlColor);
			m_underlineColorStack.SetDefault(m_htmlColor);
			m_strikethroughColorStack.SetDefault(m_htmlColor);
			m_highlightColorStack.SetDefault(m_htmlColor);
			m_colorGradientPreset = null;
			m_colorGradientStack.SetDefault(null);
			m_actionStack.Clear();
			m_isFXMatrixSet = false;
			m_lineOffset = 0f;
			m_lineHeight = -32767f;
			float num8 = m_currentFontAsset.faceInfo.lineHeight - (m_currentFontAsset.faceInfo.ascentLine - m_currentFontAsset.faceInfo.descentLine);
			m_cSpacing = 0f;
			m_monoSpacing = 0f;
			float num9 = 0f;
			m_xAdvance = 0f;
			tag_LineIndent = 0f;
			tag_Indent = 0f;
			m_indentStack.SetDefault(0f);
			tag_NoParsing = false;
			m_characterCount = 0;
			m_firstCharacterOfLine = 0;
			m_lastCharacterOfLine = 0;
			m_firstVisibleCharacterOfLine = 0;
			m_lastVisibleCharacterOfLine = 0;
			m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			m_lineNumber = 0;
			m_lineVisibleCharacterCount = 0;
			bool flag4 = true;
			m_firstOverflowCharacterIndex = -1;
			m_pageNumber = 0;
			int num10 = Mathf.Clamp(m_pageToDisplay - 1, 0, m_textInfo.pageInfo.Length - 1);
			int num11 = 0;
			int num12 = 0;
			Vector4 vector2 = m_margin;
			float marginWidth = m_marginWidth;
			float marginHeight = m_marginHeight;
			m_marginLeft = 0f;
			m_marginRight = 0f;
			m_width = -1f;
			float num13 = marginWidth + 0.0001f - m_marginLeft - m_marginRight;
			m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
			m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
			m_textInfo.ClearLineInfo();
			m_maxCapHeight = 0f;
			m_maxAscender = 0f;
			m_maxDescender = 0f;
			float num14 = 0f;
			float num15 = 0f;
			bool flag5 = false;
			m_isNewPage = false;
			bool flag6 = true;
			m_isNonBreakingSpace = false;
			bool flag7 = false;
			bool flag8 = false;
			int num16 = 0;
			SaveWordWrappingState(ref m_SavedWordWrapState, -1, -1);
			SaveWordWrappingState(ref m_SavedLineState, -1, -1);
			loopCountA++;
			Vector3 vector3 = default(Vector3);
			Vector3 vector4 = default(Vector3);
			Vector3 vector5 = default(Vector3);
			Vector3 vector6 = default(Vector3);
			for (int i = 0; i < m_TextParsingBuffer.Length && m_TextParsingBuffer[i].unicode != 0; i++)
			{
				num4 = m_TextParsingBuffer[i].unicode;
				if (m_isRichText && num4 == 60)
				{
					m_isParsingText = true;
					m_textElementType = TMP_TextElementType.Character;
					if (ValidateHtmlTag(m_TextParsingBuffer, i + 1, out var endIndex))
					{
						i = endIndex;
						if (m_textElementType == TMP_TextElementType.Character)
						{
							continue;
						}
					}
				}
				else
				{
					m_textElementType = m_textInfo.characterInfo[m_characterCount].elementType;
					m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
					m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
				}
				int currentMaterialIndex = m_currentMaterialIndex;
				bool isUsingAlternateTypeface = m_textInfo.characterInfo[m_characterCount].isUsingAlternateTypeface;
				m_isParsingText = false;
				if (m_characterCount < m_firstVisibleCharacter)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = false;
					m_textInfo.characterInfo[m_characterCount].character = '\u200b';
					m_characterCount++;
					continue;
				}
				float num17 = 1f;
				if (m_textElementType == TMP_TextElementType.Character)
				{
					if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num4))
						{
							num4 = char.ToUpper((char)num4);
						}
					}
					else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num4))
						{
							num4 = char.ToLower((char)num4);
						}
					}
					else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num4))
					{
						num17 = 0.8f;
						num4 = char.ToUpper((char)num4);
					}
				}
				if (m_textElementType == TMP_TextElementType.Sprite)
				{
					m_currentSpriteAsset = m_textInfo.characterInfo[m_characterCount].spriteAsset;
					m_spriteIndex = m_textInfo.characterInfo[m_characterCount].spriteIndex;
					TMP_SpriteCharacter tMP_SpriteCharacter = m_currentSpriteAsset.spriteCharacterTable[m_spriteIndex];
					if (tMP_SpriteCharacter == null)
					{
						continue;
					}
					if (num4 == 60)
					{
						num4 = 57344 + m_spriteIndex;
					}
					else
					{
						m_spriteColor = TMP_Text.s_colorWhite;
					}
					float num18 = m_currentFontSize / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
					num2 = m_currentFontAsset.faceInfo.ascentLine / tMP_SpriteCharacter.glyph.metrics.height * tMP_SpriteCharacter.scale * tMP_SpriteCharacter.glyph.scale * num18;
					m_cached_TextElement = tMP_SpriteCharacter;
					m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Sprite;
					m_textInfo.characterInfo[m_characterCount].scale = num18;
					m_textInfo.characterInfo[m_characterCount].spriteAsset = m_currentSpriteAsset;
					m_textInfo.characterInfo[m_characterCount].fontAsset = m_currentFontAsset;
					m_textInfo.characterInfo[m_characterCount].materialReferenceIndex = m_currentMaterialIndex;
					m_currentMaterialIndex = currentMaterialIndex;
					num5 = 0f;
				}
				else if (m_textElementType == TMP_TextElementType.Character)
				{
					m_cached_TextElement = m_textInfo.characterInfo[m_characterCount].textElement;
					if (m_cached_TextElement == null)
					{
						continue;
					}
					m_currentFontAsset = m_textInfo.characterInfo[m_characterCount].fontAsset;
					m_currentMaterial = m_textInfo.characterInfo[m_characterCount].material;
					m_currentMaterialIndex = m_textInfo.characterInfo[m_characterCount].materialReferenceIndex;
					m_fontScale = m_currentFontSize * num17 / (float)m_currentFontAsset.faceInfo.pointSize * m_currentFontAsset.faceInfo.scale * (m_isOrthographic ? 1f : 0.1f);
					num2 = m_fontScale * m_fontScaleMultiplier * m_cached_TextElement.scale * m_cached_TextElement.glyph.scale;
					m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
					m_textInfo.characterInfo[m_characterCount].scale = num2;
					num5 = ((m_currentMaterialIndex == 0) ? m_padding : m_subTextObjects[m_currentMaterialIndex].padding);
				}
				float num19 = num2;
				if (num4 == 173)
				{
					num2 = 0f;
				}
				m_textInfo.characterInfo[m_characterCount].character = (char)num4;
				m_textInfo.characterInfo[m_characterCount].pointSize = m_currentFontSize;
				m_textInfo.characterInfo[m_characterCount].color = m_htmlColor;
				m_textInfo.characterInfo[m_characterCount].underlineColor = m_underlineColor;
				m_textInfo.characterInfo[m_characterCount].strikethroughColor = m_strikethroughColor;
				m_textInfo.characterInfo[m_characterCount].highlightColor = m_highlightColor;
				m_textInfo.characterInfo[m_characterCount].style = m_FontStyleInternal;
				TMP_GlyphValueRecord tMP_GlyphValueRecord = default(TMP_GlyphValueRecord);
				float num20 = m_characterSpacing;
				if (m_enableKerning)
				{
					if (m_characterCount < totalCharacterCount - 1)
					{
						uint glyphIndex = m_cached_TextElement.glyphIndex;
						uint glyphIndex2 = m_textInfo.characterInfo[m_characterCount + 1].textElement.glyphIndex;
						long key = new GlyphPairKey(glyphIndex, glyphIndex2).key;
						if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out var value))
						{
							tMP_GlyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
							num20 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num20);
						}
					}
					if (m_characterCount >= 1)
					{
						uint glyphIndex3 = m_textInfo.characterInfo[m_characterCount - 1].textElement.glyphIndex;
						uint glyphIndex4 = m_cached_TextElement.glyphIndex;
						long key2 = new GlyphPairKey(glyphIndex3, glyphIndex4).key;
						if (m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out var value2))
						{
							tMP_GlyphValueRecord += value2.secondAdjustmentRecord.glyphValueRecord;
							num20 = (((value2.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num20);
						}
					}
				}
				if (m_isRightToLeft)
				{
					m_xAdvance -= ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num7 + num20 + m_wordSpacing + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
					if (char.IsWhiteSpace((char)num4) || num4 == 8203)
					{
						m_xAdvance -= m_wordSpacing * num2;
					}
				}
				float num21 = 0f;
				if (m_monoSpacing != 0f)
				{
					num21 = (m_monoSpacing / 2f - (m_cached_TextElement.glyph.metrics.width / 2f + m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - m_charWidthAdjDelta);
					m_xAdvance += num21;
				}
				if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
					{
						float @float = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num6 = m_currentFontAsset.boldStyle / 4f * @float * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num6 + num5 > @float)
						{
							num5 = @float - num6;
						}
					}
					else
					{
						num6 = 0f;
					}
					num7 = 1f + m_currentFontAsset.boldSpacing * 0.01f;
				}
				else
				{
					if (m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
					{
						float float2 = m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
						num6 = m_currentFontAsset.normalStyle / 4f * float2 * m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
						if (num6 + num5 > float2)
						{
							num5 = float2 - num6;
						}
					}
					else
					{
						num6 = 0f;
					}
					num7 = 1f;
				}
				float num22 = m_currentFontAsset.faceInfo.baseline * m_fontScale * m_fontScaleMultiplier * m_currentFontAsset.faceInfo.scale;
				vector3.x = m_xAdvance + (m_cached_TextElement.glyph.metrics.horizontalBearingX - num5 - num6 + tMP_GlyphValueRecord.xPlacement) * num2 * (1f - m_charWidthAdjDelta);
				vector3.y = num22 + (m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + tMP_GlyphValueRecord.yPlacement) * num2 - m_lineOffset + m_baselineOffset;
				vector3.z = 0f;
				vector4.x = vector3.x;
				vector4.y = vector3.y - (m_cached_TextElement.glyph.metrics.height + num5 * 2f) * num2;
				vector4.z = 0f;
				vector5.x = vector4.x + (m_cached_TextElement.glyph.metrics.width + num5 * 2f + num6 * 2f) * num2 * (1f - m_charWidthAdjDelta);
				vector5.y = vector3.y;
				vector5.z = 0f;
				vector6.x = vector5.x;
				vector6.y = vector4.y;
				vector6.z = 0f;
				if (m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
				{
					float num23 = (float)(int)m_currentFontAsset.italicStyle * 0.01f;
					Vector3 vector7 = new Vector3(num23 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY + num5 + num6) * num2), 0f, 0f);
					Vector3 vector8 = new Vector3(num23 * ((m_cached_TextElement.glyph.metrics.horizontalBearingY - m_cached_TextElement.glyph.metrics.height - num5 - num6) * num2), 0f, 0f);
					vector3 += vector7;
					vector4 += vector8;
					vector5 += vector7;
					vector6 += vector8;
				}
				if (m_isFXMatrixSet)
				{
					_ = m_FXMatrix.lossyScale.x;
					_ = 1f;
					Vector3 vector9 = (vector5 + vector4) / 2f;
					vector3 = m_FXMatrix.MultiplyPoint3x4(vector3 - vector9) + vector9;
					vector4 = m_FXMatrix.MultiplyPoint3x4(vector4 - vector9) + vector9;
					vector5 = m_FXMatrix.MultiplyPoint3x4(vector5 - vector9) + vector9;
					vector6 = m_FXMatrix.MultiplyPoint3x4(vector6 - vector9) + vector9;
				}
				m_textInfo.characterInfo[m_characterCount].bottomLeft = vector4;
				m_textInfo.characterInfo[m_characterCount].topLeft = vector3;
				m_textInfo.characterInfo[m_characterCount].topRight = vector5;
				m_textInfo.characterInfo[m_characterCount].bottomRight = vector6;
				m_textInfo.characterInfo[m_characterCount].origin = m_xAdvance;
				m_textInfo.characterInfo[m_characterCount].baseLine = num22 - m_lineOffset + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].aspectRatio = (vector5.x - vector4.x) / (vector3.y - vector4.y);
				float num24 = m_currentFontAsset.faceInfo.ascentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].ascender = num24 - m_lineOffset;
				m_maxLineAscender = ((num24 > m_maxLineAscender) ? num24 : m_maxLineAscender);
				float num25 = m_currentFontAsset.faceInfo.descentLine * ((m_textElementType == TMP_TextElementType.Character) ? (num2 / num17) : m_textInfo.characterInfo[m_characterCount].scale) + m_baselineOffset;
				float num26 = (m_textInfo.characterInfo[m_characterCount].descender = num25 - m_lineOffset);
				m_maxLineDescender = ((num25 < m_maxLineDescender) ? num25 : m_maxLineDescender);
				if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
				{
					float num27 = (num24 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
					num24 = m_maxLineAscender;
					m_maxLineAscender = ((num27 > m_maxLineAscender) ? num27 : m_maxLineAscender);
					float num28 = (num25 - m_baselineOffset) / m_currentFontAsset.faceInfo.subscriptSize;
					num25 = m_maxLineDescender;
					m_maxLineDescender = ((num28 < m_maxLineDescender) ? num28 : m_maxLineDescender);
				}
				if (m_lineNumber == 0 || m_isNewPage)
				{
					m_maxAscender = ((m_maxAscender > num24) ? m_maxAscender : num24);
					m_maxCapHeight = Mathf.Max(m_maxCapHeight, m_currentFontAsset.faceInfo.capLine * num2 / num17);
				}
				if (m_lineOffset == 0f)
				{
					num14 = ((num14 > num24) ? num14 : num24);
				}
				m_textInfo.characterInfo[m_characterCount].isVisible = false;
				if (num4 == 9 || num4 == 160 || num4 == 8199 || (!char.IsWhiteSpace((char)num4) && num4 != 8203) || m_textElementType == TMP_TextElementType.Sprite)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = true;
					num13 = ((m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - m_marginLeft - m_marginRight, m_width) : (marginWidth + 0.0001f - m_marginLeft - m_marginRight));
					m_textInfo.lineInfo[m_lineNumber].marginLeft = m_marginLeft;
					bool flag9 = (m_lineJustification & (TextAlignmentOptions)16) == (TextAlignmentOptions)16 || (m_lineJustification & (TextAlignmentOptions)8) == (TextAlignmentOptions)8;
					if (Mathf.Abs(m_xAdvance) + ((!m_isRightToLeft) ? m_cached_TextElement.glyph.metrics.horizontalAdvance : 0f) * (1f - m_charWidthAdjDelta) * ((num4 != 173) ? num2 : num19) > num13 * (flag9 ? 1.05f : 1f))
					{
						num12 = m_characterCount - 1;
						if (base.enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
						{
							if (num16 == m_SavedWordWrapState.previous_WordBreak || flag6)
							{
								if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
								{
									if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
									{
										loopCountA = 0;
										m_charWidthAdjDelta += 0.01f;
										GenerateTextMesh();
										return;
									}
									m_maxFontSize = m_fontSize;
									m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
									m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
									if (loopCountA <= 20)
									{
										GenerateTextMesh();
									}
									return;
								}
								if (!m_isCharacterWrappingEnabled)
								{
									if (!flag7)
									{
										flag7 = true;
									}
									else
									{
										m_isCharacterWrappingEnabled = true;
									}
								}
								else
								{
									flag8 = true;
								}
							}
							i = RestoreWordWrappingState(ref m_SavedWordWrapState);
							num16 = i;
							if (m_TextParsingBuffer[i].unicode == 173)
							{
								m_isTextTruncated = true;
								m_TextParsingBuffer[i].unicode = 45;
								GenerateTextMesh();
								return;
							}
							if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
							{
								float num29 = m_maxLineAscender - m_startOfLineAscender;
								AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num29);
								m_lineOffset += num29;
								m_SavedWordWrapState.lineOffset = m_lineOffset;
								m_SavedWordWrapState.previousLineAscender = m_maxLineAscender;
							}
							m_isNewPage = false;
							float num30 = m_maxLineAscender - m_lineOffset;
							float num31 = m_maxLineDescender - m_lineOffset;
							m_maxDescender = ((m_maxDescender < num31) ? m_maxDescender : num31);
							if (!flag5)
							{
								num15 = m_maxDescender;
							}
							if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
							{
								flag5 = true;
							}
							m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
							m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
							m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = ((m_characterCount - 1 > 0) ? (m_characterCount - 1) : 0));
							m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
							m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
							m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
							m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num31);
							m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num30);
							m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x;
							m_textInfo.lineInfo[m_lineNumber].width = num13;
							m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num20 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
							m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
							m_textInfo.lineInfo[m_lineNumber].ascender = num30;
							m_textInfo.lineInfo[m_lineNumber].descender = num31;
							m_textInfo.lineInfo[m_lineNumber].lineHeight = num30 - num31 + num8 * num;
							m_firstCharacterOfLine = m_characterCount;
							m_lineVisibleCharacterCount = 0;
							SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount - 1);
							m_lineNumber++;
							flag4 = true;
							flag6 = true;
							if (m_lineNumber >= m_textInfo.lineInfo.Length)
							{
								ResizeLineExtents(m_lineNumber);
							}
							if (m_lineHeight == -32767f)
							{
								float num32 = m_textInfo.characterInfo[m_characterCount].ascender - m_textInfo.characterInfo[m_characterCount].baseLine;
								num9 = 0f - m_maxLineDescender + num32 + (num8 + m_lineSpacing + m_lineSpacingDelta) * num;
								m_lineOffset += num9;
								m_startOfLineAscender = num32;
							}
							else
							{
								m_lineOffset += m_lineHeight + m_lineSpacing * num;
							}
							m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
							m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
							m_xAdvance = 0f + tag_Indent;
							continue;
						}
						if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
						{
							if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
							{
								loopCountA = 0;
								m_charWidthAdjDelta += 0.01f;
								GenerateTextMesh();
								return;
							}
							m_maxFontSize = m_fontSize;
							m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
							m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
							if (loopCountA <= 20)
							{
								GenerateTextMesh();
							}
							return;
						}
						switch (m_overflowMode)
						{
						case TextOverflowModes.Overflow:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							break;
						case TextOverflowModes.Ellipsis:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							m_isTextTruncated = true;
							if (m_characterCount < 1)
							{
								m_textInfo.characterInfo[m_characterCount].isVisible = false;
								break;
							}
							m_TextParsingBuffer[i - 1].unicode = 8230;
							m_TextParsingBuffer[i].unicode = 0;
							if (m_cached_Ellipsis_Character != null)
							{
								m_textInfo.characterInfo[num12].character = '…';
								m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
								m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
								m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
								m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
							}
							else
							{
								Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
							}
							m_totalCharacterCount = num12 + 1;
							GenerateTextMesh();
							return;
						case TextOverflowModes.Truncate:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							m_textInfo.characterInfo[m_characterCount].isVisible = false;
							break;
						}
					}
					if (num4 == 9 || num4 == 160 || num4 == 8199)
					{
						m_textInfo.characterInfo[m_characterCount].isVisible = false;
						m_lastVisibleCharacterOfLine = m_characterCount;
						m_textInfo.lineInfo[m_lineNumber].spaceCount++;
						m_textInfo.spaceCount++;
						if (num4 == 160)
						{
							m_textInfo.lineInfo[m_lineNumber].controlCharacterCount++;
						}
					}
					else
					{
						Color32 vertexColor = ((!m_overrideHtmlColors) ? m_htmlColor : m_fontColor32);
						if (m_textElementType == TMP_TextElementType.Character)
						{
							SaveGlyphVertexInfo(num5, num6, vertexColor);
						}
						else if (m_textElementType == TMP_TextElementType.Sprite)
						{
							SaveSpriteVertexInfo(vertexColor);
						}
					}
					if (m_textInfo.characterInfo[m_characterCount].isVisible && num4 != 173)
					{
						if (flag4)
						{
							flag4 = false;
							m_firstVisibleCharacterOfLine = m_characterCount;
						}
						m_lineVisibleCharacterCount++;
						m_lastVisibleCharacterOfLine = m_characterCount;
					}
				}
				else if ((num4 == 10 || char.IsSeparator((char)num4)) && num4 != 173 && num4 != 8203 && num4 != 8288)
				{
					m_textInfo.lineInfo[m_lineNumber].spaceCount++;
					m_textInfo.spaceCount++;
				}
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
				{
					float num33 = m_maxLineAscender - m_startOfLineAscender;
					AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num33);
					num26 -= num33;
					m_lineOffset += num33;
					m_startOfLineAscender += num33;
					m_SavedWordWrapState.lineOffset = m_lineOffset;
					m_SavedWordWrapState.previousLineAscender = m_startOfLineAscender;
				}
				m_textInfo.characterInfo[m_characterCount].lineNumber = m_lineNumber;
				m_textInfo.characterInfo[m_characterCount].pageNumber = m_pageNumber;
				if ((num4 != 10 && num4 != 13 && num4 != 8230) || m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
				{
					m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
				}
				if (m_maxAscender - num26 > marginHeight + 0.0001f)
				{
					if (m_enableAutoSizing && m_lineSpacingDelta > m_lineSpacingMax && m_lineNumber > 0)
					{
						loopCountA = 0;
						m_lineSpacingDelta -= 1f;
						GenerateTextMesh();
						return;
					}
					if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
					{
						m_maxFontSize = m_fontSize;
						m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
						m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
						if (loopCountA <= 20)
						{
							GenerateTextMesh();
						}
						return;
					}
					if (m_firstOverflowCharacterIndex == -1)
					{
						m_firstOverflowCharacterIndex = m_characterCount;
					}
					switch (m_overflowMode)
					{
					case TextOverflowModes.Overflow:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						break;
					case TextOverflowModes.Ellipsis:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (m_lineNumber > 0)
						{
							m_TextParsingBuffer[m_textInfo.characterInfo[num12].index].unicode = 8230;
							m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
							if (m_cached_Ellipsis_Character != null)
							{
								m_textInfo.characterInfo[num12].character = '…';
								m_textInfo.characterInfo[num12].textElement = m_cached_Ellipsis_Character;
								m_textInfo.characterInfo[num12].fontAsset = m_materialReferences[0].fontAsset;
								m_textInfo.characterInfo[num12].material = m_materialReferences[0].material;
								m_textInfo.characterInfo[num12].materialReferenceIndex = 0;
							}
							else
							{
								Debug.LogWarning("Unable to use Ellipsis character since it wasn't found in the current Font Asset [" + m_fontAsset.name + "]. Consider regenerating this font asset to include the Ellipsis character (u+2026).\nNote: Warnings can be disabled in the TMP Settings file.", this);
							}
							m_totalCharacterCount = num12 + 1;
							GenerateTextMesh();
							m_isTextTruncated = true;
						}
						else
						{
							ClearMesh(updateMesh: false);
						}
						return;
					case TextOverflowModes.Truncate:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (m_lineNumber > 0)
						{
							m_TextParsingBuffer[m_textInfo.characterInfo[num12].index + 1].unicode = 0;
							m_totalCharacterCount = num12 + 1;
							GenerateTextMesh();
							m_isTextTruncated = true;
						}
						else
						{
							ClearMesh(updateMesh: false);
						}
						return;
					case TextOverflowModes.Page:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (num4 != 13 && num4 != 10)
						{
							if (i == 0)
							{
								ClearMesh();
								return;
							}
							if (num11 == i)
							{
								m_TextParsingBuffer[i].unicode = 0;
								m_isTextTruncated = true;
							}
							num11 = i;
							i = RestoreWordWrappingState(ref m_SavedLineState);
							m_isNewPage = true;
							m_xAdvance = 0f + tag_Indent;
							m_lineOffset = 0f;
							m_maxAscender = 0f;
							num14 = 0f;
							m_lineNumber++;
							m_pageNumber++;
							continue;
						}
						break;
					case TextOverflowModes.Linked:
						if (m_linkedTextComponent != null)
						{
							m_linkedTextComponent.text = base.text;
							m_linkedTextComponent.firstVisibleCharacter = m_characterCount;
							m_linkedTextComponent.ForceMeshUpdate();
						}
						if (m_lineNumber > 0)
						{
							m_TextParsingBuffer[i].unicode = 0;
							m_totalCharacterCount = m_characterCount;
							GenerateTextMesh();
							m_isTextTruncated = true;
						}
						else
						{
							ClearMesh(updateMesh: true);
						}
						return;
					}
				}
				if (num4 == 9)
				{
					float num34 = m_currentFontAsset.faceInfo.tabWidth * (float)(int)m_currentFontAsset.tabSize * num2;
					float num35 = Mathf.Ceil(m_xAdvance / num34) * num34;
					m_xAdvance = ((num35 > m_xAdvance) ? num35 : (m_xAdvance + num34));
				}
				else if (m_monoSpacing != 0f)
				{
					m_xAdvance += (m_monoSpacing - num21 + (num20 + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
					if (char.IsWhiteSpace((char)num4) || num4 == 8203)
					{
						m_xAdvance += m_wordSpacing * num2;
					}
				}
				else if (!m_isRightToLeft)
				{
					float num36 = 1f;
					if (m_isFXMatrixSet)
					{
						num36 = m_FXMatrix.lossyScale.x;
					}
					m_xAdvance += ((m_cached_TextElement.glyph.metrics.horizontalAdvance * num36 * num7 + num20 + m_currentFontAsset.normalSpacingOffset + tMP_GlyphValueRecord.xAdvance) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
					if (char.IsWhiteSpace((char)num4) || num4 == 8203)
					{
						m_xAdvance += m_wordSpacing * num2;
					}
				}
				else
				{
					m_xAdvance -= tMP_GlyphValueRecord.xAdvance * num2;
				}
				m_textInfo.characterInfo[m_characterCount].xAdvance = m_xAdvance;
				if (num4 == 13)
				{
					m_xAdvance = 0f + tag_Indent;
				}
				if (num4 == 10 || m_characterCount == totalCharacterCount - 1)
				{
					if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == -32767f && !m_isNewPage)
					{
						float num37 = m_maxLineAscender - m_startOfLineAscender;
						AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num37);
						num26 -= num37;
						m_lineOffset += num37;
					}
					m_isNewPage = false;
					float num38 = m_maxLineAscender - m_lineOffset;
					float num39 = m_maxLineDescender - m_lineOffset;
					m_maxDescender = ((m_maxDescender < num39) ? m_maxDescender : num39);
					if (!flag5)
					{
						num15 = m_maxDescender;
					}
					if (m_useMaxVisibleDescender && (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines))
					{
						flag5 = true;
					}
					m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
					m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = (m_firstVisibleCharacterOfLine = ((m_firstCharacterOfLine > m_firstVisibleCharacterOfLine) ? m_firstCharacterOfLine : m_firstVisibleCharacterOfLine));
					m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = (m_lastCharacterOfLine = m_characterCount);
					m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = (m_lastVisibleCharacterOfLine = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine));
					m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
					m_textInfo.lineInfo[m_lineNumber].visibleCharacterCount = m_lineVisibleCharacterCount;
					m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num39);
					m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num38);
					m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x - num5 * num2;
					m_textInfo.lineInfo[m_lineNumber].width = num13;
					if (m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
					{
						m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
					}
					if (m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].isVisible)
					{
						m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (num20 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
					}
					else
					{
						m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastCharacterOfLine].xAdvance - (num20 + m_currentFontAsset.normalSpacingOffset) * num2 - m_cSpacing;
					}
					m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
					m_textInfo.lineInfo[m_lineNumber].ascender = num38;
					m_textInfo.lineInfo[m_lineNumber].descender = num39;
					m_textInfo.lineInfo[m_lineNumber].lineHeight = num38 - num39 + num8 * num;
					m_firstCharacterOfLine = m_characterCount + 1;
					m_lineVisibleCharacterCount = 0;
					if (num4 == 10)
					{
						SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount);
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_lineNumber++;
						flag4 = true;
						flag7 = false;
						flag6 = true;
						if (m_lineNumber >= m_textInfo.lineInfo.Length)
						{
							ResizeLineExtents(m_lineNumber);
						}
						if (m_lineHeight == -32767f)
						{
							num9 = 0f - m_maxLineDescender + num24 + (num8 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * num;
							m_lineOffset += num9;
						}
						else
						{
							m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
						}
						m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						m_startOfLineAscender = num24;
						m_xAdvance = 0f + tag_LineIndent + tag_Indent;
						num12 = m_characterCount - 1;
						m_characterCount++;
						continue;
					}
				}
				if (m_textInfo.characterInfo[m_characterCount].isVisible)
				{
					m_meshExtents.min.x = Mathf.Min(m_meshExtents.min.x, m_textInfo.characterInfo[m_characterCount].bottomLeft.x);
					m_meshExtents.min.y = Mathf.Min(m_meshExtents.min.y, m_textInfo.characterInfo[m_characterCount].bottomLeft.y);
					m_meshExtents.max.x = Mathf.Max(m_meshExtents.max.x, m_textInfo.characterInfo[m_characterCount].topRight.x);
					m_meshExtents.max.y = Mathf.Max(m_meshExtents.max.y, m_textInfo.characterInfo[m_characterCount].topRight.y);
				}
				if (m_overflowMode == TextOverflowModes.Page && num4 != 13 && num4 != 10)
				{
					if (m_pageNumber + 1 > m_textInfo.pageInfo.Length)
					{
						TMP_TextInfo.Resize(ref m_textInfo.pageInfo, m_pageNumber + 1, isBlockAllocated: true);
					}
					m_textInfo.pageInfo[m_pageNumber].ascender = num14;
					m_textInfo.pageInfo[m_pageNumber].descender = ((num25 < m_textInfo.pageInfo[m_pageNumber].descender) ? num25 : m_textInfo.pageInfo[m_pageNumber].descender);
					if (m_pageNumber == 0 && m_characterCount == 0)
					{
						m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
					}
					else if (m_characterCount > 0 && m_pageNumber != m_textInfo.characterInfo[m_characterCount - 1].pageNumber)
					{
						m_textInfo.pageInfo[m_pageNumber - 1].lastCharacterIndex = m_characterCount - 1;
						m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
					}
					else if (m_characterCount == totalCharacterCount - 1)
					{
						m_textInfo.pageInfo[m_pageNumber].lastCharacterIndex = m_characterCount;
					}
				}
				if (m_enableWordWrapping || m_overflowMode == TextOverflowModes.Truncate || m_overflowMode == TextOverflowModes.Ellipsis)
				{
					if ((char.IsWhiteSpace((char)num4) || num4 == 8203 || num4 == 45 || num4 == 173) && (!m_isNonBreakingSpace || flag7) && num4 != 160 && num4 != 8199 && num4 != 8209 && num4 != 8239 && num4 != 8288)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag6 = false;
					}
					else if (((num4 > 4352 && num4 < 4607) || (num4 > 11904 && num4 < 40959) || (num4 > 43360 && num4 < 43391) || (num4 > 44032 && num4 < 55295) || (num4 > 63744 && num4 < 64255) || (num4 > 65072 && num4 < 65103) || (num4 > 65280 && num4 < 65519)) && !m_isNonBreakingSpace)
					{
						if (flag6 || flag8 || (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num4) && m_characterCount < totalCharacterCount - 1 && !TMP_Settings.linebreakingRules.followingCharacters.ContainsKey(m_textInfo.characterInfo[m_characterCount + 1].character)))
						{
							SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
							m_isCharacterWrappingEnabled = false;
							flag6 = false;
						}
					}
					else if (flag6 || m_isCharacterWrappingEnabled || flag8)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					}
				}
				m_characterCount++;
			}
			num3 = m_maxFontSize - m_minFontSize;
			if (!m_isCharacterWrappingEnabled && m_enableAutoSizing && num3 > 0.051f && m_fontSize < m_fontSizeMax)
			{
				m_minFontSize = m_fontSize;
				m_fontSize += Mathf.Max((m_maxFontSize - m_fontSize) / 2f, 0.05f);
				m_fontSize = (float)(int)(Mathf.Min(m_fontSize, m_fontSizeMax) * 20f + 0.5f) / 20f;
				if (loopCountA <= 20)
				{
					GenerateTextMesh();
				}
				return;
			}
			m_isCharacterWrappingEnabled = false;
			if (m_characterCount == 0)
			{
				ClearMesh(updateMesh: true);
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			int index = m_materialReferences[0].referenceCount * ((!m_isVolumetricText) ? 4 : 8);
			m_textInfo.meshInfo[0].Clear(uploadChanges: false);
			Vector3 vector10 = Vector3.zero;
			Vector3[] rectTransformCorners = m_RectTransformCorners;
			switch (m_textAlignment)
			{
			case TextAlignmentOptions.TopLeft:
			case TextAlignmentOptions.Top:
			case TextAlignmentOptions.TopRight:
			case TextAlignmentOptions.TopJustified:
			case TextAlignmentOptions.TopFlush:
			case TextAlignmentOptions.TopGeoAligned:
				vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].ascender - vector2.y, 0f)) : (rectTransformCorners[1] + new Vector3(0f + vector2.x, 0f - m_maxAscender - vector2.y, 0f)));
				break;
			case TextAlignmentOptions.Left:
			case TextAlignmentOptions.Center:
			case TextAlignmentOptions.Right:
			case TextAlignmentOptions.Justified:
			case TextAlignmentOptions.Flush:
			case TextAlignmentOptions.CenterGeoAligned:
				vector10 = ((m_overflowMode == TextOverflowModes.Page) ? ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_textInfo.pageInfo[num10].ascender + vector2.y + m_textInfo.pageInfo[num10].descender - vector2.w) / 2f, 0f)) : ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxAscender + vector2.y + num15 - vector2.w) / 2f, 0f)));
				break;
			case TextAlignmentOptions.BottomLeft:
			case TextAlignmentOptions.Bottom:
			case TextAlignmentOptions.BottomRight:
			case TextAlignmentOptions.BottomJustified:
			case TextAlignmentOptions.BottomFlush:
			case TextAlignmentOptions.BottomGeoAligned:
				vector10 = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - m_textInfo.pageInfo[num10].descender + vector2.w, 0f)) : (rectTransformCorners[0] + new Vector3(0f + vector2.x, 0f - num15 + vector2.w, 0f)));
				break;
			case TextAlignmentOptions.BaselineLeft:
			case TextAlignmentOptions.Baseline:
			case TextAlignmentOptions.BaselineRight:
			case TextAlignmentOptions.BaselineJustified:
			case TextAlignmentOptions.BaselineFlush:
			case TextAlignmentOptions.BaselineGeoAligned:
				vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f, 0f);
				break;
			case TextAlignmentOptions.MidlineLeft:
			case TextAlignmentOptions.Midline:
			case TextAlignmentOptions.MidlineRight:
			case TextAlignmentOptions.MidlineJustified:
			case TextAlignmentOptions.MidlineFlush:
			case TextAlignmentOptions.MidlineGeoAligned:
				vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_meshExtents.max.y + vector2.y + m_meshExtents.min.y - vector2.w) / 2f, 0f);
				break;
			case TextAlignmentOptions.CaplineLeft:
			case TextAlignmentOptions.Capline:
			case TextAlignmentOptions.CaplineRight:
			case TextAlignmentOptions.CaplineJustified:
			case TextAlignmentOptions.CaplineFlush:
			case TextAlignmentOptions.CaplineGeoAligned:
				vector10 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + vector2.x, 0f - (m_maxCapHeight - vector2.y - vector2.w) / 2f, 0f);
				break;
			}
			Vector3 vector11 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			int index_X = 0;
			int index_X2 = 0;
			int num40 = 0;
			int lineCount = 0;
			int num41 = 0;
			bool flag10 = false;
			bool flag11 = false;
			int num42 = 0;
			int num43 = 0;
			float f = (m_previousLossyScaleY = transform.lossyScale.y);
			Color32 color = Color.white;
			Color32 underlineColor = Color.white;
			Color32 color2 = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
			float num44 = 0f;
			float num45 = 0f;
			float num46 = 0f;
			float num47 = 0f;
			float num48 = 0f;
			float num49 = TMP_Text.k_LargePositiveFloat;
			int num50 = 0;
			float num51 = 0f;
			float num52 = 0f;
			float b = 0f;
			TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
			for (int j = 0; j < m_characterCount; j++)
			{
				TMP_FontAsset fontAsset = characterInfo[j].fontAsset;
				char character = characterInfo[j].character;
				int lineNumber = characterInfo[j].lineNumber;
				TMP_LineInfo tMP_LineInfo = m_textInfo.lineInfo[lineNumber];
				lineCount = lineNumber + 1;
				TextAlignmentOptions textAlignmentOptions = tMP_LineInfo.alignment;
				switch (textAlignmentOptions)
				{
				case TextAlignmentOptions.TopLeft:
				case TextAlignmentOptions.Left:
				case TextAlignmentOptions.BottomLeft:
				case TextAlignmentOptions.BaselineLeft:
				case TextAlignmentOptions.MidlineLeft:
				case TextAlignmentOptions.CaplineLeft:
					vector11 = (m_isRightToLeft ? new Vector3(0f - tMP_LineInfo.maxAdvance, 0f, 0f) : new Vector3(0f + tMP_LineInfo.marginLeft, 0f, 0f));
					break;
				case TextAlignmentOptions.Top:
				case TextAlignmentOptions.Center:
				case TextAlignmentOptions.Bottom:
				case TextAlignmentOptions.Baseline:
				case TextAlignmentOptions.Midline:
				case TextAlignmentOptions.Capline:
					vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - tMP_LineInfo.maxAdvance / 2f, 0f, 0f);
					break;
				case TextAlignmentOptions.TopGeoAligned:
				case TextAlignmentOptions.CenterGeoAligned:
				case TextAlignmentOptions.BottomGeoAligned:
				case TextAlignmentOptions.BaselineGeoAligned:
				case TextAlignmentOptions.MidlineGeoAligned:
				case TextAlignmentOptions.CaplineGeoAligned:
					vector11 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - (tMP_LineInfo.lineExtents.min.x + tMP_LineInfo.lineExtents.max.x) / 2f, 0f, 0f);
					break;
				case TextAlignmentOptions.TopRight:
				case TextAlignmentOptions.Right:
				case TextAlignmentOptions.BottomRight:
				case TextAlignmentOptions.BaselineRight:
				case TextAlignmentOptions.MidlineRight:
				case TextAlignmentOptions.CaplineRight:
					vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width - tMP_LineInfo.maxAdvance, 0f, 0f));
					break;
				case TextAlignmentOptions.TopJustified:
				case TextAlignmentOptions.TopFlush:
				case TextAlignmentOptions.Justified:
				case TextAlignmentOptions.Flush:
				case TextAlignmentOptions.BottomJustified:
				case TextAlignmentOptions.BottomFlush:
				case TextAlignmentOptions.BaselineJustified:
				case TextAlignmentOptions.BaselineFlush:
				case TextAlignmentOptions.MidlineJustified:
				case TextAlignmentOptions.MidlineFlush:
				case TextAlignmentOptions.CaplineJustified:
				case TextAlignmentOptions.CaplineFlush:
				{
					if (character == '\u00ad' || character == '\u200b' || character == '\u2060')
					{
						break;
					}
					char character2 = characterInfo[tMP_LineInfo.lastCharacterIndex].character;
					bool flag12 = (textAlignmentOptions & (TextAlignmentOptions)16) == (TextAlignmentOptions)16;
					if ((!char.IsControl(character2) && lineNumber < m_lineNumber) || flag12 || tMP_LineInfo.maxAdvance > tMP_LineInfo.width)
					{
						if (lineNumber != num41 || j == 0 || j == m_firstVisibleCharacter)
						{
							vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
							flag10 = (char.IsSeparator(character) ? true : false);
							break;
						}
						float num53 = ((!m_isRightToLeft) ? (tMP_LineInfo.width - tMP_LineInfo.maxAdvance) : (tMP_LineInfo.width + tMP_LineInfo.maxAdvance));
						int num54 = tMP_LineInfo.visibleCharacterCount - 1 + tMP_LineInfo.controlCharacterCount;
						int num55 = (characterInfo[tMP_LineInfo.lastCharacterIndex].isVisible ? tMP_LineInfo.spaceCount : (tMP_LineInfo.spaceCount - 1)) - tMP_LineInfo.controlCharacterCount;
						if (flag10)
						{
							num55--;
							num54++;
						}
						float num56 = ((num55 > 0) ? m_wordWrappingRatios : 1f);
						if (num55 < 1)
						{
							num55 = 1;
						}
						if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
						{
							if (!m_isRightToLeft)
							{
								vector11 += new Vector3(num53 * (1f - num56) / (float)num55, 0f, 0f);
							}
							else
							{
								vector11 -= new Vector3(num53 * (1f - num56) / (float)num55, 0f, 0f);
							}
						}
						else if (!m_isRightToLeft)
						{
							vector11 += new Vector3(num53 * num56 / (float)num54, 0f, 0f);
						}
						else
						{
							vector11 -= new Vector3(num53 * num56 / (float)num54, 0f, 0f);
						}
					}
					else
					{
						vector11 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
					}
					break;
				}
				}
				zero3 = vector10 + vector11;
				if (characterInfo[j].isVisible)
				{
					TMP_TextElementType elementType = characterInfo[j].elementType;
					switch (elementType)
					{
					case TMP_TextElementType.Character:
					{
						Extents lineExtents = tMP_LineInfo.lineExtents;
						float num57 = m_uvLineOffset * (float)lineNumber % 1f;
						switch (m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.x = 0f;
							characterInfo[j].vertex_TL.uv2.x = 0f;
							characterInfo[j].vertex_TR.uv2.x = 1f;
							characterInfo[j].vertex_BR.uv2.x = 1f;
							break;
						case TextureMappingOptions.Line:
							if (m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num57;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num57;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num57;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num57;
							}
							else
							{
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector11.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num57;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfo[j].vertex_BL.uv2.y = 0f;
								characterInfo[j].vertex_TL.uv2.y = 1f;
								characterInfo[j].vertex_TR.uv2.y = 0f;
								characterInfo[j].vertex_BR.uv2.y = 1f;
								break;
							case TextureMappingOptions.Line:
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num57;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num57;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num57;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num57;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num58 = (1f - (characterInfo[j].vertex_BL.uv2.y + characterInfo[j].vertex_TL.uv2.y) * characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.x = characterInfo[j].vertex_BL.uv2.y * characterInfo[j].aspectRatio + num58 + num57;
							characterInfo[j].vertex_TL.uv2.x = characterInfo[j].vertex_BL.uv2.x;
							characterInfo[j].vertex_TR.uv2.x = characterInfo[j].vertex_TL.uv2.y * characterInfo[j].aspectRatio + num58 + num57;
							characterInfo[j].vertex_BR.uv2.x = characterInfo[j].vertex_TR.uv2.x;
							break;
						}
						}
						switch (m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.y = 0f;
							characterInfo[j].vertex_TL.uv2.y = 1f;
							characterInfo[j].vertex_TR.uv2.y = 1f;
							characterInfo[j].vertex_BR.uv2.y = 0f;
							break;
						case TextureMappingOptions.Line:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender);
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y);
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num59 = (1f - (characterInfo[j].vertex_BL.uv2.x + characterInfo[j].vertex_TR.uv2.x) / characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.y = num59 + characterInfo[j].vertex_BL.uv2.x / characterInfo[j].aspectRatio;
							characterInfo[j].vertex_TL.uv2.y = num59 + characterInfo[j].vertex_TR.uv2.x / characterInfo[j].aspectRatio;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						}
						}
						num44 = characterInfo[j].scale * Mathf.Abs(f) * (1f - m_charWidthAdjDelta);
						if (!characterInfo[j].isUsingAlternateTypeface && (characterInfo[j].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num44 *= -1f;
						}
						float x = characterInfo[j].vertex_BL.uv2.x;
						float y = characterInfo[j].vertex_BL.uv2.y;
						float x2 = characterInfo[j].vertex_TR.uv2.x;
						float y2 = characterInfo[j].vertex_TR.uv2.y;
						float num60 = (int)x;
						float num61 = (int)y;
						x -= num60;
						x2 -= num60;
						y -= num61;
						y2 -= num61;
						characterInfo[j].vertex_BL.uv2.x = PackUV(x, y);
						characterInfo[j].vertex_BL.uv2.y = num44;
						characterInfo[j].vertex_TL.uv2.x = PackUV(x, y2);
						characterInfo[j].vertex_TL.uv2.y = num44;
						characterInfo[j].vertex_TR.uv2.x = PackUV(x2, y2);
						characterInfo[j].vertex_TR.uv2.y = num44;
						characterInfo[j].vertex_BR.uv2.x = PackUV(x2, y);
						characterInfo[j].vertex_BR.uv2.y = num44;
						break;
					}
					}
					if (j < m_maxVisibleCharacters && num40 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode != TextOverflowModes.Page)
					{
						characterInfo[j].vertex_BL.position += zero3;
						characterInfo[j].vertex_TL.position += zero3;
						characterInfo[j].vertex_TR.position += zero3;
						characterInfo[j].vertex_BR.position += zero3;
					}
					else if (j < m_maxVisibleCharacters && num40 < m_maxVisibleWords && lineNumber < m_maxVisibleLines && m_overflowMode == TextOverflowModes.Page && characterInfo[j].pageNumber == num10)
					{
						characterInfo[j].vertex_BL.position += zero3;
						characterInfo[j].vertex_TL.position += zero3;
						characterInfo[j].vertex_TR.position += zero3;
						characterInfo[j].vertex_BR.position += zero3;
					}
					else
					{
						characterInfo[j].vertex_BL.position = Vector3.zero;
						characterInfo[j].vertex_TL.position = Vector3.zero;
						characterInfo[j].vertex_TR.position = Vector3.zero;
						characterInfo[j].vertex_BR.position = Vector3.zero;
						characterInfo[j].isVisible = false;
					}
					switch (elementType)
					{
					case TMP_TextElementType.Character:
						FillCharacterVertexBuffers(j, index_X, m_isVolumetricText);
						break;
					case TMP_TextElementType.Sprite:
						FillSpriteVertexBuffers(j, index_X2);
						break;
					}
				}
				m_textInfo.characterInfo[j].bottomLeft += zero3;
				m_textInfo.characterInfo[j].topLeft += zero3;
				m_textInfo.characterInfo[j].topRight += zero3;
				m_textInfo.characterInfo[j].bottomRight += zero3;
				m_textInfo.characterInfo[j].origin += zero3.x;
				m_textInfo.characterInfo[j].xAdvance += zero3.x;
				m_textInfo.characterInfo[j].ascender += zero3.y;
				m_textInfo.characterInfo[j].descender += zero3.y;
				m_textInfo.characterInfo[j].baseLine += zero3.y;
				if (lineNumber != num41 || j == m_characterCount - 1)
				{
					if (lineNumber != num41)
					{
						m_textInfo.lineInfo[num41].baseline += zero3.y;
						m_textInfo.lineInfo[num41].ascender += zero3.y;
						m_textInfo.lineInfo[num41].descender += zero3.y;
						m_textInfo.lineInfo[num41].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num41].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[num41].descender);
						m_textInfo.lineInfo[num41].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num41].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[num41].ascender);
					}
					if (j == m_characterCount - 1)
					{
						m_textInfo.lineInfo[lineNumber].baseline += zero3.y;
						m_textInfo.lineInfo[lineNumber].ascender += zero3.y;
						m_textInfo.lineInfo[lineNumber].descender += zero3.y;
						m_textInfo.lineInfo[lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[lineNumber].descender);
						m_textInfo.lineInfo[lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[lineNumber].ascender);
					}
				}
				if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
				{
					if (!flag11)
					{
						flag11 = true;
						num42 = j;
					}
					if (flag11 && j == m_characterCount - 1)
					{
						int num62 = m_textInfo.wordInfo.Length;
						int wordCount = m_textInfo.wordCount;
						if (m_textInfo.wordCount + 1 > num62)
						{
							TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num62 + 1);
						}
						num43 = j;
						m_textInfo.wordInfo[wordCount].firstCharacterIndex = num42;
						m_textInfo.wordInfo[wordCount].lastCharacterIndex = num43;
						m_textInfo.wordInfo[wordCount].characterCount = num43 - num42 + 1;
						m_textInfo.wordInfo[wordCount].textComponent = this;
						num40++;
						m_textInfo.wordCount++;
						m_textInfo.lineInfo[lineNumber].wordCount++;
					}
				}
				else if ((flag11 || (j == 0 && (!char.IsPunctuation(character) || char.IsWhiteSpace(character) || character == '\u200b' || j == m_characterCount - 1))) && (j <= 0 || j >= characterInfo.Length - 1 || j >= m_characterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(characterInfo[j - 1].character) || !char.IsLetterOrDigit(characterInfo[j + 1].character)))
				{
					num43 = ((j == m_characterCount - 1 && char.IsLetterOrDigit(character)) ? j : (j - 1));
					flag11 = false;
					int num63 = m_textInfo.wordInfo.Length;
					int wordCount2 = m_textInfo.wordCount;
					if (m_textInfo.wordCount + 1 > num63)
					{
						TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num63 + 1);
					}
					m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num42;
					m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num43;
					m_textInfo.wordInfo[wordCount2].characterCount = num43 - num42 + 1;
					m_textInfo.wordInfo[wordCount2].textComponent = this;
					num40++;
					m_textInfo.wordCount++;
					m_textInfo.lineInfo[lineNumber].wordCount++;
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag13 = true;
					int pageNumber = m_textInfo.characterInfo[j].pageNumber;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != m_pageToDisplay))
					{
						flag13 = false;
					}
					if (!char.IsWhiteSpace(character) && character != '\u200b')
					{
						num48 = Mathf.Max(num48, m_textInfo.characterInfo[j].scale);
						num45 = Mathf.Max(num45, Mathf.Abs(num44));
						num49 = Mathf.Min((pageNumber == num50) ? num49 : TMP_Text.k_LargePositiveFloat, m_textInfo.characterInfo[j].baseLine + base.font.faceInfo.underlineOffset * num48);
						num50 = pageNumber;
					}
					if (!flag && flag13 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag = true;
						num46 = m_textInfo.characterInfo[j].scale;
						if (num48 == 0f)
						{
							num48 = num46;
							num45 = num44;
						}
						start = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, num49, 0f);
						color = m_textInfo.characterInfo[j].underlineColor;
					}
					if (flag && m_characterCount == 1)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num49, 0f);
						num47 = m_textInfo.characterInfo[j].scale;
						DrawUnderlineMesh(start, zero, ref index, num46, num47, num48, num45, color);
						num48 = 0f;
						num45 = 0f;
						num49 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
					{
						if (char.IsWhiteSpace(character) || character == '\u200b')
						{
							int lastVisibleCharacterIndex = tMP_LineInfo.lastVisibleCharacterIndex;
							zero = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num49, 0f);
							num47 = m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num49, 0f);
							num47 = m_textInfo.characterInfo[j].scale;
						}
						flag = false;
						DrawUnderlineMesh(start, zero, ref index, num46, num47, num48, num45, color);
						num48 = 0f;
						num45 = 0f;
						num49 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && !flag13)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num49, 0f);
						num47 = m_textInfo.characterInfo[j - 1].scale;
						DrawUnderlineMesh(start, zero, ref index, num46, num47, num48, num45, color);
						num48 = 0f;
						num45 = 0f;
						num49 = TMP_Text.k_LargePositiveFloat;
					}
					else if (flag && j < m_characterCount - 1 && !color.Compare(m_textInfo.characterInfo[j + 1].underlineColor))
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num49, 0f);
						num47 = m_textInfo.characterInfo[j].scale;
						DrawUnderlineMesh(start, zero, ref index, num46, num47, num48, num45, color);
						num48 = 0f;
						num45 = 0f;
						num49 = TMP_Text.k_LargePositiveFloat;
					}
				}
				else if (flag)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num49, 0f);
					num47 = m_textInfo.characterInfo[j - 1].scale;
					DrawUnderlineMesh(start, zero, ref index, num46, num47, num48, num45, color);
					num48 = 0f;
					num45 = 0f;
					num49 = TMP_Text.k_LargePositiveFloat;
				}
				bool num64 = (m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = fontAsset.faceInfo.strikethroughOffset;
				if (num64)
				{
					bool flag14 = true;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && m_textInfo.characterInfo[j].pageNumber + 1 != m_pageToDisplay))
					{
						flag14 = false;
					}
					if (!flag2 && flag14 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag2 = true;
						num51 = m_textInfo.characterInfo[j].pointSize;
						num52 = m_textInfo.characterInfo[j].scale;
						start2 = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num52, 0f);
						underlineColor = m_textInfo.characterInfo[j].strikethroughColor;
						b = m_textInfo.characterInfo[j].baseLine;
					}
					if (flag2 && m_characterCount == 1)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num52, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
					}
					else if (flag2 && j == tMP_LineInfo.lastCharacterIndex)
					{
						if (char.IsWhiteSpace(character) || character == '\u200b')
						{
							int lastVisibleCharacterIndex2 = tMP_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num52, 0f);
						}
						else
						{
							zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num52, 0f);
						}
						flag2 = false;
						DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
					}
					else if (flag2 && j < m_characterCount && (m_textInfo.characterInfo[j + 1].pointSize != num51 || !TMP_Math.Approximately(m_textInfo.characterInfo[j + 1].baseLine + zero3.y, b)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tMP_LineInfo.lastVisibleCharacterIndex;
						zero2 = ((j <= lastVisibleCharacterIndex3) ? new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num52, 0f) : new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num52, 0f));
						DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
					}
					else if (flag2 && j < m_characterCount && fontAsset.GetInstanceID() != characterInfo[j + 1].fontAsset.GetInstanceID())
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + strikethroughOffset * num52, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
					}
					else if (flag2 && !flag14)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num52, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * num52, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num52, num52, num52, num44, underlineColor);
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Highlight) == FontStyles.Highlight)
				{
					bool flag15 = true;
					int pageNumber2 = m_textInfo.characterInfo[j].pageNumber;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber2 + 1 != m_pageToDisplay))
					{
						flag15 = false;
					}
					if (!flag3 && flag15 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
					{
						flag3 = true;
						start3 = TMP_Text.k_LargePositiveVector2;
						vector = TMP_Text.k_LargeNegativeVector2;
						color2 = m_textInfo.characterInfo[j].highlightColor;
					}
					if (flag3)
					{
						Color32 highlightColor = m_textInfo.characterInfo[j].highlightColor;
						bool flag16 = false;
						if (!color2.Compare(highlightColor))
						{
							vector.x = (vector.x + m_textInfo.characterInfo[j].bottomLeft.x) / 2f;
							start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
							vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
							DrawTextHighlight(start3, vector, ref index, color2);
							flag3 = true;
							start3 = vector;
							vector = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].descender, 0f);
							color2 = m_textInfo.characterInfo[j].highlightColor;
							flag16 = true;
						}
						if (!flag16)
						{
							start3.x = Mathf.Min(start3.x, m_textInfo.characterInfo[j].bottomLeft.x);
							start3.y = Mathf.Min(start3.y, m_textInfo.characterInfo[j].descender);
							vector.x = Mathf.Max(vector.x, m_textInfo.characterInfo[j].topRight.x);
							vector.y = Mathf.Max(vector.y, m_textInfo.characterInfo[j].ascender);
						}
					}
					if (flag3 && m_characterCount == 1)
					{
						flag3 = false;
						DrawTextHighlight(start3, vector, ref index, color2);
					}
					else if (flag3 && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
					{
						flag3 = false;
						DrawTextHighlight(start3, vector, ref index, color2);
					}
					else if (flag3 && !flag15)
					{
						flag3 = false;
						DrawTextHighlight(start3, vector, ref index, color2);
					}
				}
				else if (flag3)
				{
					flag3 = false;
					DrawTextHighlight(start3, vector, ref index, color2);
				}
				num41 = lineNumber;
			}
			m_textInfo.characterCount = m_characterCount;
			m_textInfo.spriteCount = m_spriteCount;
			m_textInfo.lineCount = lineCount;
			m_textInfo.wordCount = ((num40 == 0 || m_characterCount <= 0) ? 1 : num40);
			m_textInfo.pageCount = m_pageNumber + 1;
			if (m_renderMode == TextRenderFlags.Render && IsActive())
			{
				if (m_geometrySortingOrder != 0)
				{
					m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
				}
				m_mesh.MarkDynamic();
				m_mesh.vertices = m_textInfo.meshInfo[0].vertices;
				m_mesh.uv = m_textInfo.meshInfo[0].uvs0;
				m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
				m_mesh.colors32 = m_textInfo.meshInfo[0].colors32;
				m_mesh.RecalculateBounds();
				for (int k = 1; k < m_textInfo.materialCount; k++)
				{
					m_textInfo.meshInfo[k].ClearUnusedVertices();
					if (!(m_subTextObjects[k] == null))
					{
						if (m_geometrySortingOrder != 0)
						{
							m_textInfo.meshInfo[k].SortGeometry(VertexSortingOrder.Reverse);
						}
						m_subTextObjects[k].mesh.vertices = m_textInfo.meshInfo[k].vertices;
						m_subTextObjects[k].mesh.uv = m_textInfo.meshInfo[k].uvs0;
						m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
						m_subTextObjects[k].mesh.colors32 = m_textInfo.meshInfo[k].colors32;
						m_subTextObjects[k].mesh.RecalculateBounds();
					}
				}
			}
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (m_rectTransform == null)
			{
				m_rectTransform = base.rectTransform;
			}
			m_rectTransform.GetLocalCorners(m_RectTransformCorners);
			return m_RectTransformCorners;
		}

		private void SetMeshFilters(bool state)
		{
			if (m_meshFilter != null)
			{
				if (state)
				{
					m_meshFilter.sharedMesh = m_mesh;
				}
				else
				{
					m_meshFilter.sharedMesh = null;
				}
			}
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				if (m_subTextObjects[i].meshFilter != null)
				{
					if (state)
					{
						m_subTextObjects[i].meshFilter.sharedMesh = m_subTextObjects[i].mesh;
					}
					else
					{
						m_subTextObjects[i].meshFilter.sharedMesh = null;
					}
				}
			}
		}

		protected override void SetActiveSubMeshes(bool state)
		{
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				if (m_subTextObjects[i].enabled != state)
				{
					m_subTextObjects[i].enabled = state;
				}
			}
		}

		protected override void ClearSubMeshObjects()
		{
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				Debug.Log("Destroying Sub Text object[" + i + "].");
				UnityEngine.Object.DestroyImmediate(m_subTextObjects[i]);
			}
		}

		protected override Bounds GetCompoundBounds()
		{
			Bounds bounds = m_mesh.bounds;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			for (int i = 1; i < m_subTextObjects.Length && m_subTextObjects[i] != null; i++)
			{
				Bounds bounds2 = m_subTextObjects[i].mesh.bounds;
				min.x = ((min.x < bounds2.min.x) ? min.x : bounds2.min.x);
				min.y = ((min.y < bounds2.min.y) ? min.y : bounds2.min.y);
				max.x = ((max.x > bounds2.max.x) ? max.x : bounds2.max.x);
				max.y = ((max.y > bounds2.max.y) ? max.y : bounds2.max.y);
			}
			Vector3 center = (min + max) / 2f;
			Vector2 vector = max - min;
			return new Bounds(center, vector);
		}

		private void UpdateSDFScale(float scaleDelta)
		{
			if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity)
			{
				m_havePropertiesChanged = true;
				OnPreRenderObject();
				return;
			}
			for (int i = 0; i < m_textInfo.materialCount; i++)
			{
				TMP_MeshInfo tMP_MeshInfo = m_textInfo.meshInfo[i];
				for (int j = 0; j < tMP_MeshInfo.uvs2.Length; j++)
				{
					tMP_MeshInfo.uvs2[j].y *= Mathf.Abs(scaleDelta);
				}
			}
			for (int k = 0; k < m_textInfo.meshInfo.Length; k++)
			{
				if (k == 0)
				{
					m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
				}
				else
				{
					m_subTextObjects[k].mesh.uv2 = m_textInfo.meshInfo[k].uvs2;
				}
			}
		}

		protected override void AdjustLineOffset(int startIndex, int endIndex, float offset)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				m_textInfo.characterInfo[i].bottomLeft -= vector;
				m_textInfo.characterInfo[i].topLeft -= vector;
				m_textInfo.characterInfo[i].topRight -= vector;
				m_textInfo.characterInfo[i].bottomRight -= vector;
				m_textInfo.characterInfo[i].ascender -= vector.y;
				m_textInfo.characterInfo[i].baseLine -= vector.y;
				m_textInfo.characterInfo[i].descender -= vector.y;
				if (m_textInfo.characterInfo[i].isVisible)
				{
					m_textInfo.characterInfo[i].vertex_BL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TR.position -= vector;
					m_textInfo.characterInfo[i].vertex_BR.position -= vector;
				}
			}
		}

		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			SetMask(type);
			SetMaskCoordinates(maskCoords);
		}

		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			SetMask(type);
			SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		public override void SetVerticesDirty()
		{
			if (!m_verticesAlreadyDirty && !(this == null) && IsActive())
			{
				TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
				m_verticesAlreadyDirty = true;
			}
		}

		public override void SetLayoutDirty()
		{
			m_isPreferredWidthDirty = true;
			m_isPreferredHeightDirty = true;
			if (!m_layoutAlreadyDirty && !(this == null) && IsActive())
			{
				m_layoutAlreadyDirty = true;
				m_isLayoutDirty = true;
			}
		}

		public override void SetMaterialDirty()
		{
			UpdateMaterial();
		}

		public override void SetAllDirty()
		{
			m_isInputParsingRequired = true;
			SetLayoutDirty();
			SetVerticesDirty();
			SetMaterialDirty();
		}

		public override void Rebuild(CanvasUpdate update)
		{
			if (this == null)
			{
				return;
			}
			switch (update)
			{
			case CanvasUpdate.Prelayout:
				if (m_autoSizeTextContainer)
				{
					m_rectTransform.sizeDelta = GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
				}
				break;
			case CanvasUpdate.PreRender:
				OnPreRenderObject();
				m_verticesAlreadyDirty = false;
				m_layoutAlreadyDirty = false;
				if (m_isMaterialDirty)
				{
					UpdateMaterial();
					m_isMaterialDirty = false;
				}
				break;
			}
		}

		protected override void UpdateMaterial()
		{
			if (!(m_sharedMaterial == null))
			{
				if (m_renderer == null)
				{
					m_renderer = renderer;
				}
				if (m_renderer.sharedMaterial.GetInstanceID() != m_sharedMaterial.GetInstanceID())
				{
					m_renderer.sharedMaterial = m_sharedMaterial;
				}
			}
		}

		public override void UpdateMeshPadding()
		{
			m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_havePropertiesChanged = true;
			checkPaddingRequired = false;
			if (m_textInfo != null)
			{
				for (int i = 1; i < m_textInfo.materialCount; i++)
				{
					m_subTextObjects[i].UpdateMeshPadding(m_enableExtraPadding, m_isUsingBold);
				}
			}
		}

		public override void ForceMeshUpdate()
		{
			m_havePropertiesChanged = true;
			OnPreRenderObject();
		}

		public override void ForceMeshUpdate(bool ignoreInactive)
		{
			m_havePropertiesChanged = true;
			m_ignoreActiveState = true;
			OnPreRenderObject();
		}

		public override TMP_TextInfo GetTextInfo(string text)
		{
			StringToCharArray(text, ref m_TextParsingBuffer);
			SetArraySizes(m_TextParsingBuffer);
			m_renderMode = TextRenderFlags.DontRender;
			ComputeMarginSize();
			GenerateTextMesh();
			m_renderMode = TextRenderFlags.Render;
			return base.textInfo;
		}

		public override void ClearMesh(bool updateMesh)
		{
			if (m_textInfo.meshInfo[0].mesh == null)
			{
				m_textInfo.meshInfo[0].mesh = m_mesh;
			}
			m_textInfo.ClearMeshInfo(updateMesh);
		}

		public override void UpdateGeometry(Mesh mesh, int index)
		{
			mesh.RecalculateBounds();
		}

		public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
			int materialCount = m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh = ((i != 0) ? m_subTextObjects[i].mesh : m_mesh);
				if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
				{
					mesh.vertices = m_textInfo.meshInfo[i].vertices;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
				{
					mesh.uv = m_textInfo.meshInfo[i].uvs0;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
				{
					mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
				{
					mesh.colors32 = m_textInfo.meshInfo[i].colors32;
				}
				mesh.RecalculateBounds();
			}
		}

		public override void UpdateVertexData()
		{
			int materialCount = m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = m_mesh;
				}
				else
				{
					m_textInfo.meshInfo[i].ClearUnusedVertices();
					mesh = m_subTextObjects[i].mesh;
				}
				mesh.vertices = m_textInfo.meshInfo[i].vertices;
				mesh.uv = m_textInfo.meshInfo[i].uvs0;
				mesh.uv2 = m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
			}
		}

		public void UpdateFontAsset()
		{
			LoadFontAsset();
		}

		public void CalculateLayoutInputHorizontal()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			m_currentAutoSizeMode = m_enableAutoSizing;
			if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
			{
				m_minWidth = 0f;
				m_flexibleWidth = 0f;
				if (m_enableAutoSizing)
				{
					m_fontSize = m_fontSizeMax;
				}
				m_marginWidth = TMP_Text.k_LargePositiveFloat;
				m_marginHeight = TMP_Text.k_LargePositiveFloat;
				if (m_isInputParsingRequired || m_isTextTruncated)
				{
					ParseInputText();
				}
				GenerateTextMesh();
				m_renderMode = TextRenderFlags.Render;
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
		}

		public void CalculateLayoutInputVertical()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
			{
				m_minHeight = 0f;
				m_flexibleHeight = 0f;
				if (m_enableAutoSizing)
				{
					m_currentAutoSizeMode = true;
					m_enableAutoSizing = false;
				}
				m_marginHeight = TMP_Text.k_LargePositiveFloat;
				GenerateTextMesh();
				m_enableAutoSizing = m_currentAutoSizeMode;
				m_renderMode = TextRenderFlags.Render;
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
			m_isCalculateSizeRequired = false;
		}
	}
}
