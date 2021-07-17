using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPSLib;

namespace BPS_UnitTest
{
	[TestClass]
	public class BPSFileTest
	{
		[TestMethod]
		public void CountTest()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Act
			int count = file.Count();

			// Assert
			Assert.AreEqual(2, count);
		}

		[TestMethod]
		public void CountTest_Empty()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			int count = file.Count();

			// Assert
			Assert.AreEqual(0, count);
		}

		[TestMethod]
		public void ContainsTest()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Act
			bool contain = file.Contains("key2");

			// Assert
			Assert.IsTrue(contain);
		}

		[TestMethod]
		public void ContainsTest_NoExist()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Act
			bool contain = file.Contains("key3");

			// Assert
			Assert.IsFalse(contain);
		}

		[TestMethod]
		public void AddTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key0", "value");
			file.Add("key1", "value");
			file.Add("key2", "value");
			file.Add("key3", "value");

			// Assert
			Assert.AreEqual(4, file.Count());
		}

		[TestMethod]
		public void AddTest_AlreadyExist()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key0", "value");
			file.Add("key0", "value");
			file.Add("key0", "value");
			file.Add("key1", "value");

			// Assert
			Assert.AreEqual(2, file.Count());
		}

		[TestMethod]
		public void RemoveTest()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");

			// Act
			file.Remove("key");

			// Assert
			Assert.AreEqual(0, file.Count());
		}

		[TestMethod]
		public void RemoveTest_Nothing()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Remove("key");

			// Assert
			Assert.AreEqual(0, file.Count());
		}

		[TestMethod]
		public void RemoveTest_MoreThanOne()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key1", "value");
			file.Add("key2", "value");

			// Act
			file.Remove("key1");

			// Assert
			Assert.AreEqual(2, file.Count());
		}

		[TestMethod]
		public void FindTest()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");

			// Act
			string value = file.Find("key").ToString();

			// Assert
			Assert.AreEqual("value", value);
		}

		[TestMethod]
		public void FindTest_MoreThanOne()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key1", "value");
			file.Add("key2", "value");
			file.Add("key3", "value");

			// Act
			string value = file.Find("key2").ToString();

			// Assert
			Assert.AreEqual("value", value);
		}

		[TestMethod]
		public void ClearTest()
		{
			// Arrange
			BPSFile file = new BPSFile();
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Act
			file.Clear();

			// Assert
			Assert.AreEqual(0, file.Count());
		}
	}
}
