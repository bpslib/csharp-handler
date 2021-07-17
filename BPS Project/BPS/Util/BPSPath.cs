namespace BPSLib.Util
{
	internal class BPSPath
	{
		/// <summary>
		/// Document file extension.
		/// </summary>
		internal const string BPS_FILE_EXTENSION = ".bps";

		/// <summary>
		/// Insert BPS extension on filename.
		/// </summary>
		/// <param name="path">the normalized path.</param>
		internal static string NormalizePath(string path)
		{
			return path.EndsWith(BPS_FILE_EXTENSION) ? path : path + BPS_FILE_EXTENSION;
		}

		/// <summary>
		/// Remove BPS extension from filename.
		/// </summary>
		/// <param name="path">the path without extension.</param>
		internal static string RemoveExtension(string path)
		{
			return path.EndsWith(BPS_FILE_EXTENSION) ? path.Substring(0, path.Length - 4) : path;
		}
	}
}
