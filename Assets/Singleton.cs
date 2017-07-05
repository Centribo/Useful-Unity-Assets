﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the HoloToolkit Root.

namespace UnityEngine
{
	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		private static T _Instance;

		public static T Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = FindObjectOfType<T>();
				}
				return _Instance;
			}
		}
	}
}