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

            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
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
    }
}
