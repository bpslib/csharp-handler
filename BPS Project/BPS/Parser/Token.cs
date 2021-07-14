/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

namespace BPSLib.Parser
{
	internal class Token
	{
		#region Vars

		public TokenCategory Category { get; }
		public string Image { get; }

		#endregion Vars

		#region Contructors

		internal Token(TokenCategory category, string image)
		{
			Category = category;
			Image = image;
		}

		#endregion Constructors
	}
}
