using Nancy;
using BandTracker.Objects;
using System.Collections.Generic;
using System;

namespace BandTracker
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Band> allBands = Band.GetAll();
                return View["index.cshtml", allBands];
            };

            Get["/venues"] = _ => {
                List<Venue> allVenues = Venue.GetAll();
                return View["venues.cshtml", allVenues];
            };

            Get["/venues/new"] = _ => {
                return View["venue_form.cshtml"];
            };

            Post["/venues/new"] = _ => {
                string venueName = Request.Form["venue-name"];
                string venueLocation = Request.Form["venue-location"];

                Venue newVenue = new Venue(venueName, venueLocation);
                newVenue.Save();
                return View["venue_added.cshtml", newVenue];
            };

            Get["/bands/new"] = _ => {
                return View["band_form.cshtml"];
            };

            Post["/bands/new"] = _ => {
                string bandName = Request.Form["band-name"];
                string bandDescription = Request.Form["band-description"];

                Band newBand = new Band(bandName, bandDescription);
                newBand.Save();
                return View["band_added.cshtml", newBand];
            };

            Get["/venue/{id}"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["venue.cshtml", selectedVenue];
            };

            Get["/band/{id}"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                return View["band.cshtml", selectedBand];
            };

            Get["/venue/{id}/new_band"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["venue_add_band.cshtml", selectedVenue];
            };

            Post["/venue/{id}/new_band"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                int bandId = int.Parse(Request.Form["band-id"]);

                Band selectedBand = Band.Find(bandId);
                selectedVenue.AddBand(selectedBand);
                return View["venue.cshtml", selectedVenue];
            };

            Get["/band/{id}/new_venue"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                return View["band_add_venue.cshtml", selectedBand];
            };

            Post["/band/{id}/new_venue"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                int venueId =int.Parse(Request.Form["venue-id"]);

                Venue selectedVenue = Venue.Find(venueId);
                selectedBand.AddVenue(selectedVenue);
                return View["band.cshtml", selectedBand];
            };

            Get["/venue/edit/{id}"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["venue_edit.cshtml", selectedVenue];
            };

            Patch["/venue/edit/{id}"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);

                string venueName = Request.Form["venue-name"];
                string venueLocation = Request.Form["venue-location"];

                selectedVenue.Update(venueName, venueLocation);
                Venue updatedVenue = Venue.Find(parameters.id);
                return View["venue_edited.cshtml", updatedVenue];
            };

            Get["/band/edit/{id}"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                return View["band_edit.cshtml", selectedBand];
            };

            Patch["/band/edit/{id}"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);

                string bandName = Request.Form["band-name"];
                string bandDescription = Request.Form["band-description"];

                selectedBand.Update(bandName, bandDescription);
                Band updatedBand = Band.Find(parameters.id);
                return View["band_edited.cshtml", updatedBand];
            };

            Get["/venue/delete/{id}"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["venue_delete.cshtml", selectedVenue];
            };

            Delete["/venue/delete/{id}"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                selectedVenue.Delete();
                return View["entry_deleted.cshtml"];
            };

            Get["/band/delete/{id}"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                return View["band_delete.cshtml", selectedBand];
            };

            Delete["/band/delete/{id}"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                selectedBand.Delete();
                return View["entry_deleted.cshtml"];
            };
        }
    }
}
