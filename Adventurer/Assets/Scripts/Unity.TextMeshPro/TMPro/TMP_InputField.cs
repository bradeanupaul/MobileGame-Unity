using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TMPro
{
	[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]
	public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement, IScrollHandler
	{
		public enum ContentType
		{
			Standard = 0,
			Autocorrected = 1,
			IntegerNumber = 2,
			DecimalNumber = 3,
			Alphanumeric = 4,
			Name = 5,
			EmailAddress = 6,
			Password = 7,
			Pin = 8,
			Custom = 9
		}

		public enum InputType
		{
			Standard = 0,
			AutoCorrect = 1,
			Password = 2
		}

		public enum CharacterValidation
		{
			None = 0,
			Digit = 1,
			Integer = 2,
			Decimal = 3,
			Alphanumeric = 4,
			Name = 5,
			Regex = 6,
			EmailAddress = 7,
			CustomValidator = 8
		}

		public enum LineType
		{
			SingleLine = 0,
			MultiLineSubmit = 1,
			MultiLineNewline = 2
		}

		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		[Serializable]
		public class SelectionEvent : UnityEvent<string>
		{
		}

		[Serializable]
		public class TextSelectionEvent : UnityEvent<string, int, int>
		{
		}

		[Serializable]
		public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
		{
		}

		protected enum EditState
		{
			Continue = 0,
			Finish = 1
		}

		protected TouchScreenKeyboard m_SoftKeyboard;

		private static readonly char[] kSeparators = new char[6] { ' ', '.', ',', '\t', '\r', '\n' };

		[SerializeField]
		protected RectTransform m_TextViewport;

		[SerializeField]
		protected TMP_Text m_TextComponent;

		protected RectTransform m_TextComponentRectTransform;

		[SerializeField]
		protected Graphic m_Placeholder;

		[SerializeField]
		protected Scrollbar m_VerticalScrollbar;

		[SerializeField]
		protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		private bool m_IsDrivenByLayoutComponents;

		private float m_ScrollPosition;

		[SerializeField]
		protected float m_ScrollSensitivity = 1f;

		[SerializeField]
		private ContentType m_ContentType;

		[SerializeField]
		private InputType m_InputType;

		[SerializeField]
		private char m_AsteriskChar = '*';

		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		[SerializeField]
		private LineType m_LineType;

		[SerializeField]
		private bool m_HideMobileInput;

		[SerializeField]
		private bool m_HideSoftKeyboard;

		[SerializeField]
		private CharacterValidation m_CharacterValidation;

		[SerializeField]
		private string m_RegexValue = string.Empty;

		[SerializeField]
		private float m_GlobalPointSize = 14f;

		[SerializeField]
		private int m_CharacterLimit;

		[SerializeField]
		private SubmitEvent m_OnEndEdit = new SubmitEvent();

		[SerializeField]
		private SubmitEvent m_OnSubmit = new SubmitEvent();

		[SerializeField]
		private SelectionEvent m_OnSelect = new SelectionEvent();

		[SerializeField]
		private SelectionEvent m_OnDeselect = new SelectionEvent();

		[SerializeField]
		private TextSelectionEvent m_OnTextSelection = new TextSelectionEvent();

		[SerializeField]
		private TextSelectionEvent m_OnEndTextSelection = new TextSelectionEvent();

		[SerializeField]
		private OnChangeEvent m_OnValueChanged = new OnChangeEvent();

		[SerializeField]
		private TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TouchScreenKeyboardEvent();

		[SerializeField]
		private OnValidateInput m_OnValidateInput;

		[SerializeField]
		private Color m_CaretColor = new Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

		[SerializeField]
		private bool m_CustomCaretColor;

		[SerializeField]
		private Color m_SelectionColor = new Color(56f / 85f, 0.80784315f, 1f, 64f / 85f);

		[SerializeField]
		[TextArea(5, 10)]
		protected string m_Text = string.Empty;

		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		[SerializeField]
		private bool m_ReadOnly;

		[SerializeField]
		private bool m_RichText = true;

		protected int m_StringPosition;

		protected int m_StringSelectPosition;

		protected int m_CaretPosition;

		protected int m_CaretSelectPosition;

		private RectTransform caretRectTrans;

		protected UIVertex[] m_CursorVerts;

		private CanvasRenderer m_CachedInputRenderer;

		private Vector2 m_LastPosition;

		[NonSerialized]
		protected Mesh m_Mesh;

		private bool m_AllowInput;

		private bool m_ShouldActivateNextUpdate;

		private bool m_UpdateDrag;

		private bool m_DragPositionOutOfBounds;

		private const float kHScrollSpeed = 0.05f;

		private const float kVScrollSpeed = 0.1f;

		protected bool m_CaretVisible;

		private Coroutine m_BlinkCoroutine;

		private float m_BlinkStartTime;

		private Coroutine m_DragCoroutine;

		private string m_OriginalText = "";

		private bool m_WasCanceled;

		private bool m_HasDoneFocusTransition;

		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		private bool m_PreventCallback;

		private bool m_TouchKeyboardAllowsInPlaceEditing;

		private bool m_IsTextComponentUpdateRequired;

		private bool m_IsScrollbarUpdateRequired;

		private bool m_IsUpdatingScrollbarValues;

		private bool m_isLastKeyBackspace;

		private float m_PointerDownClickStartTime;

		private float m_KeyDownStartTime;

		private float m_DoubleClickDelay = 0.5f;

		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		[SerializeField]
		protected TMP_FontAsset m_GlobalFontAsset;

		[SerializeField]
		protected bool m_OnFocusSelectAll = true;

		protected bool m_isSelectAll;

		[SerializeField]
		protected bool m_ResetOnDeActivation = true;

		private bool m_SelectionStillActive;

		private bool m_ReleaseSelection;

		private GameObject m_SelectedObject;

		[SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		[SerializeField]
		protected bool m_isRichTextEditingAllowed;

		[SerializeField]
		protected int m_LineLimit;

		[SerializeField]
		protected TMP_InputValidator m_InputValidator;

		private bool m_isSelected;

		private bool m_IsStringPositionDirty;

		private bool m_IsCaretPositionDirty;

		private bool m_forceRectTransformAdjustment;

		private Event m_ProcessingEvent = new Event();

		private BaseInput inputSystem
		{
			get
			{
				if ((bool)EventSystem.current && (bool)EventSystem.current.currentInputModule)
				{
					return EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		private string compositionString
		{
			get
			{
				if (!(inputSystem != null))
				{
					return Input.compositionString;
				}
				return inputSystem.compositionString;
			}
		}

		protected Mesh mesh
		{
			get
			{
				if (m_Mesh == null)
				{
					m_Mesh = new Mesh();
				}
				return m_Mesh;
			}
		}

		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
				{
					return m_HideMobileInput;
				}
				return true;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
				{
					SetPropertyUtility.SetStruct(ref m_HideMobileInput, value);
				}
				else
				{
					m_HideMobileInput = true;
				}
			}
		}

		public bool shouldHideSoftKeyboard
		{
			get
			{
				switch (Application.platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
				case RuntimePlatform.tvOS:
					return m_HideSoftKeyboard;
				default:
					return true;
				}
			}
			set
			{
				switch (Application.platform)
				{
				case RuntimePlatform.IPhonePlayer:
				case RuntimePlatform.Android:
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
				case RuntimePlatform.tvOS:
					SetPropertyUtility.SetStruct(ref m_HideSoftKeyboard, value);
					break;
				default:
					m_HideSoftKeyboard = true;
					break;
				}
				if (m_HideSoftKeyboard && m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && m_SoftKeyboard.active)
				{
					m_SoftKeyboard.active = false;
					m_SoftKeyboard = null;
				}
			}
		}

		public string text
		{
			get
			{
				return m_Text;
			}
			set
			{
				SetText(value);
			}
		}

		public bool isFocused => m_AllowInput;

		public float caretBlinkRate
		{
			get
			{
				return m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_CaretBlinkRate, value) && m_AllowInput)
				{
					SetCaretActive();
				}
			}
		}

		public int caretWidth
		{
			get
			{
				return m_CaretWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_CaretWidth, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public RectTransform textViewport
		{
			get
			{
				return m_TextViewport;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_TextViewport, value);
			}
		}

		public TMP_Text textComponent
		{
			get
			{
				return m_TextComponent;
			}
			set
			{
				if (SetPropertyUtility.SetClass(ref m_TextComponent, value))
				{
					SetTextComponentWrapMode();
				}
			}
		}

		public Graphic placeholder
		{
			get
			{
				return m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_Placeholder, value);
			}
		}

		public Scrollbar verticalScrollbar
		{
			get
			{
				return m_VerticalScrollbar;
			}
			set
			{
				if (m_VerticalScrollbar != null)
				{
					m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
				}
				SetPropertyUtility.SetClass(ref m_VerticalScrollbar, value);
				if ((bool)m_VerticalScrollbar)
				{
					m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
				}
			}
		}

		public float scrollSensitivity
		{
			get
			{
				return m_ScrollSensitivity;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_ScrollSensitivity, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public Color caretColor
		{
			get
			{
				if (!customCaretColor)
				{
					return textComponent.color;
				}
				return m_CaretColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref m_CaretColor, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public bool customCaretColor
		{
			get
			{
				return m_CustomCaretColor;
			}
			set
			{
				if (m_CustomCaretColor != value)
				{
					m_CustomCaretColor = value;
					MarkGeometryAsDirty();
				}
			}
		}

		public Color selectionColor
		{
			get
			{
				return m_SelectionColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref m_SelectionColor, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public SubmitEvent onEndEdit
		{
			get
			{
				return m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnEndEdit, value);
			}
		}

		public SubmitEvent onSubmit
		{
			get
			{
				return m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnSubmit, value);
			}
		}

		public SelectionEvent onSelect
		{
			get
			{
				return m_OnSelect;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnSelect, value);
			}
		}

		public SelectionEvent onDeselect
		{
			get
			{
				return m_OnDeselect;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnDeselect, value);
			}
		}

		public TextSelectionEvent onTextSelection
		{
			get
			{
				return m_OnTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnTextSelection, value);
			}
		}

		public TextSelectionEvent onEndTextSelection
		{
			get
			{
				return m_OnEndTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnEndTextSelection, value);
			}
		}

		public OnChangeEvent onValueChanged
		{
			get
			{
				return m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnValueChanged, value);
			}
		}

		public TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		public OnValidateInput onValidateInput
		{
			get
			{
				return m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass(ref m_OnValidateInput, value);
			}
		}

		public int characterLimit
		{
			get
			{
				return m_CharacterLimit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_CharacterLimit, Math.Max(0, value)))
				{
					UpdateLabel();
					if (m_SoftKeyboard != null)
					{
						m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		public float pointSize
		{
			get
			{
				return m_GlobalPointSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_GlobalPointSize, Math.Max(0f, value)))
				{
					SetGlobalPointSize(m_GlobalPointSize);
					UpdateLabel();
				}
			}
		}

		public TMP_FontAsset fontAsset
		{
			get
			{
				return m_GlobalFontAsset;
			}
			set
			{
				if (SetPropertyUtility.SetClass(ref m_GlobalFontAsset, value))
				{
					SetGlobalFontAsset(m_GlobalFontAsset);
					UpdateLabel();
				}
			}
		}

		public bool onFocusSelectAll
		{
			get
			{
				return m_OnFocusSelectAll;
			}
			set
			{
				m_OnFocusSelectAll = value;
			}
		}

		public bool resetOnDeActivation
		{
			get
			{
				return m_ResetOnDeActivation;
			}
			set
			{
				m_ResetOnDeActivation = value;
			}
		}

		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return m_RestoreOriginalTextOnEscape;
			}
			set
			{
				m_RestoreOriginalTextOnEscape = value;
			}
		}

		public bool isRichTextEditingAllowed
		{
			get
			{
				return m_isRichTextEditingAllowed;
			}
			set
			{
				m_isRichTextEditingAllowed = value;
			}
		}

		public ContentType contentType
		{
			get
			{
				return m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_ContentType, value))
				{
					EnforceContentType();
				}
			}
		}

		public LineType lineType
		{
			get
			{
				return m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_LineType, value))
				{
					SetToCustomIfContentTypeIsNot(ContentType.Standard, ContentType.Autocorrected);
					SetTextComponentWrapMode();
				}
			}
		}

		public int lineLimit
		{
			get
			{
				return m_LineLimit;
			}
			set
			{
				if (m_LineType == LineType.SingleLine)
				{
					m_LineLimit = 1;
				}
				else
				{
					SetPropertyUtility.SetStruct(ref m_LineLimit, value);
				}
			}
		}

		public InputType inputType
		{
			get
			{
				return m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_InputType, value))
				{
					SetToCustom();
				}
			}
		}

		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_KeyboardType, value))
				{
					SetToCustom();
				}
			}
		}

		public CharacterValidation characterValidation
		{
			get
			{
				return m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_CharacterValidation, value))
				{
					SetToCustom();
				}
			}
		}

		public TMP_InputValidator inputValidator
		{
			get
			{
				return m_InputValidator;
			}
			set
			{
				if (SetPropertyUtility.SetClass(ref m_InputValidator, value))
				{
					SetToCustom(CharacterValidation.CustomValidator);
				}
			}
		}

		public bool readOnly
		{
			get
			{
				return m_ReadOnly;
			}
			set
			{
				m_ReadOnly = value;
			}
		}

		public bool richText
		{
			get
			{
				return m_RichText;
			}
			set
			{
				m_RichText = value;
				SetTextComponentRichTextMode();
			}
		}

		public bool multiLine
		{
			get
			{
				if (m_LineType != LineType.MultiLineNewline)
				{
					return lineType == LineType.MultiLineSubmit;
				}
				return true;
			}
		}

		public char asteriskChar
		{
			get
			{
				return m_AsteriskChar;
			}
			set
			{
				if (SetPropertyUtility.SetStruct(ref m_AsteriskChar, value))
				{
					UpdateLabel();
				}
			}
		}

		public bool wasCanceled => m_WasCanceled;

		protected int caretPositionInternal
		{
			get
			{
				return m_CaretPosition + compositionString.Length;
			}
			set
			{
				m_CaretPosition = value;
				ClampCaretPos(ref m_CaretPosition);
			}
		}

		protected int stringPositionInternal
		{
			get
			{
				return m_StringPosition + compositionString.Length;
			}
			set
			{
				m_StringPosition = value;
				ClampStringPos(ref m_StringPosition);
			}
		}

		protected int caretSelectPositionInternal
		{
			get
			{
				return m_CaretSelectPosition + compositionString.Length;
			}
			set
			{
				m_CaretSelectPosition = value;
				ClampCaretPos(ref m_CaretSelectPosition);
			}
		}

		protected int stringSelectPositionInternal
		{
			get
			{
				return m_StringSelectPosition + compositionString.Length;
			}
			set
			{
				m_StringSelectPosition = value;
				ClampStringPos(ref m_StringSelectPosition);
			}
		}

		private bool hasSelection => stringPositionInternal != stringSelectPositionInternal;

		public int caretPosition
		{
			get
			{
				return caretSelectPositionInternal;
			}
			set
			{
				selectionAnchorPosition = value;
				selectionFocusPosition = value;
				m_IsStringPositionDirty = true;
			}
		}

		public int selectionAnchorPosition
		{
			get
			{
				return caretPositionInternal;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					caretPositionInternal = value;
					m_IsStringPositionDirty = true;
				}
			}
		}

		public int selectionFocusPosition
		{
			get
			{
				return caretSelectPositionInternal;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					caretSelectPositionInternal = value;
					m_IsStringPositionDirty = true;
				}
			}
		}

		public int stringPosition
		{
			get
			{
				return stringSelectPositionInternal;
			}
			set
			{
				selectionStringAnchorPosition = value;
				selectionStringFocusPosition = value;
				m_IsCaretPositionDirty = true;
			}
		}

		public int selectionStringAnchorPosition
		{
			get
			{
				return stringPositionInternal;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					stringPositionInternal = value;
					m_IsCaretPositionDirty = true;
				}
			}
		}

		public int selectionStringFocusPosition
		{
			get
			{
				return stringSelectPositionInternal;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					stringSelectPositionInternal = value;
					m_IsCaretPositionDirty = true;
				}
			}
		}

		private static string clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		public virtual float minWidth => 0f;

		public virtual float preferredWidth
		{
			get
			{
				if (textComponent == null)
				{
					return 0f;
				}
				return m_TextComponent.preferredWidth + 16f + (float)m_CaretWidth + 1f;
			}
		}

		public virtual float flexibleWidth => -1f;

		public virtual float minHeight => 0f;

		public virtual float preferredHeight
		{
			get
			{
				if (textComponent == null)
				{
					return 0f;
				}
				return m_TextComponent.preferredHeight + 16f;
			}
		}

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => 1;

		protected TMP_InputField()
		{
			SetTextComponentWrapMode();
		}

		private bool isKeyboardUsingEvents()
		{
			RuntimePlatform platform = Application.platform;
			if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
			{
				return false;
			}
			return true;
		}

		public void SetTextWithoutNotify(string input)
		{
			SetText(input, sendCallback: false);
		}

		private void SetText(string value, bool sendCallback = true)
		{
			if (!(text == value))
			{
				if (value == null)
				{
					value = "";
				}
				value = value.Replace("\0", string.Empty);
				m_Text = value;
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.text = m_Text;
				}
				if (m_StringPosition > m_Text.Length)
				{
					m_StringPosition = (m_StringSelectPosition = m_Text.Length);
				}
				else if (m_StringSelectPosition > m_Text.Length)
				{
					m_StringSelectPosition = m_Text.Length;
				}
				AdjustTextPositionRelativeToViewport(0f);
				m_forceRectTransformAdjustment = true;
				m_IsTextComponentUpdateRequired = true;
				UpdateLabel();
				if (sendCallback)
				{
					SendOnValueChanged();
				}
			}
		}

		protected void ClampStringPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
			}
			else if (pos > text.Length)
			{
				pos = text.Length;
			}
		}

		protected void ClampCaretPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
			}
			else if (pos > m_TextComponent.textInfo.characterCount - 1)
			{
				pos = m_TextComponent.textInfo.characterCount - 1;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_Text == null)
			{
				m_Text = string.Empty;
			}
			if (Application.isPlaying && m_CachedInputRenderer == null && m_TextComponent != null)
			{
				m_IsDrivenByLayoutComponents = GetComponent<ILayoutController>() != null;
				GameObject gameObject = new GameObject(base.transform.name + " Input Caret", typeof(RectTransform));
				TMP_SelectionCaret tMP_SelectionCaret = gameObject.AddComponent<TMP_SelectionCaret>();
				tMP_SelectionCaret.raycastTarget = false;
				tMP_SelectionCaret.color = Color.clear;
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				caretRectTrans = gameObject.GetComponent<RectTransform>();
				m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				AssignPositioningIfNeeded();
			}
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			}
			if (m_TextComponent != null)
			{
				m_TextComponent.RegisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.RegisterDirtyVerticesCallback(UpdateLabel);
				if (m_VerticalScrollbar != null)
				{
					m_TextComponent.ignoreRectMaskCulling = true;
					m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
				}
				UpdateLabel();
			}
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
		}

		protected override void OnDisable()
		{
			m_BlinkCoroutine = null;
			DeactivateInputField();
			if (m_TextComponent != null)
			{
				m_TextComponent.UnregisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.UnregisterDirtyVerticesCallback(UpdateLabel);
				if (m_VerticalScrollbar != null)
				{
					m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
				}
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.Clear();
			}
			if (m_Mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(m_Mesh);
			}
			m_Mesh = null;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
			base.OnDisable();
		}

		private void ON_TEXT_CHANGED(UnityEngine.Object obj)
		{
			if (obj == m_TextComponent && Application.isPlaying && compositionString.Length == 0)
			{
				caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			}
		}

		private IEnumerator CaretBlink()
		{
			m_CaretVisible = true;
			yield return null;
			while ((isFocused || m_SelectionStillActive) && m_CaretBlinkRate > 0f)
			{
				float num = 1f / m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - m_BlinkStartTime) % num < num / 2f;
				if (m_CaretVisible != flag)
				{
					m_CaretVisible = flag;
					if (!hasSelection)
					{
						MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			m_BlinkCoroutine = null;
		}

		private void SetCaretVisible()
		{
			if (m_AllowInput)
			{
				m_CaretVisible = true;
				m_BlinkStartTime = Time.unscaledTime;
				SetCaretActive();
			}
		}

		private void SetCaretActive()
		{
			if (!m_AllowInput)
			{
				return;
			}
			if (m_CaretBlinkRate > 0f)
			{
				if (m_BlinkCoroutine == null)
				{
					m_BlinkCoroutine = StartCoroutine(CaretBlink());
				}
			}
			else
			{
				m_CaretVisible = true;
			}
		}

		protected void OnFocus()
		{
			if (m_OnFocusSelectAll)
			{
				SelectAll();
			}
		}

		protected void SelectAll()
		{
			m_isSelectAll = true;
			stringPositionInternal = text.Length;
			stringSelectPositionInternal = 0;
		}

		public void MoveTextEnd(bool shift)
		{
			if (m_isRichTextEditingAllowed)
			{
				int length = text.Length;
				if (shift)
				{
					stringSelectPositionInternal = length;
				}
				else
				{
					stringPositionInternal = length;
					stringSelectPositionInternal = stringPositionInternal;
				}
			}
			else
			{
				int num = m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					caretSelectPositionInternal = num;
					stringSelectPositionInternal = GetStringIndexFromCaretPosition(num);
				}
				else
				{
					int num3 = (caretSelectPositionInternal = num);
					caretPositionInternal = num3;
					num3 = (stringPositionInternal = GetStringIndexFromCaretPosition(num));
					stringSelectPositionInternal = num3;
				}
			}
			UpdateLabel();
		}

		public void MoveTextStart(bool shift)
		{
			if (m_isRichTextEditingAllowed)
			{
				int num = 0;
				if (shift)
				{
					stringSelectPositionInternal = num;
				}
				else
				{
					stringPositionInternal = num;
					stringSelectPositionInternal = stringPositionInternal;
				}
			}
			else
			{
				int num2 = 0;
				if (shift)
				{
					caretSelectPositionInternal = num2;
					stringSelectPositionInternal = GetStringIndexFromCaretPosition(num2);
				}
				else
				{
					int num4 = (caretSelectPositionInternal = num2);
					caretPositionInternal = num4;
					num4 = (stringPositionInternal = GetStringIndexFromCaretPosition(num2));
					stringSelectPositionInternal = num4;
				}
			}
			UpdateLabel();
		}

		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			int num = (ctrl ? (m_TextComponent.textInfo.characterCount - 1) : m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex);
			int index = m_TextComponent.textInfo.characterInfo[num].index;
			if (shift)
			{
				stringSelectPositionInternal = index;
				caretSelectPositionInternal = num;
			}
			else
			{
				stringPositionInternal = index;
				stringSelectPositionInternal = stringPositionInternal;
				int num3 = (caretPositionInternal = num);
				caretSelectPositionInternal = num3;
			}
			UpdateLabel();
		}

		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			int num = ((!ctrl) ? m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex : 0);
			int num2 = 0;
			if (num > 0)
			{
				num2 = m_TextComponent.textInfo.characterInfo[num - 1].index + m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
			}
			if (shift)
			{
				stringSelectPositionInternal = num2;
				caretSelectPositionInternal = num;
			}
			else
			{
				stringPositionInternal = num2;
				stringSelectPositionInternal = stringPositionInternal;
				int num4 = (caretPositionInternal = num);
				caretSelectPositionInternal = num4;
			}
			UpdateLabel();
		}

		private bool InPlaceEditing()
		{
			if (m_TouchKeyboardAllowsInPlaceEditing || (TouchScreenKeyboard.isSupported && (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)))
			{
				return true;
			}
			if (TouchScreenKeyboard.isSupported && shouldHideSoftKeyboard)
			{
				return true;
			}
			if (TouchScreenKeyboard.isSupported && !shouldHideSoftKeyboard && !shouldHideMobileInput)
			{
				return false;
			}
			return true;
		}

		private void UpdateStringPositionFromKeyboard()
		{
			RangeInt selection = m_SoftKeyboard.selection;
			if (selection.start != 0 || selection.length != 0)
			{
				int start = selection.start;
				int end = selection.end;
				bool flag = false;
				if (stringPositionInternal != start)
				{
					flag = true;
					stringPositionInternal = start;
					caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
				}
				if (stringSelectPositionInternal != end)
				{
					stringSelectPositionInternal = end;
					flag = true;
					caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
				}
				if (flag)
				{
					m_BlinkStartTime = Time.unscaledTime;
					UpdateLabel();
				}
			}
		}

		protected virtual void LateUpdate()
		{
			if (m_ShouldActivateNextUpdate)
			{
				if (!isFocused)
				{
					ActivateInputFieldInternal();
					m_ShouldActivateNextUpdate = false;
					return;
				}
				m_ShouldActivateNextUpdate = false;
			}
			if (m_IsScrollbarUpdateRequired)
			{
				UpdateScrollbar();
				m_IsScrollbarUpdateRequired = false;
			}
			if (!isFocused && m_SelectionStillActive)
			{
				GameObject gameObject = ((EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null);
				if (gameObject != null && gameObject != base.gameObject)
				{
					if (gameObject != m_SelectedObject)
					{
						m_SelectedObject = gameObject;
						if (gameObject.GetComponent<TMP_InputField>() != null)
						{
							m_SelectionStillActive = false;
							MarkGeometryAsDirty();
							m_SelectedObject = null;
						}
					}
					return;
				}
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					bool flag = false;
					float unscaledTime = Time.unscaledTime;
					if (m_KeyDownStartTime + m_DoubleClickDelay > unscaledTime)
					{
						flag = true;
					}
					m_KeyDownStartTime = unscaledTime;
					if (flag)
					{
						m_SelectionStillActive = false;
						MarkGeometryAsDirty();
						return;
					}
				}
			}
			if ((InPlaceEditing() && isKeyboardUsingEvents()) || !isFocused)
			{
				return;
			}
			AssignPositioningIfNeeded();
			if (m_SoftKeyboard == null || m_SoftKeyboard.status != 0)
			{
				if (m_SoftKeyboard != null)
				{
					if (!m_ReadOnly)
					{
						this.text = m_SoftKeyboard.text;
					}
					if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
					{
						SendTouchScreenKeyboardStatusChanged();
					}
					if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						m_ReleaseSelection = true;
						m_WasCanceled = true;
						SendTouchScreenKeyboardStatusChanged();
					}
					if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Done)
					{
						m_ReleaseSelection = true;
						OnSubmit(null);
						SendTouchScreenKeyboardStatusChanged();
					}
				}
				OnDeselect(null);
				return;
			}
			string text = m_SoftKeyboard.text;
			if (m_Text != text)
			{
				if (m_ReadOnly)
				{
					m_SoftKeyboard.text = m_Text;
				}
				else
				{
					m_Text = "";
					for (int i = 0; i < text.Length; i++)
					{
						char c = text[i];
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (onValidateInput != null)
						{
							c = onValidateInput(m_Text, m_Text.Length, c);
						}
						else if (characterValidation != 0)
						{
							c = Validate(m_Text, m_Text.Length, c);
						}
						if (lineType == LineType.MultiLineSubmit && c == '\n')
						{
							m_SoftKeyboard.text = m_Text;
							OnSubmit(null);
							OnDeselect(null);
							return;
						}
						if (c != 0)
						{
							m_Text += c;
						}
					}
					if (characterLimit > 0 && m_Text.Length > characterLimit)
					{
						m_Text = m_Text.Substring(0, characterLimit);
					}
					UpdateStringPositionFromKeyboard();
					if (m_Text != text)
					{
						m_SoftKeyboard.text = m_Text;
					}
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (m_HideMobileInput && Application.platform == RuntimePlatform.Android)
			{
				UpdateStringPositionFromKeyboard();
			}
			if (m_SoftKeyboard.status != 0)
			{
				if (m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					m_WasCanceled = true;
				}
				OnDeselect(null);
			}
		}

		private bool MayDrag(PointerEventData eventData)
		{
			if (IsActive() && IsInteractable() && eventData.button == PointerEventData.InputButton.Left && m_TextComponent != null)
			{
				if (m_SoftKeyboard != null && !shouldHideSoftKeyboard)
				{
					return shouldHideMobileInput;
				}
				return true;
			}
			return false;
		}

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				m_UpdateDrag = true;
			}
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!MayDrag(eventData))
			{
				return;
			}
			CaretPosition cursor;
			int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
			if (m_isRichTextEditingAllowed)
			{
				switch (cursor)
				{
				case CaretPosition.Left:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
					break;
				case CaretPosition.Right:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					break;
				}
			}
			else
			{
				switch (cursor)
				{
				case CaretPosition.Left:
					stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
					break;
				case CaretPosition.Right:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					break;
				}
			}
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			MarkGeometryAsDirty();
			m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(textViewport, eventData.position, eventData.pressEventCamera);
			if (m_DragPositionOutOfBounds && m_DragCoroutine == null)
			{
				m_DragCoroutine = StartCoroutine(MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (m_UpdateDrag && m_DragPositionOutOfBounds)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(textViewport, eventData.position, eventData.pressEventCamera, out var localPoint);
				Rect rect = textViewport.rect;
				if (multiLine)
				{
					if (localPoint.y > rect.yMax)
					{
						MoveUp(shift: true, goToFirstChar: true);
					}
					else if (localPoint.y < rect.yMin)
					{
						MoveDown(shift: true, goToLastChar: true);
					}
				}
				else if (localPoint.x < rect.xMin)
				{
					MoveLeft(shift: true, ctrl: false);
				}
				else if (localPoint.x > rect.xMax)
				{
					MoveRight(shift: true, ctrl: false);
				}
				UpdateLabel();
				float num = (multiLine ? 0.1f : 0.05f);
				if (m_WaitForSecondsRealtime == null)
				{
					m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num);
				}
				else
				{
					m_WaitForSecondsRealtime.waitTime = num;
				}
				yield return m_WaitForSecondsRealtime;
			}
			m_DragCoroutine = null;
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				m_UpdateDrag = false;
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = m_AllowInput;
			base.OnPointerDown(eventData);
			if (!InPlaceEditing() && (m_SoftKeyboard == null || !m_SoftKeyboard.active))
			{
				OnSelect(eventData);
				return;
			}
			bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			bool flag2 = false;
			float unscaledTime = Time.unscaledTime;
			if (m_PointerDownClickStartTime + m_DoubleClickDelay > unscaledTime)
			{
				flag2 = true;
			}
			m_PointerDownClickStartTime = unscaledTime;
			if (allowInput || !m_OnFocusSelectAll)
			{
				CaretPosition cursor;
				int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
				if (flag)
				{
					if (m_isRichTextEditingAllowed)
					{
						switch (cursor)
						{
						case CaretPosition.Left:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
							break;
						case CaretPosition.Right:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
							break;
						}
					}
					else
					{
						switch (cursor)
						{
						case CaretPosition.Left:
							stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
							break;
						case CaretPosition.Right:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
							break;
						}
					}
				}
				else if (m_isRichTextEditingAllowed)
				{
					switch (cursor)
					{
					case CaretPosition.Left:
					{
						int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index);
						stringPositionInternal = num2;
						break;
					}
					case CaretPosition.Right:
					{
						int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
						stringPositionInternal = num2;
						break;
					}
					}
				}
				else
				{
					switch (cursor)
					{
					case CaretPosition.Left:
					{
						int num2 = (stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength)));
						stringPositionInternal = num2;
						break;
					}
					case CaretPosition.Right:
					{
						int num2 = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
						stringPositionInternal = num2;
						break;
					}
					}
				}
				if (flag2)
				{
					int num5 = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (num5 != -1)
					{
						caretPositionInternal = m_TextComponent.textInfo.wordInfo[num5].firstCharacterIndex;
						caretSelectPositionInternal = m_TextComponent.textInfo.wordInfo[num5].lastCharacterIndex + 1;
						stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
						stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						caretPositionInternal = cursorIndexFromPosition;
						caretSelectPositionInternal = caretPositionInternal + 1;
						stringPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						stringSelectPositionInternal = stringPositionInternal + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else
				{
					int num2 = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal));
					caretPositionInternal = num2;
				}
			}
			UpdateLabel();
			eventData.Use();
		}

		protected EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			bool flag = ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((modifiers & EventModifiers.Command) != 0) : ((modifiers & EventModifiers.Control) != 0));
			bool flag2 = (modifiers & EventModifiers.Shift) != 0;
			bool flag3 = (modifiers & EventModifiers.Alt) != 0;
			bool flag4 = flag && !flag3 && !flag2;
			switch (evt.keyCode)
			{
			case KeyCode.Backspace:
				Backspace();
				return EditState.Continue;
			case KeyCode.Delete:
				DeleteKey();
				return EditState.Continue;
			case KeyCode.Home:
				MoveToStartOfLine(flag2, flag);
				return EditState.Continue;
			case KeyCode.End:
				MoveToEndOfLine(flag2, flag);
				return EditState.Continue;
			case KeyCode.A:
				if (flag4)
				{
					SelectAll();
					return EditState.Continue;
				}
				break;
			case KeyCode.C:
				if (flag4)
				{
					if (inputType != InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					return EditState.Continue;
				}
				break;
			case KeyCode.V:
				if (flag4)
				{
					Append(clipboard);
					return EditState.Continue;
				}
				break;
			case KeyCode.X:
				if (flag4)
				{
					if (inputType != InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					Delete();
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
					return EditState.Continue;
				}
				break;
			case KeyCode.LeftArrow:
				MoveLeft(flag2, flag);
				return EditState.Continue;
			case KeyCode.RightArrow:
				MoveRight(flag2, flag);
				return EditState.Continue;
			case KeyCode.UpArrow:
				MoveUp(flag2);
				return EditState.Continue;
			case KeyCode.DownArrow:
				MoveDown(flag2);
				return EditState.Continue;
			case KeyCode.PageUp:
				MovePageUp(flag2);
				return EditState.Continue;
			case KeyCode.PageDown:
				MovePageDown(flag2);
				return EditState.Continue;
			case KeyCode.Return:
			case KeyCode.KeypadEnter:
				if (lineType != LineType.MultiLineNewline)
				{
					m_ReleaseSelection = true;
					return EditState.Finish;
				}
				break;
			case KeyCode.Escape:
				m_ReleaseSelection = true;
				m_WasCanceled = true;
				return EditState.Finish;
			}
			char c = evt.character;
			if (!multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (IsValidChar(c))
			{
				Append(c);
			}
			if (c == '\0' && compositionString.Length > 0)
			{
				UpdateLabel();
			}
			return EditState.Continue;
		}

		protected virtual bool IsValidChar(char c)
		{
			switch (c)
			{
			case '\u007f':
				return false;
			default:
				_ = 10;
				break;
			case '\t':
				break;
			}
			return true;
		}

		public void ProcessEvent(Event e)
		{
			KeyPressed(e);
		}

		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(m_ProcessingEvent))
			{
				if (m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					flag = true;
					if (KeyPressed(m_ProcessingEvent) == EditState.Finish)
					{
						SendOnSubmit();
						DeactivateInputField();
						break;
					}
				}
				EventType type = m_ProcessingEvent.type;
				if ((uint)(type - 13) <= 1u)
				{
					string commandName = m_ProcessingEvent.commandName;
					if (commandName == "SelectAll")
					{
						SelectAll();
						flag = true;
					}
				}
			}
			if (flag)
			{
				UpdateLabel();
			}
			eventData.Use();
		}

		public virtual void OnScroll(PointerEventData eventData)
		{
			if (!(m_TextComponent.preferredHeight < m_TextViewport.rect.height))
			{
				float num = 0f - eventData.scrollDelta.y;
				m_ScrollPosition += 1f / (float)m_TextComponent.textInfo.lineCount * num * m_ScrollSensitivity;
				m_ScrollPosition = Mathf.Clamp01(m_ScrollPosition);
				AdjustTextPositionRelativeToViewport(m_ScrollPosition);
				m_AllowInput = false;
				if ((bool)m_VerticalScrollbar)
				{
					m_IsUpdatingScrollbarValues = true;
					m_VerticalScrollbar.value = m_ScrollPosition;
				}
			}
		}

		private string GetSelectedString()
		{
			if (!hasSelection)
			{
				return "";
			}
			int num = stringPositionInternal;
			int num2 = stringSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return text.Substring(num, num2 - num);
		}

		private int FindNextWordBegin()
		{
			if (stringSelectPositionInternal + 1 >= text.Length)
			{
				return text.Length;
			}
			int num = text.IndexOfAny(kSeparators, stringSelectPositionInternal + 1);
			if (num == -1)
			{
				return text.Length;
			}
			return num + 1;
		}

		private void MoveRight(bool shift, bool ctrl)
		{
			int num2;
			if (hasSelection && !shift)
			{
				num2 = (stringSelectPositionInternal = Mathf.Max(stringPositionInternal, stringSelectPositionInternal));
				stringPositionInternal = num2;
				num2 = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretPositionInternal = num2;
				return;
			}
			int num3 = (ctrl ? FindNextWordBegin() : ((!m_isRichTextEditingAllowed) ? (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].stringLength) : ((stringSelectPositionInternal >= text.Length || !char.IsHighSurrogate(text[stringSelectPositionInternal])) ? (stringSelectPositionInternal + 1) : (stringSelectPositionInternal + 2))));
			if (shift)
			{
				stringSelectPositionInternal = num3;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
				return;
			}
			num2 = (stringPositionInternal = num3);
			stringSelectPositionInternal = num2;
			if (stringPositionInternal >= m_TextComponent.textInfo.characterInfo[caretPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength)
			{
				num2 = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretSelectPositionInternal = num2;
			}
		}

		private int FindPrevWordBegin()
		{
			if (stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = text.LastIndexOfAny(kSeparators, stringSelectPositionInternal - 2);
			if (num == -1)
			{
				return 0;
			}
			return num + 1;
		}

		private void MoveLeft(bool shift, bool ctrl)
		{
			int num2;
			if (hasSelection && !shift)
			{
				num2 = (stringSelectPositionInternal = Mathf.Min(stringPositionInternal, stringSelectPositionInternal));
				stringPositionInternal = num2;
				num2 = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretPositionInternal = num2;
				return;
			}
			int num3 = (ctrl ? FindPrevWordBegin() : ((!m_isRichTextEditingAllowed) ? ((caretSelectPositionInternal < 2) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].stringLength)) : ((stringSelectPositionInternal <= 0 || !char.IsLowSurrogate(text[stringSelectPositionInternal - 1])) ? (stringSelectPositionInternal - 1) : (stringSelectPositionInternal - 2))));
			if (shift)
			{
				stringSelectPositionInternal = num3;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
				return;
			}
			num2 = (stringPositionInternal = num3);
			stringSelectPositionInternal = num2;
			if (caretPositionInternal > 0 && stringPositionInternal <= m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index)
			{
				num2 = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretSelectPositionInternal = num2;
			}
		}

		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			int num = m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
			int num2 = -1;
			float num3 = 32767f;
			float num4 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex; i < num; i++)
			{
				TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num6 >= 0f && num6 <= 1f)
				{
					if (num6 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num5 = Mathf.Abs(num5);
				if (num5 < num3)
				{
					num2 = i;
					num3 = num5;
					num4 = num6;
				}
			}
			if (num2 == -1)
			{
				return num;
			}
			if (num4 < 0.5f)
			{
				return num2;
			}
			return num2 + 1;
		}

		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				return m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return m_TextComponent.textInfo.characterCount - 1;
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
			int num = -1;
			float num2 = 32767f;
			float num3 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex; i < lastCharacterIndex; i++)
			{
				TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num5 >= 0f && num5 <= 1f)
				{
					if (num5 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num4 = Mathf.Abs(num4);
				if (num4 < num2)
				{
					num = i;
					num2 = num4;
					num3 = num5;
				}
			}
			if (num == -1)
			{
				return lastCharacterIndex;
			}
			if (num3 < 0.5f)
			{
				return num;
			}
			return num + 1;
		}

		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			float height = m_TextViewport.rect.height;
			int num = lineNumber - 1;
			while (num > 0 && !(m_TextComponent.textInfo.lineInfo[num].baseline > m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height))
			{
				num--;
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
			int num2 = -1;
			float num3 = 32767f;
			float num4 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex; i < lastCharacterIndex; i++)
			{
				TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num6 >= 0f && num6 <= 1f)
				{
					if (num6 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num5 = Mathf.Abs(num5);
				if (num5 < num3)
				{
					num2 = i;
					num3 = num5;
					num4 = num6;
				}
			}
			if (num2 == -1)
			{
				return lastCharacterIndex;
			}
			if (num4 < 0.5f)
			{
				return num2;
			}
			return num2 + 1;
		}

		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				return m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return m_TextComponent.textInfo.characterCount - 1;
			}
			float height = m_TextViewport.rect.height;
			int i;
			for (i = lineNumber + 1; i < m_TextComponent.textInfo.lineCount - 1 && !(m_TextComponent.textInfo.lineInfo[i].baseline < m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height); i++)
			{
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[i].lastCharacterIndex;
			int num = -1;
			float num2 = 32767f;
			float num3 = 0f;
			for (int j = m_TextComponent.textInfo.lineInfo[i].firstCharacterIndex; j < lastCharacterIndex; j++)
			{
				TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[j];
				float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num5 >= 0f && num5 <= 1f)
				{
					if (num5 < 0.5f)
					{
						return j;
					}
					return j + 1;
				}
				num4 = Mathf.Abs(num4);
				if (num4 < num2)
				{
					num = j;
					num2 = num4;
					num3 = num5;
				}
			}
			if (num == -1)
			{
				return lastCharacterIndex;
			}
			if (num3 < 0.5f)
			{
				return num;
			}
			return num + 1;
		}

		private void MoveDown(bool shift)
		{
			MoveDown(shift, goToLastChar: true);
		}

		private void MoveDown(bool shift, bool goToLastChar)
		{
			int num2;
			if (hasSelection && !shift)
			{
				num2 = (caretSelectPositionInternal = Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num2;
			}
			int num3 = (multiLine ? LineDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
				return;
			}
			num2 = (caretPositionInternal = num3);
			caretSelectPositionInternal = num2;
			num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num2;
		}

		private void MoveUp(bool shift)
		{
			MoveUp(shift, goToFirstChar: true);
		}

		private void MoveUp(bool shift, bool goToFirstChar)
		{
			int num2;
			if (hasSelection && !shift)
			{
				num2 = (caretSelectPositionInternal = Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num2;
			}
			int num3 = (multiLine ? LineUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
				return;
			}
			num2 = (caretPositionInternal = num3);
			caretSelectPositionInternal = num2;
			num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num2;
		}

		private void MovePageUp(bool shift)
		{
			MovePageUp(shift, goToFirstChar: true);
		}

		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (hasSelection && !shift)
			{
				int num2 = (caretSelectPositionInternal = Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num2;
			}
			int num3 = (multiLine ? PageUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			}
			else
			{
				int num2 = (caretPositionInternal = num3);
				caretSelectPositionInternal = num2;
				num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
				stringSelectPositionInternal = num2;
			}
			if (m_LineType != 0)
			{
				float height = m_TextViewport.rect.height;
				float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.max.y;
				float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMax;
				height = ((num6 > num5 + height) ? height : (num6 - num5));
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, height);
				AssignPositioningIfNeeded();
				m_IsScrollbarUpdateRequired = true;
			}
		}

		private void MovePageDown(bool shift)
		{
			MovePageDown(shift, goToLastChar: true);
		}

		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (hasSelection && !shift)
			{
				int num2 = (caretSelectPositionInternal = Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num2;
			}
			int num3 = (multiLine ? PageDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			}
			else
			{
				int num2 = (caretPositionInternal = num3);
				caretSelectPositionInternal = num2;
				num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
				stringSelectPositionInternal = num2;
			}
			if (m_LineType != 0)
			{
				float height = m_TextViewport.rect.height;
				float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.min.y;
				float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMin;
				height = ((num6 > num5 + height) ? height : (num6 - num5));
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, height);
				AssignPositioningIfNeeded();
				m_IsScrollbarUpdateRequired = true;
			}
		}

		private void Delete()
		{
			if (m_ReadOnly || stringPositionInternal == stringSelectPositionInternal)
			{
				return;
			}
			if (m_isRichTextEditingAllowed || m_isSelectAll)
			{
				if (stringPositionInternal < stringSelectPositionInternal)
				{
					m_Text = text.Remove(stringPositionInternal, stringSelectPositionInternal - stringPositionInternal);
					stringSelectPositionInternal = stringPositionInternal;
				}
				else
				{
					m_Text = text.Remove(stringSelectPositionInternal, stringPositionInternal - stringSelectPositionInternal);
					stringPositionInternal = stringSelectPositionInternal;
				}
				m_isSelectAll = false;
			}
			else if (caretPositionInternal < caretSelectPositionInternal)
			{
				stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
				stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].stringLength;
				m_Text = text.Remove(stringPositionInternal, stringSelectPositionInternal - stringPositionInternal);
				stringSelectPositionInternal = stringPositionInternal;
				caretSelectPositionInternal = caretPositionInternal;
			}
			else
			{
				stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].stringLength;
				stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].index;
				m_Text = text.Remove(stringSelectPositionInternal, stringPositionInternal - stringSelectPositionInternal);
				stringPositionInternal = stringSelectPositionInternal;
				caretPositionInternal = caretSelectPositionInternal;
			}
		}

		private void DeleteKey()
		{
			if (m_ReadOnly)
			{
				return;
			}
			if (hasSelection)
			{
				Delete();
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
			else if (m_isRichTextEditingAllowed)
			{
				if (stringPositionInternal < text.Length)
				{
					if (char.IsHighSurrogate(text[stringPositionInternal]))
					{
						m_Text = text.Remove(stringPositionInternal, 2);
					}
					else
					{
						m_Text = text.Remove(stringPositionInternal, 1);
					}
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (caretPositionInternal < m_TextComponent.textInfo.characterCount - 1)
			{
				int stringLength = m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength;
				int index = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
				m_Text = text.Remove(index, stringLength);
				SendOnValueChangedAndUpdateLabel();
			}
		}

		private void Backspace()
		{
			if (m_ReadOnly)
			{
				return;
			}
			if (hasSelection)
			{
				Delete();
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
			else if (m_isRichTextEditingAllowed)
			{
				if (stringPositionInternal > 0)
				{
					int num = 1;
					if (char.IsLowSurrogate(text[stringPositionInternal - 1]))
					{
						num = 2;
					}
					stringSelectPositionInternal = (stringPositionInternal -= num);
					m_Text = text.Remove(stringPositionInternal, num);
					caretSelectPositionInternal = --caretPositionInternal;
					m_isLastKeyBackspace = true;
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else
			{
				if (caretPositionInternal > 0)
				{
					int stringLength = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].stringLength;
					m_Text = text.Remove(m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index, stringLength);
					int num3 = (stringPositionInternal = ((caretPositionInternal < 2) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[caretPositionInternal - 2].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal - 2].stringLength)));
					stringSelectPositionInternal = num3;
					caretSelectPositionInternal = --caretPositionInternal;
				}
				m_isLastKeyBackspace = true;
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
		}

		protected virtual void Append(string input)
		{
			if (m_ReadOnly || !InPlaceEditing())
			{
				return;
			}
			int i = 0;
			for (int length = input.Length; i < length; i++)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
				{
					Append(c);
				}
			}
		}

		protected virtual void Append(char input)
		{
			if (m_ReadOnly || !InPlaceEditing())
			{
				return;
			}
			if (onValidateInput != null)
			{
				input = onValidateInput(text, stringPositionInternal, input);
			}
			else
			{
				if (characterValidation == CharacterValidation.CustomValidator)
				{
					input = Validate(text, stringPositionInternal, input);
					if (input != 0)
					{
						SendOnValueChanged();
						UpdateLabel();
					}
					return;
				}
				if (characterValidation != 0)
				{
					input = Validate(text, stringPositionInternal, input);
				}
			}
			if (input != 0)
			{
				Insert(input);
			}
		}

		private void Insert(char c)
		{
			if (m_ReadOnly)
			{
				return;
			}
			string value = c.ToString();
			Delete();
			if (characterLimit <= 0 || text.Length < characterLimit)
			{
				m_Text = text.Insert(m_StringPosition, value);
				if (!char.IsHighSurrogate(c))
				{
					caretSelectPositionInternal = ++caretPositionInternal;
				}
				stringSelectPositionInternal = ++stringPositionInternal;
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChanged();
			}
		}

		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (m_SoftKeyboard != null && InPlaceEditing())
			{
				m_SoftKeyboard.text = m_Text;
			}
		}

		private void SendOnValueChangedAndUpdateLabel()
		{
			UpdateLabel();
			SendOnValueChanged();
		}

		private void SendOnValueChanged()
		{
			if (onValueChanged != null)
			{
				onValueChanged.Invoke(text);
			}
		}

		protected void SendOnEndEdit()
		{
			if (onEndEdit != null)
			{
				onEndEdit.Invoke(m_Text);
			}
		}

		protected void SendOnSubmit()
		{
			if (onSubmit != null)
			{
				onSubmit.Invoke(m_Text);
			}
		}

		protected void SendOnFocus()
		{
			if (onSelect != null)
			{
				onSelect.Invoke(m_Text);
			}
		}

		protected void SendOnFocusLost()
		{
			if (onDeselect != null)
			{
				onDeselect.Invoke(m_Text);
			}
		}

		protected void SendOnTextSelection()
		{
			m_isSelected = true;
			if (onTextSelection != null)
			{
				onTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
			}
		}

		protected void SendOnEndTextSelection()
		{
			if (m_isSelected)
			{
				if (onEndTextSelection != null)
				{
					onEndTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
				}
				m_isSelected = false;
			}
		}

		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (onTouchScreenKeyboardStatusChanged != null)
			{
				onTouchScreenKeyboardStatusChanged.Invoke(m_SoftKeyboard.status);
			}
		}

		protected void UpdateLabel()
		{
			if (!(m_TextComponent != null) || !(m_TextComponent.font != null) || m_PreventCallback)
			{
				return;
			}
			m_PreventCallback = true;
			string text = ((compositionString.Length <= 0) ? this.text : (this.text.Substring(0, m_StringPosition) + compositionString + this.text.Substring(m_StringPosition)));
			string text2 = ((inputType != InputType.Password) ? text : new string(asteriskChar, text.Length));
			bool flag = string.IsNullOrEmpty(text);
			if (m_Placeholder != null)
			{
				m_Placeholder.enabled = flag;
			}
			if (!flag)
			{
				SetCaretVisible();
			}
			m_TextComponent.text = text2 + "\u200b";
			if (m_LineLimit > 0)
			{
				m_TextComponent.ForceMeshUpdate();
				if (m_TextComponent.textInfo.lineCount > m_LineLimit)
				{
					int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[m_LineLimit - 1].lastCharacterIndex;
					int num = m_TextComponent.textInfo.characterInfo[lastCharacterIndex].index + m_TextComponent.textInfo.characterInfo[lastCharacterIndex].stringLength;
					this.text = text2.Remove(num, text2.Length - num);
					m_TextComponent.text = this.text + "\u200b";
				}
			}
			if (m_IsTextComponentUpdateRequired)
			{
				m_IsTextComponentUpdateRequired = false;
				m_TextComponent.ForceMeshUpdate();
			}
			MarkGeometryAsDirty();
			m_IsScrollbarUpdateRequired = true;
			m_PreventCallback = false;
		}

		private void UpdateScrollbar()
		{
			if ((bool)m_VerticalScrollbar)
			{
				float size = m_TextViewport.rect.height / m_TextComponent.preferredHeight;
				m_IsUpdatingScrollbarValues = true;
				m_VerticalScrollbar.size = size;
				float scrollPosition = (m_VerticalScrollbar.value = m_TextComponent.rectTransform.anchoredPosition.y / (m_TextComponent.preferredHeight - m_TextViewport.rect.height));
				m_ScrollPosition = scrollPosition;
			}
		}

		private void OnScrollbarValueChange(float value)
		{
			if (m_IsUpdatingScrollbarValues)
			{
				m_IsUpdatingScrollbarValues = false;
			}
			else if (!(value < 0f) && !(value > 1f))
			{
				AdjustTextPositionRelativeToViewport(value);
				m_ScrollPosition = value;
			}
		}

		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (!(m_TextViewport == null))
			{
				TMP_TextInfo textInfo = m_TextComponent.textInfo;
				if (textInfo != null && textInfo.lineInfo != null && textInfo.lineCount != 0 && textInfo.lineCount <= textInfo.lineInfo.Length)
				{
					m_TextComponent.rectTransform.anchoredPosition = new Vector2(m_TextComponent.rectTransform.anchoredPosition.x, (m_TextComponent.preferredHeight - m_TextViewport.rect.height) * relativePosition);
					AssignPositioningIfNeeded();
				}
			}
		}

		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (stringIndex < m_TextComponent.textInfo.characterInfo[i].index + m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			ClampCaretPos(ref caretPosition);
			return m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		public void ForceLabelUpdate()
		{
			UpdateLabel();
		}

		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				UpdateGeometry();
			}
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		private void UpdateGeometry()
		{
			if (InPlaceEditing() && !(m_CachedInputRenderer == null))
			{
				OnFillVBO(mesh);
				m_CachedInputRenderer.SetMesh(mesh);
			}
		}

		private void AssignPositioningIfNeeded()
		{
			if (m_TextComponent != null && caretRectTrans != null && (caretRectTrans.localPosition != m_TextComponent.rectTransform.localPosition || caretRectTrans.localRotation != m_TextComponent.rectTransform.localRotation || caretRectTrans.localScale != m_TextComponent.rectTransform.localScale || caretRectTrans.anchorMin != m_TextComponent.rectTransform.anchorMin || caretRectTrans.anchorMax != m_TextComponent.rectTransform.anchorMax || caretRectTrans.anchoredPosition != m_TextComponent.rectTransform.anchoredPosition || caretRectTrans.sizeDelta != m_TextComponent.rectTransform.sizeDelta || caretRectTrans.pivot != m_TextComponent.rectTransform.pivot))
			{
				caretRectTrans.localPosition = m_TextComponent.rectTransform.localPosition;
				caretRectTrans.localRotation = m_TextComponent.rectTransform.localRotation;
				caretRectTrans.localScale = m_TextComponent.rectTransform.localScale;
				caretRectTrans.anchorMin = m_TextComponent.rectTransform.anchorMin;
				caretRectTrans.anchorMax = m_TextComponent.rectTransform.anchorMax;
				caretRectTrans.anchoredPosition = m_TextComponent.rectTransform.anchoredPosition;
				caretRectTrans.sizeDelta = m_TextComponent.rectTransform.sizeDelta;
				caretRectTrans.pivot = m_TextComponent.rectTransform.pivot;
			}
		}

		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!isFocused && !m_SelectionStillActive)
				{
					vertexHelper.FillMesh(vbo);
					return;
				}
				if (m_IsStringPositionDirty)
				{
					stringPositionInternal = GetStringIndexFromCaretPosition(m_CaretPosition);
					stringSelectPositionInternal = GetStringIndexFromCaretPosition(m_CaretSelectPosition);
					m_IsStringPositionDirty = false;
				}
				if (m_IsCaretPositionDirty)
				{
					caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
					caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
					m_IsCaretPositionDirty = false;
				}
				if (!hasSelection && !m_ReadOnly)
				{
					GenerateCaret(vertexHelper, Vector2.zero);
					SendOnEndTextSelection();
				}
				else
				{
					GenerateHightlight(vertexHelper, Vector2.zero);
					SendOnTextSelection();
				}
				vertexHelper.FillMesh(vbo);
			}
		}

		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (m_CaretVisible)
			{
				if (m_CursorVerts == null)
				{
					CreateCursorVerts();
				}
				float num = m_CaretWidth;
				Vector2 zero = Vector2.zero;
				float num2 = 0f;
				int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
				TMP_CharacterInfo tMP_CharacterInfo;
				if (caretPositionInternal == m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
				{
					tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal];
					zero = new Vector2(tMP_CharacterInfo.origin, tMP_CharacterInfo.descender);
					num2 = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
				}
				else
				{
					tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1];
					zero = new Vector2(tMP_CharacterInfo.xAdvance, tMP_CharacterInfo.descender);
					num2 = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
				}
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.selection = new RangeInt(stringPositionInternal, 0);
				}
				if ((isFocused && zero != m_LastPosition) || m_forceRectTransformAdjustment)
				{
					AdjustRectTransformRelativeToViewport(zero, num2, tMP_CharacterInfo.isVisible);
				}
				m_LastPosition = zero;
				float num3 = zero.y + num2;
				float y = num3 - num2;
				float scaleFactor = m_TextComponent.canvas.scaleFactor;
				m_CursorVerts[0].position = new Vector3(zero.x, y, 0f);
				m_CursorVerts[1].position = new Vector3(zero.x, num3, 0f);
				m_CursorVerts[2].position = new Vector3(zero.x + (num + 1f) / scaleFactor, num3, 0f);
				m_CursorVerts[3].position = new Vector3(zero.x + (num + 1f) / scaleFactor, y, 0f);
				m_CursorVerts[0].color = caretColor;
				m_CursorVerts[1].color = caretColor;
				m_CursorVerts[2].color = caretColor;
				m_CursorVerts[3].color = caretColor;
				vbo.AddUIVertexQuad(m_CursorVerts);
				int height = Screen.height;
				zero.y = (float)height - zero.y;
				inputSystem.compositionCursorPos = zero;
			}
		}

		private void CreateCursorVerts()
		{
			m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < m_CursorVerts.Length; i++)
			{
				m_CursorVerts[i] = UIVertex.simpleVert;
				m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			TMP_TextInfo textInfo = m_TextComponent.textInfo;
			caretPositionInternal = (m_CaretPosition = GetCaretPositionFromStringIndex(stringPositionInternal));
			caretSelectPositionInternal = (m_CaretSelectPosition = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
			if (m_SoftKeyboard != null)
			{
				int num = ((caretPositionInternal < caretSelectPositionInternal) ? textInfo.characterInfo[caretPositionInternal].index : textInfo.characterInfo[caretSelectPositionInternal].index);
				int length = ((caretPositionInternal < caretSelectPositionInternal) ? (stringSelectPositionInternal - num) : (stringPositionInternal - num));
				m_SoftKeyboard.selection = new RangeInt(num, length);
			}
			float num2 = 0f;
			Vector2 startPosition;
			if (caretSelectPositionInternal < textInfo.characterCount)
			{
				startPosition = new Vector2(textInfo.characterInfo[caretSelectPositionInternal].origin, textInfo.characterInfo[caretSelectPositionInternal].descender);
				num2 = textInfo.characterInfo[caretSelectPositionInternal].ascender - textInfo.characterInfo[caretSelectPositionInternal].descender;
			}
			else
			{
				startPosition = new Vector2(textInfo.characterInfo[caretSelectPositionInternal - 1].xAdvance, textInfo.characterInfo[caretSelectPositionInternal - 1].descender);
				num2 = textInfo.characterInfo[caretSelectPositionInternal - 1].ascender - textInfo.characterInfo[caretSelectPositionInternal - 1].descender;
			}
			AdjustRectTransformRelativeToViewport(startPosition, num2, isCharVisible: true);
			int num3 = Mathf.Max(0, caretPositionInternal);
			int num4 = Mathf.Max(0, caretSelectPositionInternal);
			if (num3 > num4)
			{
				int num5 = num3;
				num3 = num4;
				num4 = num5;
			}
			num4--;
			int num6 = textInfo.characterInfo[num3].lineNumber;
			int lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = selectionColor;
			for (int i = num3; i <= num4 && i < textInfo.characterCount; i++)
			{
				if (i == lastCharacterIndex || i == num4)
				{
					TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[num3];
					TMP_CharacterInfo tMP_CharacterInfo2 = textInfo.characterInfo[i];
					if (i > 0 && tMP_CharacterInfo2.character == '\n' && textInfo.characterInfo[i - 1].character == '\r')
					{
						tMP_CharacterInfo2 = textInfo.characterInfo[i - 1];
					}
					Vector2 vector = new Vector2(tMP_CharacterInfo.origin, textInfo.lineInfo[num6].ascender);
					Vector2 vector2 = new Vector2(tMP_CharacterInfo2.xAdvance, textInfo.lineInfo[num6].descender);
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num3 = i + 1;
					num6++;
					if (num6 < textInfo.lineCount)
					{
						lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
					}
				}
			}
			m_IsScrollbarUpdateRequired = true;
		}

		private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
		{
			if (m_TextViewport == null || m_IsDrivenByLayoutComponents)
			{
				return;
			}
			float xMin = m_TextViewport.rect.xMin;
			float xMax = m_TextViewport.rect.xMax;
			float num = xMax - (m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x + m_TextComponent.margin.z + (float)m_CaretWidth);
			if (num < 0f && (!multiLine || (multiLine && isCharVisible)))
			{
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(num, 0f);
				AssignPositioningIfNeeded();
			}
			float num2 = m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x - m_TextComponent.margin.x - xMin;
			if (num2 < 0f)
			{
				m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f - num2, 0f);
				AssignPositioningIfNeeded();
			}
			if (m_LineType != 0)
			{
				float num3 = m_TextViewport.rect.yMax - (m_TextComponent.rectTransform.anchoredPosition.y + startPosition.y + height);
				if (num3 < -0.0001f)
				{
					m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num3);
					AssignPositioningIfNeeded();
					m_IsScrollbarUpdateRequired = true;
				}
				float num4 = m_TextComponent.rectTransform.anchoredPosition.y + startPosition.y - m_TextViewport.rect.yMin;
				if (num4 < 0f)
				{
					m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, num4);
					AssignPositioningIfNeeded();
					m_IsScrollbarUpdateRequired = true;
				}
			}
			if (m_isLastKeyBackspace)
			{
				float num5 = m_TextComponent.rectTransform.anchoredPosition.x + m_TextComponent.textInfo.characterInfo[0].origin - m_TextComponent.margin.x;
				float num6 = m_TextComponent.rectTransform.anchoredPosition.x + m_TextComponent.textInfo.characterInfo[m_TextComponent.textInfo.characterCount - 1].origin + m_TextComponent.margin.z;
				if (m_TextComponent.rectTransform.anchoredPosition.x + startPosition.x <= xMin + 0.0001f)
				{
					if (num5 < xMin)
					{
						float x = Mathf.Min((xMax - xMin) / 2f, xMin - num5);
						m_TextComponent.rectTransform.anchoredPosition += new Vector2(x, 0f);
						AssignPositioningIfNeeded();
					}
				}
				else if (num6 < xMax && num5 < xMin)
				{
					float x2 = Mathf.Min(xMax - num6, xMin - num5);
					m_TextComponent.rectTransform.anchoredPosition += new Vector2(x2, 0f);
					AssignPositioningIfNeeded();
				}
				m_isLastKeyBackspace = false;
			}
			m_forceRectTransformAdjustment = false;
		}

		protected char Validate(string text, int pos, char ch)
		{
			if (characterValidation == CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (characterValidation == CharacterValidation.Integer || characterValidation == CharacterValidation.Decimal)
			{
				bool num = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag = stringPositionInternal == 0 || stringSelectPositionInternal == 0;
				if (!num)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag))
					{
						return ch;
					}
					if (ch == '.' && characterValidation == CharacterValidation.Decimal && !text.Contains("."))
					{
						return ch;
					}
				}
			}
			else if (characterValidation == CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (characterValidation == CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (characterValidation == CharacterValidation.Name)
			{
				char c = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char c2 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && c == ' ')
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && c != ' ' && c != '\'')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				switch (ch)
				{
				case '\'':
					if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
					{
						return ch;
					}
					break;
				case ' ':
					if (c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
					{
						return ch;
					}
					break;
				}
			}
			else if (characterValidation == CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					char num2 = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c3 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num2 != '.' && c3 != '.')
					{
						return ch;
					}
				}
			}
			else if (characterValidation == CharacterValidation.Regex)
			{
				if (Regex.IsMatch(ch.ToString(), m_RegexValue))
				{
					return ch;
				}
			}
			else if (characterValidation == CharacterValidation.CustomValidator && m_InputValidator != null)
			{
				char result = m_InputValidator.Validate(ref text, ref pos, ch);
				m_Text = text;
				int num4 = (stringPositionInternal = pos);
				stringSelectPositionInternal = num4;
				return result;
			}
			return '\0';
		}

		public void ActivateInputField()
		{
			if (!(m_TextComponent == null) && !(m_TextComponent.font == null) && IsActive() && IsInteractable())
			{
				if (isFocused && m_SoftKeyboard != null && !m_SoftKeyboard.active)
				{
					m_SoftKeyboard.active = true;
					m_SoftKeyboard.text = m_Text;
				}
				m_ShouldActivateNextUpdate = true;
			}
		}

		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			if (TouchScreenKeyboard.isSupported && !shouldHideSoftKeyboard)
			{
				if (inputSystem.touchSupported)
				{
					TouchScreenKeyboard.hideInput = shouldHideMobileInput;
				}
				if (!shouldHideSoftKeyboard && !m_ReadOnly && contentType != ContentType.Custom)
				{
					m_SoftKeyboard = ((inputType == InputType.Password) ? TouchScreenKeyboard.Open(m_Text, keyboardType, autocorrection: false, multiLine, secure: true, alert: false, "", characterLimit) : TouchScreenKeyboard.Open(m_Text, keyboardType, inputType == InputType.AutoCorrect, multiLine, secure: false, alert: false, "", characterLimit));
					if (!shouldHideMobileInput)
					{
						MoveTextEnd(shift: false);
					}
					else
					{
						OnFocus();
						if (m_SoftKeyboard != null)
						{
							int length = ((stringPositionInternal < stringSelectPositionInternal) ? (stringSelectPositionInternal - stringPositionInternal) : (stringPositionInternal - stringSelectPositionInternal));
							m_SoftKeyboard.selection = new RangeInt((stringPositionInternal < stringSelectPositionInternal) ? stringPositionInternal : stringSelectPositionInternal, length);
						}
					}
				}
				m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			else
			{
				if (!TouchScreenKeyboard.isSupported)
				{
					inputSystem.imeCompositionMode = IMECompositionMode.On;
				}
				OnFocus();
			}
			m_AllowInput = true;
			m_OriginalText = text;
			m_WasCanceled = false;
			SetCaretVisible();
			UpdateLabel();
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			SendOnFocus();
			ActivateInputField();
		}

		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				ActivateInputField();
			}
		}

		public void OnControlClick()
		{
		}

		public void ReleaseSelection()
		{
			m_SelectionStillActive = false;
			MarkGeometryAsDirty();
		}

		public void DeactivateInputField(bool clearSelection = false)
		{
			if (!m_AllowInput)
			{
				return;
			}
			m_HasDoneFocusTransition = false;
			m_AllowInput = false;
			if (m_Placeholder != null)
			{
				m_Placeholder.enabled = string.IsNullOrEmpty(m_Text);
			}
			if (m_TextComponent != null && IsInteractable())
			{
				if (m_WasCanceled && m_RestoreOriginalTextOnEscape)
				{
					text = m_OriginalText;
				}
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.active = false;
					m_SoftKeyboard = null;
				}
				m_SelectionStillActive = true;
				if (m_ResetOnDeActivation || m_ReleaseSelection)
				{
					m_SelectionStillActive = false;
					m_ReleaseSelection = false;
					m_SelectedObject = null;
				}
				SendOnEndEdit();
				SendOnEndTextSelection();
				inputSystem.imeCompositionMode = IMECompositionMode.Auto;
			}
			MarkGeometryAsDirty();
			m_IsScrollbarUpdateRequired = true;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			DeactivateInputField();
			base.OnDeselect(eventData);
			SendOnFocusLost();
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (IsActive() && IsInteractable())
			{
				if (!isFocused)
				{
					m_ShouldActivateNextUpdate = true;
				}
				SendOnSubmit();
			}
		}

		private void EnforceContentType()
		{
			switch (contentType)
			{
			case ContentType.Standard:
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.Default;
				m_CharacterValidation = CharacterValidation.None;
				break;
			case ContentType.Autocorrected:
				m_InputType = InputType.AutoCorrect;
				m_KeyboardType = TouchScreenKeyboardType.Default;
				m_CharacterValidation = CharacterValidation.None;
				break;
			case ContentType.IntegerNumber:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				m_CharacterValidation = CharacterValidation.Integer;
				break;
			case ContentType.DecimalNumber:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				m_CharacterValidation = CharacterValidation.Decimal;
				break;
			case ContentType.Alphanumeric:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				m_CharacterValidation = CharacterValidation.Alphanumeric;
				break;
			case ContentType.Name:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.Default;
				m_CharacterValidation = CharacterValidation.Name;
				break;
			case ContentType.EmailAddress:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Standard;
				m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				m_CharacterValidation = CharacterValidation.EmailAddress;
				break;
			case ContentType.Password:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Password;
				m_KeyboardType = TouchScreenKeyboardType.Default;
				m_CharacterValidation = CharacterValidation.None;
				break;
			case ContentType.Pin:
				m_LineType = LineType.SingleLine;
				m_InputType = InputType.Password;
				m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				m_CharacterValidation = CharacterValidation.Digit;
				break;
			}
			SetTextComponentWrapMode();
		}

		private void SetTextComponentWrapMode()
		{
			if (!(m_TextComponent == null))
			{
				if (multiLine)
				{
					m_TextComponent.enableWordWrapping = true;
				}
				else
				{
					m_TextComponent.enableWordWrapping = false;
				}
			}
		}

		private void SetTextComponentRichTextMode()
		{
			if (!(m_TextComponent == null))
			{
				m_TextComponent.richText = m_RichText;
			}
		}

		private void SetToCustomIfContentTypeIsNot(params ContentType[] allowedContentTypes)
		{
			if (contentType == ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			contentType = ContentType.Custom;
		}

		private void SetToCustom()
		{
			if (contentType != ContentType.Custom)
			{
				contentType = ContentType.Custom;
			}
		}

		private void SetToCustom(CharacterValidation characterValidation)
		{
			if (contentType == ContentType.Custom)
			{
				characterValidation = CharacterValidation.CustomValidator;
				return;
			}
			contentType = ContentType.Custom;
			characterValidation = CharacterValidation.CustomValidator;
		}

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			if (m_HasDoneFocusTransition)
			{
				state = SelectionState.Highlighted;
			}
			else if (state == SelectionState.Pressed)
			{
				m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public void SetGlobalPointSize(float pointSize)
		{
			TMP_Text tMP_Text = m_Placeholder as TMP_Text;
			if (tMP_Text != null)
			{
				tMP_Text.fontSize = pointSize;
			}
			textComponent.fontSize = pointSize;
		}

		public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
		{
			TMP_Text tMP_Text = m_Placeholder as TMP_Text;
			if (tMP_Text != null)
			{
				tMP_Text.font = fontAsset;
			}
			textComponent.font = fontAsset;
		}

		[SpecialName]
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}
	}
}
