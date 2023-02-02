using System.Text;

namespace Centribo.Common.Extensions {
	public static class StringExtensions {
		/// <summary>
		/// Shorthand for string.Format()
		/// </summary>
		/// <example>
		/// <code>
		/// int x, y;
		/// string coords = "({0}, {1})".Format(x, y);
		/// </code>
		/// </example>
		/// <param name="s">The string to be formatted</param>
		/// <param name="args">The arguments to be passed into string.Format()</param>
		/// <returns>The formatted string</returns>
		public static string Format(this string s, params object[] args) {
			return string.Format(s, args);
		}

		/// <summary>
		/// Inserts spaces before capital letters
		/// </summary>
		public static string AddSpacesToSentence(this string text, bool preserveAcronyms = true) {
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			StringBuilder newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for (int i = 1; i < text.Length; i++) {
				if (char.IsUpper(text[i]))
					if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
						(preserveAcronyms && char.IsUpper(text[i - 1]) &&
						 i < text.Length - 1 && !char.IsUpper(text[i + 1])))
						newText.Append(' ');
				newText.Append(text[i]);
			}
			return newText.ToString();
		}
	}
}