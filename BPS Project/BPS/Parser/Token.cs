/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

namespace BPSLib.Parser
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

		#endregion Vars

		#region Contructors

		/// <summary>
		/// Default <c>Token</c> constructor.
		/// </summary>
		/// <param name="category">category.</param>
		/// <param name="image">string representation.</param>
		internal Token(TokenCategory category, string image)
		{
			Category = category;
			Image = image;
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
				return Category.Equals(t.Category) && Image.Equals(t.Image);
			}
		}

		public override int GetHashCode()
		{
			// TODO: implements custom hash
			return base.GetHashCode();
		}

		#endregion Constructors
	}
}
