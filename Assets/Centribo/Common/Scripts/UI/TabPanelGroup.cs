using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Centribo.Common.UI {
	[RequireComponent(typeof(ToggleGroup))]
	public class TabPanelGroup : Panel {
		[SerializeField] private List<Panel> panels;
		[SerializeField] private List<Toggle> toggles;
		[SerializeField] private bool loopTabMovement = true;

		private ToggleGroup toggleGroup;
		private int currentTabIndex;
		private Dictionary<Toggle, Panel> panelsByToggle;

		public override void Open() {
			toggleGroup ??= GetComponent<ToggleGroup>();
			currentTabIndex = 0;

			if (panels.Count != toggles.Count) {
				Debug.LogError($"Mismatch in number of panels ({panels.Count}) and number of toggles ({toggles})", this);
			}

			panelsByToggle = new Dictionary<Toggle, Panel>();
			for (int i = 0; i < Mathf.Min(panels.Count, toggles.Count); i++) {
				panelsByToggle.Add(toggles[i], panels[i]);
			}

			foreach (Toggle toggle in toggles) {
				// toggleGroup.RegisterToggle(toggle);
				toggle.group = toggleGroup;
				toggle.onValueChanged.AddListener(delegate {
					OnToggleValueChanged(toggle);
				});
			}

			if (toggles.Count > 0) {
				for (int i = 0; i < toggles.Count; i++) {
					toggles[i].isOn = i == currentTabIndex;
				}
			}

			ForceUpdateShownPanel();
			base.Open();
		}

		public override void Close() {
			foreach (Toggle toggle in toggles) {
				toggle.onValueChanged.RemoveAllListeners();
			}

			base.Close();
		}


		public void ForceUpdateShownPanel() {
			Debug.Log(currentTabIndex);
			for (int i = 0; i < panels.Count; i++) {
				if (i == currentTabIndex) {
					panels[i]?.Open();
				} else {
					panels[i]?.Close();
				}
			}
		}

		public void IncrementActiveTab() {
			currentTabIndex++;
			WrapCurrentTabIndex();
			toggles[currentTabIndex].isOn = true;
		}

		public void DecrementActiveTab() {
			currentTabIndex--;
			WrapCurrentTabIndex();
			toggles[currentTabIndex].isOn = true;
		}

		void OnToggleValueChanged(Toggle toggle) {
			if (panelsByToggle.ContainsKey(toggle)) {
				Panel panel = panelsByToggle[toggle];

				if (toggle.isOn) {
					panel?.Open();
				} else {
					panel?.Close();
				}
			}

			if (toggle.isOn) {
				currentTabIndex = toggles.IndexOf(toggle);
			}
		}

		void WrapCurrentTabIndex() {
			if (loopTabMovement) {
				currentTabIndex = MathHelper.Mod(currentTabIndex, toggles.Count);
			} else {
				currentTabIndex = Mathf.Clamp(currentTabIndex, 0, toggles.Count - 1);
			}
		}
	}
}