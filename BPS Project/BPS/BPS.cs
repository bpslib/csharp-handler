/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections.Generic;
using System.Globalization;

namespace BPSLib
{
    /// <summary>
    /// Class <c>BPS</c>.
    /// </summary>
    public static class BPS
	{
		static BPS()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        /// <summary>
        /// Parse a string BPS data return a BPSFile.
        /// </summary>
        /// <param name="data">BPS data in string format.</param>
        /// <returns>BPS file representation from data.</returns>
        public static Dictionary<string, object> Parse(string data)
		{
            var parsedData = Core.File.Parser.Parse(data);
            return parsedData;
		}

        /// <summary>
        /// Convert a BPS structured data to plain text.
        /// </summary>
        /// <param name="data">BPS structured data to convert.</param>
        /// <returns>A String representation from data.</returns>
        public static string Plain(Dictionary<string, object> data)
		{
			return Core.Plain.Parser.Parse(data);
		}
	}
}
