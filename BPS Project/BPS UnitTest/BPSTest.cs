using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPSLib;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;

namespace BPS_UnitTest
{
	[TestClass]
	public class BPSTest
	{
        readonly string strBpsData =
            "key1:\"value\";\r\n" +
            "key2:\"\\\"value\\\"\";\r\n" +
            "key3:'v';\r\nkey4:'\\'';\r\n" +
            "key5:10f;\r\n" +
            "key6:-99f;\r\n" +
            "key7:99.666f;\r\n" +
            "key8:0.333f;\r\n" +
            "key9:-0.333f;\r\n" +
            "key10:10f;\r\n" +
            "key11:-99d;\r\n" +
            "key12:99.666d;\r\n" +
            "key13:0.333d;\r\n" +
            "key14:-0.333d;\r\n" +
            "key15:false;\r\n" +
            "key16:true;\r\n" +
            "key17:[\"abc\",\"d\\\"ef\"];\r\n" +
            "key19:['a','\\''];\r\n" +
            "key20:[0f,-0.6f];\r\n" +
            "key21:[10d,-90.88d];\r\n" +
            "key22:[[0,1],[10,999,98]];\r\n";

        readonly Dictionary<string, object>  bpsData = new Dictionary<string, object>
            {
                { "key1", "value" },
                { "key2", "\"value\"" },
                { "key3", 'v' },
                { "key4", '\'' },
                { "key5", 10f },
                { "key6", -99f },
                { "key7", 99.666f },
                { "key8", 0.333f },
                { "key9", -0.333f },
                { "key10", 10f },
                { "key11", -99d },
                { "key12", 99.666d },
                { "key13", 0.333d },
                { "key14", -0.333d },
                { "key15", false },
                { "key16", true },
                { "key17", new string[2] { "abc", "d\"ef" }},
                { "key19", new char[2] { 'a', '\'' } },
                { "key20", new float[2] { 0.0f, -0.6f } },
                { "key21", new double[2] { 10.0d, -90.88d } },
                { "key22", new Array[2] { new int[2] { 0, 1 }, new int[3] { 10, 999, 98 } } }
            };

        [TestMethod]
		public void Plain_ValueTypes_Test()
		{
            // Arrange

            // Act
            string result = BPS.Plain(bpsData);

			// Assert
			Assert.AreEqual(strBpsData, result);
        }

        [TestMethod]
        public void Parse_Test()
        {
            // Arrange

            // Act
            var result = BPS.Parse(strBpsData);

            // Assert
            Assert.AreEqual(bpsData, result);
        }
    }
}