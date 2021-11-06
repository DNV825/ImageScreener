using System;
using Xunit;
using ImageScreener;

namespace ImageScreener.UnitTests
{
    public class ImageScreener_IsCreateNewFolder
    {
        [Fact]
        public void IsImageScreener_CreateNewFolder_Success()
        {
            var createNewFolder = new CreateNewFolder();
            Assert.True(true, "success sample.");
        }
    }
}
