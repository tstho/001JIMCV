﻿using _001JIMCV.Models.Classes;
using _001JIMCV.Models.Classes.Enum;
using System.Collections.Generic;
using System.Linq;

namespace _001JIMCV.Models.Dals
{
    public class JourneyDal
    {
        private BDDContext _bddContext;
        //Constructeur
        public JourneyDal()
        {
            _bddContext = new BDDContext();
        }
        public List<Journey> GetAllJourneys()
        {
            return _bddContext.Journeys.ToList();
        }
        public List<Flight> GetAllFLights()
        {
            return _bddContext.Flights.ToList();
        }
        public List<Accommodation> GetAllAccommodations()
        {
            return _bddContext.Accommodations.ToList();
        }
        public List<Activity> GetAllActivities()
        {
            return _bddContext.Activities.ToList();
        }
        public int AddJourney(string depDate, string returnDate, string country)
        {
            
            Journey journey = new Journey() { DepartureDate = depDate, ReturnDate = returnDate, CountryDestination = country };
            this._bddContext.Journeys.Add(journey);
            this._bddContext.SaveChanges();
            return journey.Id;
        }

        public int AddPackServices()
        {
            PackServices pack = new PackServices();
            this._bddContext.PackService.Add(pack);
            this._bddContext.SaveChanges();
            return pack.Id;
        }
        public void AddFlightPackServices(int depFlightId, int returnFlightId, int packServiceId)
        {
            FlightPackServices pack = new FlightPackServices() { DepartureFlightId = depFlightId, ReturnFlightId = returnFlightId, PackServicesId= packServiceId };
            this._bddContext.FlightPackServices.Add(pack);
            this._bddContext.SaveChanges();
        }

        public void AddAccommodationPackServices(int accommodationId, int packServiceId)
        {
            AccommodationPackServices AccommodationPackServices = new AccommodationPackServices() { AccommodationId = accommodationId, PackServicesId = packServiceId };
            this._bddContext.AccommodationPackServices.Add(AccommodationPackServices);
            this._bddContext.SaveChanges();
        }

        public void AddActivityPackServices(int activityId, int packServiceId)
        {
            ActivityPackServices ActivityPackServices = new ActivityPackServices() { ActivityId = activityId, PackServicesId = packServiceId };
            this._bddContext.ActivityPackServices.Add(ActivityPackServices);
            this._bddContext.SaveChanges();
        }
    }
}