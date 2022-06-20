using System;

namespace Centribo.Common {
	[Flags]
	public enum CompassDirection : int {
		None      = 0b00000000,
		North     = 0b00000001,
		Northeast = 0b00000010,
		East      = 0b00000100,
		Southeast = 0b00001000,
		South     = 0b00010000,
		Southwest = 0b00100000,
		West      = 0b01000000,
		Northwest = 0b10000000
	}
}
