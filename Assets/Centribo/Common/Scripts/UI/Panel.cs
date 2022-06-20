using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Centribo.Common.UI {
	public class Panel : MonoBehaviour {
		[SerializeField] protected bool OpenOnStart = false;

		public bool IsOpen { set { if (value) { Open(); } else { Close(); } } get { return isOpen; } }

		protected bool isOpen;

		virtual public void Start() {
			if (OpenOnStart) { Open(); }
		}

		/// <summary>
		/// Open this component, which should enable the game object, and do any initialization it needs.
		/// </summary>
		virtual public void Open() {
			gameObject.SetActive(true);
			isOpen = true;
		}

		/// <summary>
		/// Sets the given event system's selected object to the "first" object that should be selected
		/// when this panel is opened
		/// </summary>
		/// <param name="eventSystem"></param>
		virtual public void SelectFirstObject(EventSystem eventSystem) { throw new System.NotImplementedException(); }

		/// <summary>
		/// Close this component, which should disable the game object and do any cleanup it needs
		/// </summary>
		virtual public void Close() {
			gameObject.SetActive(false);
			isOpen = false;
		}
	}
}