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
    public class BPSFile : IEnumerable<KeyValuePair<string, object>>
	{
        #region Vars

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

        public BPSFile(List<KeyValuePair<string, object>> data)
        {
            _data = data;
        }

		#endregion Constructors


		#region Methods

		#region Public


		/// <summary>
		/// Read a BPS file from path.
		/// </summary>
		/// <param name="path">File path with or not extension</param>
		/// <returns>Readed file</returns>
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

		public void Parse(string data)
		{
			_data = BPS.Parse(data)._data;
		}

		public string Plain()
		{
			return BPS.Plain(this);
		}

		public bool Add(string key, object value)
		{
            if (!Existis(key))
			{
				_data.Add(new KeyValuePair<string, object>(key, value));
				return true;
			}
			return false;
		}

        public bool Existis(string key)
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

        #endregion Public

        #region Private

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)_data).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_data).GetEnumerator();
		}

		#endregion Private

		#endregion Methods

	}
}
