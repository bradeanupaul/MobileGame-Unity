using System;

namespace TMPro
{
	public struct TMP_RichTextTagStack<T>
	{
		public T[] m_ItemStack;

		public int m_Index;

		private int m_Capacity;

		private T m_DefaultItem;

		private const int k_DefaultCapacity = 4;

		public TMP_RichTextTagStack(T[] tagStack)
		{
			m_ItemStack = tagStack;
			m_Capacity = tagStack.Length;
			m_Index = 0;
			m_DefaultItem = default(T);
		}

		public TMP_RichTextTagStack(int capacity)
		{
			m_ItemStack = new T[capacity];
			m_Capacity = capacity;
			m_Index = 0;
			m_DefaultItem = default(T);
		}

		public void Clear()
		{
			m_Index = 0;
		}

		public void SetDefault(T item)
		{
			m_ItemStack[0] = item;
			m_Index = 1;
		}

		public void Add(T item)
		{
			if (m_Index < m_ItemStack.Length)
			{
				m_ItemStack[m_Index] = item;
				m_Index++;
			}
		}

		public T Remove()
		{
			m_Index--;
			if (m_Index <= 0)
			{
				m_Index = 1;
				return m_ItemStack[0];
			}
			return m_ItemStack[m_Index - 1];
		}

		public void Push(T item)
		{
			if (m_Index == m_Capacity)
			{
				m_Capacity *= 2;
				if (m_Capacity == 0)
				{
					m_Capacity = 4;
				}
				Array.Resize(ref m_ItemStack, m_Capacity);
			}
			m_ItemStack[m_Index] = item;
			m_Index++;
		}

		public T Pop()
		{
			if (m_Index == 0)
			{
				return default(T);
			}
			m_Index--;
			T result = m_ItemStack[m_Index];
			m_ItemStack[m_Index] = m_DefaultItem;
			return result;
		}

		public T Peek()
		{
			if (m_Index == 0)
			{
				return m_DefaultItem;
			}
			return m_ItemStack[m_Index - 1];
		}

		public T CurrentItem()
		{
			if (m_Index > 0)
			{
				return m_ItemStack[m_Index - 1];
			}
			return m_ItemStack[0];
		}

		public T PreviousItem()
		{
			if (m_Index > 1)
			{
				return m_ItemStack[m_Index - 2];
			}
			return m_ItemStack[0];
		}
	}
}
