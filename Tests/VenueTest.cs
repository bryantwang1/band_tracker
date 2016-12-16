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
    }
}
