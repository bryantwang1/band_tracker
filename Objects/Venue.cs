using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker.Objects
{
    public class Venue
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public string Location{get; set;}

        public Venue(string VenueName, string VenueLocation, int VenueId = 0)
        {
            this.Id = VenueId;
            this.Name = VenueName;
            this.Location = VenueLocation;
        }

        public override bool Equals(System.Object otherVenue)
        {
            if(!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue) otherVenue;
                bool idEquality = this.Id == newVenue.Id;
                bool nameEquality = this.Name == newVenue.Name;
                bool locationEquality = this.Location == newVenue.Location;
                return(idEquality && nameEquality && locationEquality);
            }
        }

        public static List<Venue> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Venue> allVenues = new List<Venue> {};
            while(rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);
                string venueLocation = rdr.GetString(2);

                Venue newVenue = new Venue(venueName, venueLocation, venueId);
                allVenues.Add(newVenue);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allVenues;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM venues;DELETE FROM bands_venues;", conn);
            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, location) OUTPUT INSERTED.id VALUES (@VenueName, @VenueLocation);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@venueName";
            nameParameter.Value = this.Name;

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@VenueLocation";
            locationParameter.Value = this.Location;

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(locationParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.Id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Venue Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @SearchId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@SearchId";
            idParameter.Value = searchId;
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int venueId = 0;
            string venueName = null;
            string venueLocation = null;
            while(rdr.Read())
            {
                venueId = rdr.GetInt32(0);
                venueName = rdr.GetString(1);
                venueLocation = rdr.GetString(2);

            }
            Venue foundVenue = new Venue(venueName, venueLocation, venueId);
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundVenue;
        }

        public void Update(string newName, string newLocation)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName, location = @NewLocation WHERE id = @VenueId;", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@NewName";
            nameParameter.Value = newName;

            SqlParameter locationParameter = new SqlParameter();
            locationParameter.ParameterName = "@NewLocation";
            locationParameter.Value = newLocation;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@VenueId";
            idParameter.Value = this.Id;

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(locationParameter);
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public void AddBand(Band selectedBand)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

            SqlParameter bandParameter = new SqlParameter();
            bandParameter.ParameterName = "@BandId";
            bandParameter.Value = selectedBand.Id;

            SqlParameter venueParameter = new SqlParameter();
            venueParameter.ParameterName = "@VenueId";
            venueParameter.Value = this.Id;

            cmd.Parameters.Add(bandParameter);
            cmd.Parameters.Add(venueParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Band> GetBands()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON(venues.id = bands_venues.venue_id) JOIN bands ON (bands.id = bands_venues.band_id) WHERE venues.id = @VenueId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@VenueId";
            idParameter.Value = this.Id;
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Band> allBands = new List<Band> {};
            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                string bandLocation = rdr.GetString(2);

                Band newBand = new Band(bandName, bandLocation, bandId);
                allBands.Add(newBand);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allBands;
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@VenueId";
            idParameter.Value = this.Id;
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }
    }
}
