using System;

namespace UnityEngine.Timeline
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class MenuCategoryAttribute : Attribute
	{
		public readonly string category;

		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}
	}
}
