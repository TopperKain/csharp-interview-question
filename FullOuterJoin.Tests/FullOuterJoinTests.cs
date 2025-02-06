using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FullOuterJoin;

namespace FullOuterJoin.Tests
{
    [TestClass]
    public class FullOuterJoinTests
    {
        [TestMethod]
        public void FullOuterJoin_BothCollectionsHaveElements_ReturnsCorrectResult()
        {
            var left = new List<(int Id, string Name)>
            {
                (1, "Left1"),
                (2, "Left2"),
                (3, "Left3")
            };

            var right = new List<(int Id, string Name)>
            {
                (2, "Right2"),
                (3, "Right3"),
                (4, "Right4")
            };

            var result = left.FullOuterJoin(
                right,
                l => l.Id,
                r => r.Id,
                (l, r, id) => new { Id = id, LeftName = l.Name, RightName = r.Name }
            ).ToList();

            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Any(r => r.Id == 1 && r.LeftName == "Left1" && r.RightName == null));
            Assert.IsTrue(result.Any(r => r.Id == 2 && r.LeftName == "Left2" && r.RightName == "Right2"));
            Assert.IsTrue(result.Any(r => r.Id == 3 && r.LeftName == "Left3" && r.RightName == "Right3"));
            Assert.IsTrue(result.Any(r => r.Id == 4 && r.LeftName == null && r.RightName == "Right4"));
        }

        [TestMethod]
        public void FullOuterJoin_LeftCollectionIsEmpty_ReturnsCorrectResult()
        {
            var left = new List<(int Id, string Name)>();

            var right = new List<(int Id, string Name)>
            {
                (2, "Right2"),
                (3, "Right3"),
                (4, "Right4")
            };

            var result = left.FullOuterJoin(
                right,
                l => l.Id,
                r => r.Id,
                (l, r, id) => new { Id = id, LeftName = l.Name, RightName = r.Name }
            ).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(r => r.Id == 2 && r.LeftName == null && r.RightName == "Right2"));
            Assert.IsTrue(result.Any(r => r.Id == 3 && r.LeftName == null && r.RightName == "Right3"));
            Assert.IsTrue(result.Any(r => r.Id == 4 && r.LeftName == null && r.RightName == "Right4"));
        }

        [TestMethod]
        public void FullOuterJoin_RightCollectionIsEmpty_ReturnsCorrectResult()
        {
            var left = new List<(int Id, string Name)>
            {
                (1, "Left1"),
                (2, "Left2"),
                (3, "Left3")
            };

            var right = new List<(int Id, string Name)>();

            var result = left.FullOuterJoin(
                right,
                l => l.Id,
                r => r.Id,
                (l, r, id) => new { Id = id, LeftName = l.Name, RightName = r.Name }
            ).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(r => r.Id == 1 && r.LeftName == "Left1" && r.RightName == null));
            Assert.IsTrue(result.Any(r => r.Id == 2 && r.LeftName == "Left2" && r.RightName == null));
            Assert.IsTrue(result.Any(r => r.Id == 3 && r.LeftName == "Left3" && r.RightName == null));
        }

        [TestMethod]
        public void FullOuterJoin_BothCollectionsAreEmpty_ReturnsEmptyResult()
        {
            var left = new List<(int Id, string Name)>();
            var right = new List<(int Id, string Name)>();

            var result = left.FullOuterJoin(
                right,
                l => l.Id,
                r => r.Id,
                (l, r, id) => new { Id = id, LeftName = l.Name, RightName = r.Name }
            ).ToList();

            Assert.AreEqual(0, result.Count);
        }
    }
}
