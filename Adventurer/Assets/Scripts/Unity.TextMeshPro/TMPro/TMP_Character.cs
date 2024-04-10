using System;
using UnityEngine.TextCore;

namespace TMPro
{
	[Serializable]
	public class TMP_Character : TMP_TextElement
	{
		public TMP_Character()
		{
			m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		public TMP_Character(uint unicode, Glyph glyph)
		{
			m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		internal TMP_Character(uint unicode, uint glyphIndex)
		{
			m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
