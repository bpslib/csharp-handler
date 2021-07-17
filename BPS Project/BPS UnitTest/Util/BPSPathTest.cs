using BPSLib.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BPS_UnitTest.Util
{
	[TestClass]
	public class BPSPathTest
	{
		[TestMethod]
		public void NormalizeTest()
		{
			// Arrange
			string path = "c:\\users\\name.bps";

			// Act
			string normalized = BPSPath.Normalize("c:\\users\\name");

			// Assert
			Assert.AreEqual(path, normalized);
		}

		[TestMethod]
		public void NormalizeTest_WithoutChange()
		{
			// Arrange
			string path = "c:\\users\\name.bps";

			// Act
			string normalized = BPSPath.Normalize("c:\\users\\name.bps");

			// Assert
			Assert.AreEqual(path, normalized);
		}

		[TestMethod]
		public void RemoveExtensionTest()
		{
			// Arrange
			string path = "c:\\users\\name";

			// Act
			string normalized = BPSPath.RemoveExtension("c:\\users\\name.bps");

			// Assert
			Assert.AreEqual(path, normalized);
		}

		[TestMethod]
		public void RemoveExtensionTest_WithoutChange()
		{
			// Arrange
			string path = "c:\\users\\name";

			// Act
			string normalized = BPSPath.RemoveExtension("c:\\users\\name");

			// Assert
			Assert.AreEqual(path, normalized);
		}
	}
}
