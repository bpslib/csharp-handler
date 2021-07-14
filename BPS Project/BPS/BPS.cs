/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.IO;

namespace BPSLib
{
    public class BPS
    {
        #region Constants

        internal const string BPS_FILE_EXTENSION = ".bps";

        #endregion Constants


		#region Methods

		#region Public


		/// <summary>
		/// Read a BPS file from path.
		/// </summary>
		/// <param name="path">File path with or not extension</param>
		/// <returns>Readed file</returns>
		public static BPSFile Load(string path)
		{
			string data;
			try
			{
				var sr = new StreamReader(NormalizePath(path));
				data = sr.ReadToEnd();
				sr.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return Parse(data);
		}

		/// <summary>
		/// Write a BPS file on path.
		/// </summary>
		/// <param name="file">The file to be write</param>
		/// <param name="path">Save path with or not extension</param>
		public static void Save(BPSFile file, string path)
		{
			try
			{
				var sw = new StreamWriter(NormalizePath(path));

				sw.WriteLine(file.Plain());

				sw.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static BPSFile Parse(string data)
		{
			var parser = new Parser.File.Parser(data.Replace("\r\n", "\n").Replace("\r", string.Empty));
			parser.Parse();
			return parser.BPSFile;
		}

		public static string Plain(BPSFile file)
		{
			var parser = new Parser.Plain.Parser(file);
			parser.Parse();
			return parser.Plain;
		}

        #endregion Public

        #region Private

        /// <summary>
        /// Insert BPS extension on filename.
        /// </summary>
        /// <param name="path">File path</param>
        private static string NormalizePath(string path)
        {
            return path.EndsWith(BPS_FILE_EXTENSION) ? path : path + BPS_FILE_EXTENSION;
        }

        #endregion Private

        #endregion Methods

    }
}
