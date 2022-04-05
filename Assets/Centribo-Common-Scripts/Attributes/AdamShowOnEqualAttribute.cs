using UnityEngine;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class AdamShowOnEqualAttribute : PropertyAttribute {
	public readonly string ConditionFieldName;

	public AdamShowOnEqualAttribute(string conditionFieldName) {
		this.ConditionFieldName = conditionFieldName;
	}
}