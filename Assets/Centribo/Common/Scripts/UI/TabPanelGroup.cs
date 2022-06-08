using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Centribo.Common.UI {
	[RequireComponent(typeof(ToggleGroup))]
	public class TabPanelGroup : Panel {
		[SerializeField] protected List<Panel> Panels;
		[SerializeField] protected List<Toggle> Toggles;
		[SerializeField] public bool LoopTabMovement = true;

		protected ToggleGroup ToggleGroup;
		protected int CurrentTabIndex;
		protected Dictionary<Toggle, Panel> PanelsByToggle;

		public override void Open() {
			ToggleGroup = ToggleGroup ? ToggleGroup : GetComponent<ToggleGroup>();
			CurrentTabIndex = 0;

			if (Panels.Count != Toggles.Count) {
				Debug.LogError($"Mismatch in number of panels ({Panels.Count}) and number of toggles ({Toggles})", this);
			}

			PanelsByToggle = new Dictionary<Toggle, Panel>();
			for (int i = 0; i < Mathf.Min(Panels.Count, Toggles.Count); i++) {
				PanelsByToggle.Add(Toggles[i], Panels[i]);
			}

			foreach (Toggle toggle in Toggles) {
				// toggleGroup.RegisterToggle(toggle);
				toggle.group = ToggleGroup;
				toggle.onValueChanged.AddListener(delegate {
					OnToggleValueChanged(toggle);
				});
			}

			if (Toggles.Count > 0) {
				for (int i = 0; i < Toggles.Count; i++) {
					Toggles[i].isOn = i == CurrentTabIndex;
				}
			}

			ForceUpdateShownPanel();
			base.Open();
		}

		public override void Close() {
			foreach (Toggle toggle in Toggles) {
				toggle.onValueChanged.RemoveAllListeners();
			}

			base.Close();
		}


		public void ForceUpdateShownPanel() {
			Debug.Log(CurrentTabIndex);
			for (int i = 0; i < Panels.Count; i++) {
				if (Panels[i] == null) continue;

				if (i == CurrentTabIndex) {
					Panels[i].Open();
				} else {
					Panels[i].Close();
				}
			}
		}

		public void IncrementActiveTab() {
			CurrentTabIndex++;
			WrapCurrentTabIndex();
			Toggles[CurrentTabIndex].isOn = true;
		}

		public void DecrementActiveTab() {
			CurrentTabIndex--;
			WrapCurrentTabIndex();
			Toggles[CurrentTabIndex].isOn = true;
		}

		void OnToggleValueChanged(Toggle toggle) {
			if (PanelsByToggle.ContainsKey(toggle)) {
				Panel panel = PanelsByToggle[toggle];
				if (panel != null) {
					if (toggle.isOn) {
						panel.Open();
					} else {
						panel.Close();
					}
				}
			}

			if (toggle.isOn) {
				CurrentTabIndex = Toggles.IndexOf(toggle);
			}
		}

		void WrapCurrentTabIndex() {
			if (LoopTabMovement) {
				CurrentTabIndex = MathHelper.Mod(CurrentTabIndex, Toggles.Count);
			} else {
				CurrentTabIndex = Mathf.Clamp(CurrentTabIndex, 0, Toggles.Count - 1);
			}
		}
	}
}