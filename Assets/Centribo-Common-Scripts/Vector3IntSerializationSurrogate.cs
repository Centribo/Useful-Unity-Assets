using UnityEngine;

namespace Centribo.Common {
	[System.Serializable]
	/// <summary>
	/// Basic serialization surrogate for <see cref="Vector3Int"/>
	/// </summary>
	public class Vector3IntSerializationSurrogate {
		public int X, Y, Z;

		public Vector3IntSerializationSurrogate(Vector3Int vector) {
			this.X = vector.x;
			this.Y = vector.y;
			this.Z = vector.z;
		}

		// Cast operators:
		public static implicit operator Vector3Int(Vector3IntSerializationSurrogate surrogate) {
			return new Vector3Int(surrogate.X, surrogate.Y, surrogate.Z);
		}

		public static implicit operator Vector3IntSerializationSurrogate(Vector3Int original) {
			return new Vector3IntSerializationSurrogate(original);
		}

		public override string ToString() {
			return $"({X}, {Y}, {Z})";
		}
	}
}