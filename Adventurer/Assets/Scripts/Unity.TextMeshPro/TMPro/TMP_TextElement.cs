using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	[Serializable]
	public class TMP_TextElement
	{
		[SerializeField]
		protected TextElementType m_ElementType;

		[SerializeField]
		private uint m_Unicode;

		private Glyph m_Glyph;

		[SerializeField]
		private uint m_GlyphIndex;

		[SerializeField]
		private float m_Scale;

		public TextElementType elementType => m_ElementType;

		public uint unicode
		{
			get
			{
				return m_Unicode;
			}
			set
			{
				m_Unicode = value;
			}
		}

		public Glyph glyph
		{
			get
			{
				return m_Glyph;
			}
			set
			{
				m_Glyph = value;
			}
		}

		public uint glyphIndex
		{
			get
			{
				return m_GlyphIndex;
			}
			set
			{
				m_GlyphIndex = value;
			}
		}

		public float scale
		{
			get
			{
				return m_Scale;
			}
			set
			{
				m_Scale = value;
			}
		}
	}
}
