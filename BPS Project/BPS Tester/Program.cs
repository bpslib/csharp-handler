using BPSLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Tester
{
    class Program
	{
        static void Main()
		{
            //TestLoadAndSave();
            //TestErrorParse();
            //TestPlain();
            //TestParse();
            TestLoadAndSave();
        }

        private static void TestErrorParse()
        {
            var toparse = "key:\"aaa;";

            var parsed = BPS.Parse(toparse);

            Console.WriteLine(string.Join(Environment.NewLine, parsed));
        }

        private static void TestPlain()
        {
            string plainedData = BPS.Plain(bpsData);
            Console.Write(plainedData);
        }

        private static void TestParse()
        {
            var parsedData = BPS.Parse(strBpsData);
            Console.WriteLine(string.Join(Environment.NewLine, parsedData));
        }

        private static void TestLoadAndSave()
        {
            var path1 = "C:\\temp\\file1.bps";
            var path2 = "C:\\temp\\file2.bps";

            var plain1 = BPS.Plain(bpsData);

            Save(path1, plain1);

            var data = Load(path1);

            var parsedData = BPS.Parse(data);

            var plain2 = BPS.Plain(parsedData);

            Save(path2, plain2);

            Console.WriteLine(string.Join(Environment.NewLine, parsedData));
        }

        private static string Load(string path)
        {
            string data;
            try
            {
                var sr = new StreamReader(path);
                data = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        private static void Save(string path, string data)
        {
            try
            {
                Directory.CreateDirectory(path.Remove(path.LastIndexOf(Path.DirectorySeparatorChar)));
                var sw = new StreamWriter(path, false);
                sw.Write(data);
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        static string strBpsData =
            "key1:\"value\";\r\n" +
            "key2:\"\\\"value\\\"\";\r\n" +
            "key3:'v';\r\n" +
            "key4:'\\'';\r\n" +
            "key23:'\\\\';\r\n" +
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
            "key22:[[0,1],[10,999,98]];\r\n" +
            "";

        static Dictionary<string, object> bpsData = new Dictionary<string, object>
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
                { "key17", new string[2] { "abc", "d\"ef" } },
                { "key19", new char[2] { 'a', '\'' } },
                { "key20", new float[2] { 0.0f, -0.6f } },
                { "key21", new double[2] { 10.0d, -90.88d } },
                { "key22", new Array[2] { new int[2] { 0, 1 }, new int[3] { 10, 999, 98 } } }
            };

    }
}
