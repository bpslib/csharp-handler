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

			// Act
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Assert
			Assert.AreEqual(2, file.Count());
		}

		[TestMethod]
		public void ContainsTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key", "value");
			file.Add("key2", "value2");

			// Assert
			Assert.IsTrue(file.Contains("key2"));
			file.Clear();
			Assert.IsFalse(file.Contains("key"));
		}

		[TestMethod]
		public void AddTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key", "value");

			// Assert
			Assert.AreEqual(1, file.Count());
		}

		[TestMethod]
		public void RemoveTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key", "value");
			file.Remove("key");

			// Assert
			Assert.AreEqual(0, file.Count());
		}

		[TestMethod]
		public void FindTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key", "value");

			// Assert
			Assert.AreEqual("value", file.Find("key"));
		}

		[TestMethod]
		public void ClearTest()
		{
			// Arrange
			BPSFile file = new BPSFile();

			// Act
			file.Add("key", "value");
			file.Add("key2", "value2");
			file.Clear();

			// Assert
			Assert.AreEqual(0, file.Count());
		}
	}
}
