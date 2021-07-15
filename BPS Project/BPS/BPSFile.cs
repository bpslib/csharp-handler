/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections;
using System.Collections.Generic;

namespace BPSLib
{
	/// <summary>
	/// Class <c>BPSFile</c> represents a BPS data document.
	/// </summary>
    public class BPSFile : IEnumerable<KeyValuePair<string, object>>
	{
        #region Vars

		/// <summary>
		/// Internal data structure.
		/// </summary>
        internal List<KeyValuePair<string, object>> _data;

        #endregion Vars


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BPSFile()
        {
            _data = new List<KeyValuePair<string, object>>();
        }

		/// <summary>
		/// Internal constructor to init with data.
		/// </summary>
		/// <param name="data">The initialization data.</param>
        internal BPSFile(List<KeyValuePair<string, object>> data)
        {
            _data = data;
        }

		#endregion Constructors


		#region Methods

		#region Public

		/// <summary>
		/// Read a BPS file from path.
		/// </summary>
		/// <param name="path">file path with or not extension.</param>
		/// <returns>Readed file.</returns>
		public void Load(string path)
		{
			_data = BPS.Load(path)._data;
		}

		/// <summary>
		/// Write self on passed path.
		/// </summary>
		/// <param name="path">Save path with or not extension</param>
		public void Save(string path)
		{
			BPS.Save(this, path);
		}

		/// <summary>
		/// Parse a plain string data and overwrites self.
		/// </summary>
		/// <param name="data">data in string format.</param>
		public void Parse(string data)
		{
			_data = BPS.Parse(data)._data;
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
		/// Add a value in passed key.
		/// </summary>
		/// <param name="key">the key to represents the value.</param>
		/// <param name="value">the value to store.</param>
		/// <returns>True if it was successful.</returns>
		public bool Add(string key, object value)
		{
            if (!Contains(key))
			{
				_data.Add(new KeyValuePair<string, object>(key, value));
				return true;
			}
			return false;
		}

		/// <summary>
		/// Remove a value from passed key.
		/// </summary>
		/// <param name="key">the key to remove.</param>
		/// <returns>True if was successful.</returns>
		public bool Remove(string key)
		{
			foreach (var d in _data)
			{
				if (d.Key.Equals(key))
				{
					_data.Remove(d);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Search and return a value from key.
		/// </summary>
		/// <param name="key">the key to search.</param>
		/// <returns>Encountered value.</returns>
		public object Find(string key)
		{
			foreach (var d in _data)
			{
				if (d.Key.Equals(key))
				{
					return d.Value;
				}
			}
			return null;
		}

		/// <summary>
		/// Count the elements from BPSFile.
		/// </summary>
		/// <returns>The element count.</returns>
		public int Count()
		{
			return _data.Count;
		}

		/// <summary>
		/// Remove all data.
		/// </summary>
		public void Clear()
		{
			_data.Clear();
		}

		/// <summary>
		/// Verify if a value existis from the passed key.
		/// </summary>
		/// <param name="key">the key from value.</param>
		/// <returns>True if exists.</returns>
		public bool Contains(string key)
		{
            foreach (var d in _data)
			{
                if (d.Key.Equals(key))
				{
                    return true;
				}
			}
            return false;
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
				foreach (var d in _data)
				{
					if (!file.Contains(d.Key))
					{
						return false;
					}
				}
				return _data.Count == file.Count();
			}
		}

		public override int GetHashCode()
		{
			// TODO: implements custom hash
			return base.GetHashCode();
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)_data).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_data).GetEnumerator();
		}

		#endregion Public

		#region Private



		#endregion Private

		#endregion Methods

	}
}
