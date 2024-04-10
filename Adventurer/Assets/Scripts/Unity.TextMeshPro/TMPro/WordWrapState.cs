using UnityEngine;

namespace TMPro
{
	public struct WordWrapState
	{
		public int previous_WordBreak;

		public int total_CharacterCount;

		public int visible_CharacterCount;

		public int visible_SpriteCount;

		public int visible_LinkCount;

		public int firstCharacterIndex;

		public int firstVisibleCharacterIndex;

		public int lastCharacterIndex;

		public int lastVisibleCharIndex;

		public int lineNumber;

		public float maxCapHeight;

		public float maxAscender;

		public float maxDescender;

		public float maxLineAscender;

		public float maxLineDescender;

		public float previousLineAscender;

		public float xAdvance;

		public float preferredWidth;

		public float preferredHeight;

		public float previousLineScale;

		public int wordCount;

		public FontStyles fontStyle;

		public float fontScale;

		public float fontScaleMultiplier;

		public float currentFontSize;

		public float baselineOffset;

		public float lineOffset;

		public TMP_TextInfo textInfo;

		public TMP_LineInfo lineInfo;

		public Color32 vertexColor;

		public Color32 underlineColor;

		public Color32 strikethroughColor;

		public Color32 highlightColor;

		public TMP_FontStyleStack basicStyleStack;

		public TMP_RichTextTagStack<Color32> colorStack;

		public TMP_RichTextTagStack<Color32> underlineColorStack;

		public TMP_RichTextTagStack<Color32> strikethroughColorStack;

		public TMP_RichTextTagStack<Color32> highlightColorStack;

		public TMP_RichTextTagStack<TMP_ColorGradient> colorGradientStack;

		public TMP_RichTextTagStack<float> sizeStack;

		public TMP_RichTextTagStack<float> indentStack;

		public TMP_RichTextTagStack<FontWeight> fontWeightStack;

		public TMP_RichTextTagStack<int> styleStack;

		public TMP_RichTextTagStack<float> baselineStack;

		public TMP_RichTextTagStack<int> actionStack;

		public TMP_RichTextTagStack<MaterialReference> materialReferenceStack;

		public TMP_RichTextTagStack<TextAlignmentOptions> lineJustificationStack;

		public int spriteAnimationID;

		public TMP_FontAsset currentFontAsset;

		public TMP_SpriteAsset currentSpriteAsset;

		public Material currentMaterial;

		public int currentMaterialIndex;

		public Extents meshExtents;

		public bool tagNoParsing;

		public bool isNonBreakingSpace;
	}
}
