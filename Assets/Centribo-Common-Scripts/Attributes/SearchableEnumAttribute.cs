﻿// ---------------------------------------------------------------------------- 
// Author: Ryan Hipple
// Date:   05/01/2018
// https://github.com/roboryantron/UnityEditorJunkie/tree/master/Assets/SearchableEnum
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Centribo.Common {
	/// <summary>
	/// Put this attribute on a public (or SerializeField) enum in a
	/// MonoBehaviour or ScriptableObject to get an improved enum selector
	/// popup. The enum list is scrollable and can be filtered by typing.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class SearchableEnumAttribute : PropertyAttribute { }
}