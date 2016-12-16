using Nancy;
using HairSalon.Objects;
using System.Collections.Generic;
using System;

namespace HairSalon
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

            // Get["/stylist/{id}/new_band"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //     return View["specific_stylist_client_form.cshtml", selectedStylist];
            // };
            //
            // Post["/stylist/{id}/new_client"] = parameters => {
            //     Dictionary<string, object> model = new Dictionary<string, object> ();
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //
            //     string clientName = Request.Form["client-name"];
            //     string clientDescription = Request.Form["client-description"];
            //     int clientStylistId = selectedStylist.GetId();
            //
            //     Client newClient = new Client(clientName, clientDescription, clientStylistId);
            //     newClient.Save();
            //     model.Add("client", newClient);
            //     model.Add("stylist", selectedStylist);
            //     return View["client_added.cshtml", model];
            // };
            //
            // Get["/stylist/edit/{id}"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //     return View["stylist_edit.cshtml", selectedStylist];
            // };
            //
            // Patch["/stylist/edit/{id}"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //
            //     string stylistName = Request.Form["stylist-name"];
            //     string stylistPhone = Request.Form["stylist-phone"];
            //     string stylistDescription = Request.Form["stylist-description"];
            //
            //     selectedStylist.Update(stylistName, stylistPhone, stylistDescription);
            //     Stylist updatedStylist = Stylist.Find(parameters.id);
            //     return View["stylist_edited.cshtml", updatedStylist];
            // };
            //
            // Get["/client/edit/{id}"] = parameters => {
            //     Dictionary<string, object> model = new Dictionary<string, object> ();
            //     Client selectedClient = Client.Find(parameters.id);
            //     Stylist selectedStylist = Stylist.Find(selectedClient.GetStylistId());
            //     model.Add("client", selectedClient);
            //     model.Add("all stylists", Stylist.GetAll());
            //     return View["client_edit.cshtml", model];
            // };
            //
            // Patch["/client/edit/{id}"] = parameters => {
            //     Dictionary<string, object> model = new Dictionary<string, object> ();
            //     Client selectedClient = Client.Find(parameters.id);
            //
            //     string clientName = Request.Form["client-name"];
            //     string clientDescription = Request.Form["client-description"];
            //     int clientStylistId = int.Parse(Request.Form["client-stylist-id"]);
            //
            //     selectedClient.Update(clientName, clientDescription, clientStylistId);
            //     Client updatedClient = Client.Find(parameters.id);
            //     Stylist selectedStylist = Stylist.Find(updatedClient.GetStylistId());
            //
            //     model.Add("client", updatedClient);
            //     model.Add("stylist", selectedStylist);
            //     return View["client_edited.cshtml", model];
            // };
            //
            // Get["/stylist/delete/{id}"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //     return View["stylist_delete.cshtml", selectedStylist];
            // };
            //
            // Delete["/stylist/delete/{id}"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //     selectedStylist.Delete();
            //     return View["entry_deleted.cshtml"];
            // };
            //
            // Get["/client/delete/{id}"] = parameters => {
            //     Dictionary<string, object> model = new Dictionary<string, object> ();
            //     Client selectedClient = Client.Find(parameters.id);
            //     Stylist selectedStylist = Stylist.Find(selectedClient.GetStylistId());
            //
            //     model.Add("client", selectedClient);
            //     model.Add("stylist", selectedStylist);
            //     return View["client_delete.cshtml", model];
            // };
            //
            // Delete["/client/delete/{id}"] = parameters => {
            //     Client selectedClient = Client.Find(parameters.id);
            //     selectedClient.Delete();
            //     return View["entry_deleted.cshtml"];
            // };
        }
    }
}
