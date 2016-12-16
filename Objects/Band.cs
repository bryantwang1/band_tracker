using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker.Objects
{
    public class Band
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public string Description{get; set;}

        public Band(string bandName, string bandDescription, int bandId = 0)
        {
            this.Id = bandId;
            this.Name = bandName;
            this.Description = bandDescription;
        }

        public override bool Equals(System.Object otherBand)
        {
            if(!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.Id == newBand.Id;
                bool nameEquality = this.Name == newBand.Name;
                bool descriptionEquality = this.Description == newBand.Description;
                return(idEquality && nameEquality && descriptionEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM bands;DELETE FROM bands_venues;", conn);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Band> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Band> allBands = new List<Band> {};
            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                string bandDescription = rdr.GetString(2);

                Band newBand = new Band(bandName, bandDescription, bandId);
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, description) OUTPUT INSERTED.id VALUES (@BandName, @BandDescription);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@BandName";
            nameParameter.Value = this.Name;

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@BandDescription";
            descriptionParameter.Value = this.Description;

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);

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

        public static Band Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @SearchId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@SearchId";
            idParameter.Value = searchId;
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int bandId = 0;
            string bandName = null;
            string bandDescription = null;
            while(rdr.Read())
            {
                bandId = rdr.GetInt32(0);
                bandName = rdr.GetString(1);
                bandDescription = rdr.GetString(2);
            }
            Band foundBand = new Band(bandName, bandDescription, bandId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundBand;
        }

        public void AddVenue(Venue selectedVenue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

            SqlParameter bandParameter = new SqlParameter();
            bandParameter.ParameterName = "@BandId";
            bandParameter.Value = this.Id;

            SqlParameter venueParameter = new SqlParameter();
            venueParameter.ParameterName = "@VenueId";
            venueParameter.Value = selectedVenue.Id;

            cmd.Parameters.Add(bandParameter);
            cmd.Parameters.Add(venueParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Venue> GetVenues()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN bands_venues ON(venues.id = bands_venues.venue_id) JOIN bands ON (bands.id = bands_venues.band_id) WHERE bands.id = @BandId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@BandId";
            idParameter.Value = this.Id;
            cmd.Parameters.Add(idParameter);

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

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM bands_venues WHERE band_id = @BandId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@BandId";
            idParameter.Value = this.Id;
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public void Update(string newName, string newDescription)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName, description = @NewDescription WHERE id = @BandId;", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@NewName";
            nameParameter.Value = newName;

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@NewDescription";
            descriptionParameter.Value = newDescription;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@BandId";
            idParameter.Value = this.Id;

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }
    }
}
