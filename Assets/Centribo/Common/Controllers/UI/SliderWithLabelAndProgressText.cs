using Centribo.Common;
using Centribo.Common.Extensions;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Centribo.Common.UI {
	/// <summary>
	/// Controller for a slider that has a label and progress text
	/// </summary>
	[RequireComponent(typeof(Slider))]
	public class SliderWithLabelAndProgressText : MonoBehaviour, IStringLabellable {
		[SerializeField, Required, Foldout("Internal References")] public TextMeshProUGUI LabelTextMesh;
		[SerializeField, Required, Foldout("Internal References")] public Slider Slider;
		[SerializeField, Required, Foldout("Internal References")] private TextMeshProUGUI progressTextMesh;
		[SerializeField] string progressSeparator = "/";
		[SerializeField] Gradient fillGradient;

		public string TextLabel { get { return LabelTextMesh.text; } set { LabelTextMesh.text = value; } }
		public float MinValue { get { return Slider.minValue; } set { Slider.minValue = value; } }
		public float MaxValue { get { return Slider.maxValue; } set { Slider.maxValue = value; } }
		public float Value { get { return Slider.value; } set { Slider.value = value; } }

		private Image fillImage;

		void Awake() {
			Slider.onValueChanged.AddListener(delegate { UpdateDisplayBasedOnSliderValue(); });
		}

		void OnEnable() {
			fillImage = Slider.fillRect.GetComponent<Image>();
			UpdateDisplayBasedOnSliderValue();
		}

		void UpdateDisplayBasedOnSliderValue() {
			if (progressTextMesh != null) {
				progressTextMesh.text = $"{Slider.value}{progressSeparator}{Slider.maxValue}";
			}

			if (fillImage != null) {
				float t = MathExtensions.LinearMap(Slider.value, Slider.minValue, Slider.maxValue, 0.0f, 1.0f);
				Color c = fillGradient.Evaluate(t);
				fillImage.color = c;
			}
		}
	}
}