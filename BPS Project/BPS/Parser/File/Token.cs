/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections.Generic;

namespace BPSLib.Parser.File
{
	/// <summary>
	/// Class <c>Token</c> represents a token in parser.
	/// </summary>
	internal class Token
	{
		#region Vars

		/// <summary>
		/// Token category.
		/// </summary>
		internal TokenCategory Category { get; }

		/// <summary>
		/// Token string representation.
		/// </summary>
		internal string Image { get; }

		/// <summary>
		/// Line that was encountered.
		/// </summary>
		internal int Line { get; }

		/// <summary>
		/// Collumn that was encountered.
		/// </summary>
		internal int Collumn { get; }

		#endregion Vars

		#region Contructors

		/// <summary>
		/// Default <c>Token</c> constructor.
		/// </summary>
		/// <param name="category">category.</param>
		/// <param name="image">string representation.</param>
		internal Token(TokenCategory category, string image, int line, int collumn)
		{
			Category = category;
			Image = image;
			Line = line;
			Collumn = collumn;
		}

		public override bool Equals(object obj)
		{
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				var t = (Token)obj;
				return GetHashCode().Equals(t.GetHashCode());
			}
		}

		public override int GetHashCode()
		{
			return Image.GetHashCode() * 17 + Category.GetHashCode() * 7 + Line * Collumn;
		}

		#endregion Constructors
	}
}
