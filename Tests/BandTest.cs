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
            Venue.DeleteAll();
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

        [Fact]
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

        [Fact]
        public void Test_Find_ReturnsSpecificBandFromDatabase()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            Band result = Band.Find(testBand.Id);

            Assert.Equal(result, testBand);
        }

        [Fact]
        public void Test_GetVenues_ReturnsAllBandVenues()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            testBand.AddVenue(testVenue);

            List<Venue> savedVenue = testBand.GetVenues();
            List<Venue> expected = new List<Venue> {testVenue};

            Assert.Equal(expected, savedVenue);
        }

        [Fact]
        public void Test_AddVenue_CreatesAssociationWithAVenueInDatabase()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            Venue venue1 = new Venue("Staples Center", "Los Angeles, CA");
            venue1.Save();

            Venue venue2 = new Venue("Staples Center", "Los Angeles, CA");
            venue2.Save();

            testBand.AddVenue(venue1);

            List<Venue> result = testBand.GetVenues();
            List<Venue> expected = new List<Venue> {venue1};

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Delete_DeletesBandFromDatabase()
        {
            Band band1 = new Band("Some Band Name", "Some Band Description");
            band1.Save();

            Band band2 = new Band("Some Band Name", "Some Band Description");
            band2.Save();

            Band band3 = new Band("Some Band Name", "Some Band Description");
            band3.Save();

            band2.Delete();

            List<Band> expected = Band.GetAll();
            List<Band> result = new List<Band> {band1, band3};

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Update_UpdatesBandInDatabase()
        {
            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            string newName = "nano.RIPE";
            string newDescription = "A Japanese pop rock band with two core members.";

            testBand.Update(newName, newDescription);

            Band result = Band.GetAll()[0];
            Band expected = new Band(newName, newDescription, testBand.Id);

            Assert.Equal(expected, result);
        }
    }
}
