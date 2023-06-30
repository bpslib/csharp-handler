using BPSLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tester
{
    class Program
	{
		static void Main()
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

        public static string Load(string path)
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

        public static void Save(string path, string data)
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
