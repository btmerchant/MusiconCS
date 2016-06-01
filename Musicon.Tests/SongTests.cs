using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicon.Models;
using Musicon.DAL;

namespace Musicon.Tests
{
    [TestClass]
    public class SongTests
    {
        [TestMethod]
        public void SongEnsureICanCreateAnInstance()
        {
            Song S = new Song();
            Assert.IsNotNull(S);
        }
    }
}
