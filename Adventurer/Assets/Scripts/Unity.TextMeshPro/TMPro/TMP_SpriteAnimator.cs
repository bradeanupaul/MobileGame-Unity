using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	[DisallowMultipleComponent]
	public class TMP_SpriteAnimator : MonoBehaviour
	{
		private Dictionary<int, bool> m_animations = new Dictionary<int, bool>(16);

		private TMP_Text m_TextComponent;

		private void Awake()
		{
			m_TextComponent = GetComponent<TMP_Text>();
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void StopAllAnimations()
		{
			StopAllCoroutines();
			m_animations.Clear();
		}

		public void DoSpriteAnimation(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (!m_animations.TryGetValue(currentCharacter, out var _))
			{
				StartCoroutine(DoSpriteAnimationInternal(currentCharacter, spriteAsset, start, end, framerate));
				m_animations.Add(currentCharacter, value: true);
			}
		}

		private IEnumerator DoSpriteAnimationInternal(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (m_TextComponent == null)
			{
				yield break;
			}
			yield return null;
			int currentFrame = start;
			if (end > spriteAsset.spriteCharacterTable.Count)
			{
				end = spriteAsset.spriteCharacterTable.Count - 1;
			}
			TMP_CharacterInfo charInfo = m_TextComponent.textInfo.characterInfo[currentCharacter];
			int materialIndex = charInfo.materialReferenceIndex;
			int vertexIndex = charInfo.vertexIndex;
			TMP_MeshInfo meshInfo = m_TextComponent.textInfo.meshInfo[materialIndex];
			float elapsedTime = 0f;
			float targetTime = 1f / (float)Mathf.Abs(framerate);
			while (true)
			{
				if (elapsedTime > targetTime)
				{
					elapsedTime = 0f;
					TMP_SpriteCharacter tMP_SpriteCharacter = spriteAsset.spriteCharacterTable[currentFrame];
					Vector3[] vertices = meshInfo.vertices;
					Vector2 vector = new Vector2(charInfo.origin, charInfo.baseLine);
					float num = charInfo.fontAsset.faceInfo.ascentLine / tMP_SpriteCharacter.glyph.metrics.height * tMP_SpriteCharacter.scale * charInfo.scale;
					Vector3 vector2 = new Vector3(vector.x + tMP_SpriteCharacter.glyph.metrics.horizontalBearingX * num, vector.y + (tMP_SpriteCharacter.glyph.metrics.horizontalBearingY - tMP_SpriteCharacter.glyph.metrics.height) * num);
					Vector3 vector3 = new Vector3(vector2.x, vector.y + tMP_SpriteCharacter.glyph.metrics.horizontalBearingY * num);
					Vector3 vector4 = new Vector3(vector.x + (tMP_SpriteCharacter.glyph.metrics.horizontalBearingX + tMP_SpriteCharacter.glyph.metrics.width) * num, vector3.y);
					Vector3 vector5 = new Vector3(vector4.x, vector2.y);
					vertices[vertexIndex] = vector2;
					vertices[vertexIndex + 1] = vector3;
					vertices[vertexIndex + 2] = vector4;
					vertices[vertexIndex + 3] = vector5;
					Vector2[] uvs = meshInfo.uvs0;
					Vector2 vector6 = new Vector2((float)tMP_SpriteCharacter.glyph.glyphRect.x / (float)spriteAsset.spriteSheet.width, (float)tMP_SpriteCharacter.glyph.glyphRect.y / (float)spriteAsset.spriteSheet.height);
					Vector2 vector7 = new Vector2(vector6.x, (float)(tMP_SpriteCharacter.glyph.glyphRect.y + tMP_SpriteCharacter.glyph.glyphRect.height) / (float)spriteAsset.spriteSheet.height);
					Vector2 vector8 = new Vector2((float)(tMP_SpriteCharacter.glyph.glyphRect.x + tMP_SpriteCharacter.glyph.glyphRect.width) / (float)spriteAsset.spriteSheet.width, vector7.y);
					Vector2 vector9 = new Vector2(vector8.x, vector6.y);
					uvs[vertexIndex] = vector6;
					uvs[vertexIndex + 1] = vector7;
					uvs[vertexIndex + 2] = vector8;
					uvs[vertexIndex + 3] = vector9;
					meshInfo.mesh.vertices = vertices;
					meshInfo.mesh.uv = uvs;
					m_TextComponent.UpdateGeometry(meshInfo.mesh, materialIndex);
					currentFrame = ((framerate > 0) ? ((currentFrame >= end) ? start : (currentFrame + 1)) : ((currentFrame <= start) ? end : (currentFrame - 1)));
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}
}
