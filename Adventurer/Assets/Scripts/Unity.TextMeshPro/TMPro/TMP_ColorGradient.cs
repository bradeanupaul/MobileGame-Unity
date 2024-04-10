using System;
using UnityEngine;

namespace TMPro
{
	[Serializable]
	public class TMP_ColorGradient : ScriptableObject
	{
		public ColorMode colorMode = ColorMode.FourCornersGradient;

		public Color topLeft;

		public Color topRight;

		public Color bottomLeft;

		public Color bottomRight;

		private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

		private static readonly Color k_DefaultColor = Color.white;

		public TMP_ColorGradient()
		{
			colorMode = ColorMode.FourCornersGradient;
			topLeft = k_DefaultColor;
			topRight = k_DefaultColor;
			bottomLeft = k_DefaultColor;
			bottomRight = k_DefaultColor;
		}

		public TMP_ColorGradient(Color color)
		{
			colorMode = ColorMode.FourCornersGradient;
			topLeft = color;
			topRight = color;
			bottomLeft = color;
			bottomRight = color;
		}

		public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			colorMode = ColorMode.FourCornersGradient;
			topLeft = color0;
			topRight = color1;
			bottomLeft = color2;
			bottomRight = color3;
		}
	}
}
