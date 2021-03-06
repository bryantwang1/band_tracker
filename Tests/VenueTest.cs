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

        [Fact]
        public void Test_Delete_DeletesBandFromDatabase()
        {
            Venue venue1 = new Venue("Staples Center", "Los Angeles, CA");
            venue1.Save();

            Venue venue2 = new Venue("Staples Center", "Los Angeles, CA");
            venue2.Save();

            Venue venue3 = new Venue("Staples Center", "Los Angeles, CA");
            venue3.Save();

            venue2.Delete();

            List<Venue> expected = Venue.GetAll();
            List<Venue> result = new List<Venue> {venue1, venue3};

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetUnrelatedVenues_ReturnsListOfUnassociatedVenues()
        {

            Venue testVenue1 = new Venue("Staples Center", "Los Angeles, CA");
            testVenue1.Save();

            Venue testVenue2 = new Venue("Staples Center", "Los Angeles, CA");
            testVenue2.Save();

            Band band1 = new Band("Some Band Name", "Some Band Description");
            band1.Save();

            Band band2 = new Band("Some Band Name", "Some Band Description");
            band2.Save();

            Band band3 = new Band("Some Band Name", "Some Band Description");
            band3.Save();

            testVenue1.AddBand(band1);
            testVenue2.AddBand(band2);

            List<Band> result = testVenue1.GetUnrelatedBands();
            List<Band> expected = new List<Band> {band2, band3};

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_RemoveBand_DisassociatesBandFromVenue()
        {
            Venue testVenue1 = new Venue("Staples Center", "Los Angeles, CA");
            testVenue1.Save();

            Band band1 = new Band("Some Band Name", "Some Band Description");
            band1.Save();

            Band band2 = new Band("Some Band Name", "Some Band Description");
            band2.Save();

            Band band3 = new Band("Some Band Name", "Some Band Description");
            band3.Save();

            testVenue1.AddBand(band1);
            testVenue1.AddBand(band2);
            testVenue1.AddBand(band3);

            testVenue1.RemoveBand(band2);

            List<Band> result = testVenue1.GetBands();
            List<Band> expected = new List<Band> {band1,band3};

            Assert.Equal(expected, result);
        }
    }
}
