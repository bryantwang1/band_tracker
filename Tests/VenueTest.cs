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
    }
}
