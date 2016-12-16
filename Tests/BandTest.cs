using Xunit;
using BandTracker.Objects;
using System.Collections.Generic;
using System;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = band_tracker_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Band.DeleteAll();
        }

        [Fact]
        public void Test_BandsEmptyAtFirst()
        {
            int result = Band.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Band band1 = new Band("Some Band Name", "Some Band Description");
            Band band2 = new Band("Some Band Name", "Some Band Description");

            Assert.Equal(band1, band2);
        }

        // [Fact]
        public void Test_Save_SavesBandToDatabase()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            List<Band> result = Band.GetAll();
            List<Band> testList = new List<Band> {testBand};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToSavedObject()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            Band savedBand = Band.GetAll()[0];

            int result = savedBand.Id;
            int expected = testBand.Id;

            Assert.Equal(expected, result);
        }
        //
        // [Fact]
        // public void Test_Find_ReturnsSpecificBandFromDatabase()
        // {
        //     Band testBand = new Band("Some Band Name", "Some Band Description");
        //     testBand.Save();
        //
        //     Band result = Band.Find(testBand.Id);
        //
        //     Assert.Equal(result, testBand);
        // }
        //
        // [Fact]
        // public void Test_Update_UpdatesBandInDatabase()
        // {
        //     Band testBand = new Band("Some Band Name", "Some Band Description");
        //     testBand.Save();
        //
        //     string newName = "Some Other Band Name";
        //     string newDescription = "Some Other Band Description";
        //
        //     testBand.Update(newName, newDescription);
        //
        //     Band result = Band.GetAll()[0];
        //     Band expected = new Band(newName, newDescription, testBand.Id);
        //
        //     Assert.Equal(expected, result);
        // }
    }
}
