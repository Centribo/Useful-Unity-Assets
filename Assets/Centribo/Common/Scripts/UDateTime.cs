using System;
using UnityEngine;

namespace Centribo.Common {
	// we have to use UDateTime instead of DateTime on our classes
	// we still typically need to either cast this to a DateTime or read the DateTime field directly
	[System.Serializable]
	public class UDateTime : ISerializationCallbackReceiver {
		[HideInInspector] public DateTime dateTime;

		// if you don't want to use the PropertyDrawer then remove HideInInspector here
		[HideInInspector][SerializeField] private string _dateTime;

		public static implicit operator DateTime(UDateTime udt) {
			return (udt.dateTime);
		}

		public static implicit operator UDateTime(DateTime dt) {
			return new UDateTime() { dateTime = dt };
		}

		public void OnAfterDeserialize() {
			DateTime.TryParse(_dateTime, out dateTime);
		}

		public void OnBeforeSerialize() {
			_dateTime = dateTime.ToString();
		}
	}

}