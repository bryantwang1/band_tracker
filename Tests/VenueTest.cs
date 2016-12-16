using Xunit;
using BandTracker.Objects;
using System.Collections.Generic;
using System;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = band_tracker_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Venue.DeleteAll();
            Band.DeleteAll();
        }

        [Fact]
        public void Test_VenuesEmptyAtFirst()
        {
            int result = Venue.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Venue venue1 = new Venue("Staples Center", "Los Angeles, CA");
            Venue venue2 = new Venue("Staples Center", "Los Angeles, CA");

            Assert.Equal(venue1, venue2);
        }

        [Fact]
        public void Test_Save_SavesVenueToDatabase()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            List<Venue> result = Venue.GetAll();
            List<Venue> testList = new List<Venue> {testVenue};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToSavedObject()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            Venue savedVenue = Venue.GetAll()[0];

            int result = savedVenue.Id;
            int expected = testVenue.Id;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Find_ReturnsSpecificVenueFromDatabase()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            Venue result = Venue.Find(testVenue.Id);

            Assert.Equal(result, testVenue);
        }

        [Fact]
        public void Test_Update_UpdatesVenueInDatabase()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            string newName = "Arlene Schnitzer Concert Hall";
            string newLocation = "Portland, OR";

            testVenue.Update(newName, newLocation);

            Venue result = Venue.GetAll()[0];
            Venue expected = new Venue(newName, newLocation, testVenue.Id);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetBands_ReturnsAllVenueBands()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            Band testBand = new Band("Some Band Name", "Some Band Description");
            testBand.Save();

            testVenue.AddBand(testBand);

            List<Band> savedBand = testVenue.GetBands();
            List<Band> expected = new List<Band> {testBand};

            Assert.Equal(expected, savedBand);
        }

        [Fact]
        public void Test_AddBand_CreatesAssociationWithABandInDatabase()
        {
            Venue testVenue = new Venue("Staples Center", "Los Angeles, CA");
            testVenue.Save();

            Band band1 = new Band("Some Band Name", "Some Band Description");
            band1.Save();

            Band band2 = new Band("Some Band Name", "Some Band Description");
            band2.Save();

            testVenue.AddBand(band1);

            List<Band> result = testVenue.GetBands();
            List<Band> expected = new List<Band> {band1};

            Assert.Equal(expected, result);
        }
    }
}
