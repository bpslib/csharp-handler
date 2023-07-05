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
            TestPlain();
        }

        private static void TestErrorParse()
        {
            var toparse = "key:\"aaa;";

            var parsed = BPS.Parse(toparse);

            Console.WriteLine(string.Join(Environment.NewLine, parsed));
        }

        private static void TestPlain()
        {
            var bpsData = new Dictionary<string, object>
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

            string result = BPS.Plain(bpsData);

            Console.Write(result);
        }

        private static void TestLoadAndSave()
        {
            var path = "C:\\temp\\file.bps";

            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("test", 10);
            //data.TryGetValue("test", out object testData);
            //Console.WriteLine(testData);
            data.TryAdd("test2", "lol");
            //data.TryGetValue("test", out testData);
            //Console.WriteLine(testData);
            data.Add("asd", null);

            Save(path, BPS.Plain(data));

            var parsedData = BPS.Parse(Load(path));

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
                var sw = new StreamWriter(path);
                sw.Write(data);
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
