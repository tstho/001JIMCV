﻿using _001JIMCV.Models.Classes;
using _001JIMCV.Models;
using Microsoft.AspNetCore.Mvc;
using _001JIMCV.Models.Dals;
using System.Collections.Generic;
using _001JIMCV.ViewModels;
using _001JIMCV.Models.Classes.Enum;
using System.Linq;


namespace _001JIMCV.Controllers
{
    public class AccommodationController : Controller
    {
        private AccommodationDal accommodationDal;
        private LoginDal loginDal;
        private DashboardDal dashboardDal;
        public AccommodationController()
        {
            accommodationDal = new AccommodationDal();
            loginDal = new LoginDal();
            dashboardDal = new DashboardDal();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccommodationForm()
        {
            return View("AccommodationForm");
        }


        //Afficher la liste des accommodations

        public IActionResult GetList()
        {
           
            var accommodations = accommodationDal.GetAllAccommodations();
            ViewData["Accommodations"] = accommodations ?? new List<Accommodation>();
                return View("List");
          
        }
        //Méthode pour afficher les propositions d'hébérgement en fonction de l'utilisateur
        public ActionResult GetAccommodation()

        {//Récuperer l'Id de l'utilisateur connecté
            LoginViewModel viewModel = new LoginViewModel { Authentified = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentified)
            {
                viewModel.User = loginDal.GetUser(HttpContext.User.Identity.Name);
                UserEnum role = viewModel.User.Role;
                switch (role)
                {//si l'utilisateur est admin on affiche la liste des propositions 
                    case UserEnum.Admin:
                       return RedirectToAction("GetList");
                        //si partenaire n'afficher que ses propositions
                    case UserEnum.Provider:
                       
                        var accommodations = accommodationDal.GetAllAccommodations()
                            .Where(p => p.ProviderId == viewModel.User.Id).ToList();
                        ViewData["Accommodations"] = accommodations ?? new List<Accommodation>();
                        return View("List");
                    default:
                        
                        return View("Error");
                }
            }

            return View();
        }


        [HttpPost]
        public IActionResult CreateAccommodation(Accommodation accommodation)
        {
            LoginViewModel viewModel = new LoginViewModel { Authentified = HttpContext.User.Identity.IsAuthenticated };
          
            
                viewModel.User = loginDal.GetUser(HttpContext.User.Identity.Name);
                UserEnum role = viewModel.User.Role;

                accommodationDal.AddAccommodation(viewModel.User.Id, accommodation.ProviderName, accommodation.ProviderEmail,accommodation.Country, accommodation.City, accommodation.Type, accommodation.Name, accommodation.Adress, 
                    accommodation.StartDate, accommodation.EndDate, accommodation.Price, accommodation.Description,accommodation.Image);

            return RedirectToAction("GetAccommodation");
        }


        [HttpGet]
        public IActionResult EditAccommodation(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Accommodation accommodation = accommodationDal.GetAccommodationById(id);

            if (accommodation == null)
            {
                return View("Error");
            }

            return View("EditFormAccom", accommodation);
        }

       
        [HttpPost]
        public IActionResult EditAccommodation(Accommodation accommodation)
        {
            if (!ModelState.IsValid)
                return View(accommodation);

            if (accommodation.Id != 0)
            {
                accommodationDal.EditAccommodation(accommodation.Id, accommodation.ProviderName, accommodation.ProviderEmail, accommodation.Country, accommodation.City, accommodation.Type, accommodation.Name, accommodation.Adress, accommodation.StartDate, 
                    accommodation.EndDate, accommodation.Price, accommodation.Description, accommodation.Status, accommodation.Image);
                return RedirectToAction("GetAccommodation", new { id = accommodation.Id });
            }
            else
            {
                return View("Error");
            }
        }


        [HttpGet]
        public IActionResult DeleteAccommodation(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Accommodation accommodation = accommodationDal.GetAccommodationById(id);

            if (accommodation == null)
            {
                return View("Error");
            }

            return View(accommodation);
        }

        [HttpPost]
        public IActionResult DeleteAccommodation(Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return View(accommodation);
            }

            if (accommodation.Id != 0)
            {
                accommodationDal.DeleteAccommodation(accommodation);
                return RedirectToAction("GetAccommodation", new { id = accommodation.Id });
            }
            else
            {
                return View("Error");
            }
        }
       


    }
}

