/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using BPSLib.Util;
using System.Collections;
using System.Collections.Generic;

namespace BPSLib
{
	/// <summary>
	/// Class <c>BPSFile</c> represents a BPS data document.
	/// </summary>
	public class BPSFile : IEnumerable<KeyValuePair<string, object>>
	{
		/// <summary>
		/// Internal data structure.
		/// </summary>
		internal Dictionary<string, object> Data { get; set; }

		/// <summary>
		/// File path.
		/// </summary>
		private string _path;
		public string Path { get => _path; set => _path = BPSPath.NormalizePath(value); }


		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public BPSFile()
		{
			Data = new Dictionary<string, object>();
			Path = "";
		}

		/// <summary>
		/// Path constructor.
		/// </summary>
		public BPSFile(string path)
		{
			Data = new Dictionary<string, object>();
			Path = BPSPath.NormalizePath(path);
		}

		#endregion Constructors


		#region Methods

		#region Public

		/// <summary>
		/// Write self on passed path.
		/// </summary>
		public void Save()
		{
			BPS.Save(this);
		}

		/// <summary>
		/// Convert self data to plain text.
		/// </summary>
		/// <returns>A String representation from self data.</returns>
		public string Plain()
		{
			return BPS.Plain(this);
		}

		/// <summary>
		/// Add or update a value in passed key.
		/// </summary>
		/// <param name="key">the key to represents the value.</param>
		/// <param name="value">the value to store.</param>
		/// <returns>True if it was successful.</returns>
		public void Add(string key, object value)
		{
			if (Data.ContainsKey(key))
			{
				Data.Remove(key);
			}
			Data.Add(key, value);
		}

		/// <summary>
		/// Remove a value from passed key.
		/// </summary>
		/// <param name="key">the key to remove.</param>
		/// <returns>True if was successful.</returns>
		public bool Remove(string key)
		{
			return Data.Remove(key);
		}

		/// <summary>
		/// Search and return a value from key.
		/// </summary>
		/// <param name="key">the key to search.</param>
		/// <returns>Encountered value.</returns>
		public object Find(string key)
		{
			if (Data.TryGetValue(key, out object value))
			{
				return value;
			}
			return null;
		}

		/// <summary>
		/// Count the elements from BPSFile.
		/// </summary>
		/// <returns>The element count.</returns>
		public int Count()
		{
			return Data.Count;
		}

		/// <summary>
		/// Remove all data.
		/// </summary>
		public void Clear()
		{
			Data.Clear();
		}

		/// <summary>
		/// Verify if a value existis from the passed key.
		/// </summary>
		/// <param name="key">the key from value.</param>
		/// <returns>True if exists.</returns>
		public bool Contains(string key)
		{
			return Data.ContainsKey(key);
		}

		/// <summary>
		/// Get the name of file with extension.
		/// </summary>
		/// <returns>File name.</returns>
		public string GetFullName()
		{
			return Path.Remove(0, Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
		}

		/// <summary>
		/// Get the name of file.
		/// </summary>
		/// <returns>File name.</returns>
		public string GetName()
		{
			return BPSPath.RemoveExtension(Path.Remove(0, Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1));
		}

		public string GetPath()
		{
			return Path.Remove(Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
		}

		public override bool Equals(object obj)
		{
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				var file = (BPSFile)obj;
				return GetHashCode().Equals(file.GetHashCode());
			}
		}

		public override int GetHashCode()
		{
			var hash = 7;
			foreach (var d in Data)
			{
				hash = hash * 31 + d.GetHashCode();
			}
			return hash;
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)Data).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)Data).GetEnumerator();
		}

		#endregion Public

		#region Private



		#endregion Private

		#endregion Methods

	}
}
