using System;

namespace RolePlayingGame.Core
{
	internal class FileParseException : ApplicationException
	{
		#region Constants

		private const string _errorMessage = "There was a problem parsing file: {0}, message: {1}";

		#endregion Constants

		public FileParseException(string filePath)
			: this(filePath, string.Empty)
		{
		}

		public FileParseException(string filePath, string message)
			: this(filePath, message, null)
		{
		}

		public FileParseException(string filePath, string message, Exception innerException)
			: base(string.Format(_errorMessage, filePath, message), innerException)
		{
		}
	}
}