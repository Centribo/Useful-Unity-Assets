using UnityEngine;
using System;
using System.Collections;

namespace Centribo.Common {
	//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
	//Modified by: -

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
		AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
	public class ConditionalHideAttribute : PropertyAttribute {
		public string ConditionalSourceField = "";
		public string ConditionalSourceField2 = "";
		public string[] ConditionalSourceFields = new string[] { };
		public bool[] ConditionalSourceFieldInverseBools = new bool[] { };
		public bool HideInInspector = false;
		public bool Inverse = false;
		public bool UseOrLogic = false;

		public bool InverseCondition1 = false;
		public bool InverseCondition2 = false;


		// Use this for initialization
		public ConditionalHideAttribute(string conditionalSourceField) {
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = false;
			Inverse = false;
		}

		public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector) {
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = hideInInspector;
			Inverse = false;
		}

		public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse) {
			ConditionalSourceField = conditionalSourceField;
			HideInInspector = hideInInspector;
			Inverse = inverse;
		}

		public ConditionalHideAttribute(bool hideInInspector = false) {
			ConditionalSourceField = "";
			HideInInspector = hideInInspector;
			Inverse = false;
		}

		public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse) {
			ConditionalSourceFields = conditionalSourceFields;
			ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
			HideInInspector = hideInInspector;
			Inverse = inverse;
		}

		public ConditionalHideAttribute(string[] conditionalSourceFields, bool hideInInspector, bool inverse) {
			ConditionalSourceFields = conditionalSourceFields;
			HideInInspector = hideInInspector;
			Inverse = inverse;
		}

	}
}