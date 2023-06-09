﻿using _001JIMCV.Models.Classes;
using _001JIMCV.Models.Classes.Enum;
using _001JIMCV.Models.Dals;
using _001JIMCV.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using User = _001JIMCV.Models.Classes.User;

public class DashboardController : Controller
{
    private DashboardDal dashboardDal;
    private LoginDal loginDal;
    private AccommodationDal accommodationDal;
    public DashboardController()
    {
        dashboardDal = new DashboardDal();
        loginDal = new LoginDal();
    }

    public ActionResult Index()
    {
        LoginViewModel viewModel = new LoginViewModel { Authentified = HttpContext.User.Identity.IsAuthenticated };
        if (viewModel.Authentified)
        {
            viewModel.User = loginDal.GetUser(HttpContext.User.Identity.Name);
            UserEnum role = viewModel.User.Role; // Accéder au rôle de l'utilisateur

            string name = viewModel.User.Name;
            string email = viewModel.User.Email;
            string phone = viewModel.User.Phone;
            string gender = viewModel.User.Gender;
            string country = viewModel.User.Country;
            string city = viewModel.User.City;

            ViewData["UserName"] = name;
            ViewData["UserEmail"] = email;
            ViewData["UserPhone"] = phone;
            ViewData["UserGender"] = gender;
            ViewData["UserCountry"] = country;
            ViewData["UserCity"] = city;

            switch (role)
            {
                case UserEnum.Admin:
                    // administrateur
                    return View("AdminDashboard", viewModel);
                case UserEnum.Customer:
                    //  client
                    return View("ClientDashboard", viewModel);
                case UserEnum.Provider:
                    //  partenaire
                    return View("ProviderDashboard", viewModel);
                default:
                    // Rôle non reconnu
                    return View("Error");
            }
        }

        return View(viewModel);
    }
    [HttpGet]
    public IActionResult EditProfil(int id)
    {
        LoginViewModel viewModel = new LoginViewModel { Authentified = HttpContext.User.Identity.IsAuthenticated };
        if (viewModel.Authentified)
        {
            viewModel.User = loginDal.GetUser(HttpContext.User.Identity.Name);
            UserEnum role = viewModel.User.Role; // Accéder au rôle de l'utilisateur

             id = viewModel.User.Id;
        }
            if (id == 0)
        {
            return NotFound();
        }

        User user = dashboardDal.GetUser(id);

        if (user == null)
        {
            return View("Error");
        }

        return View("EditProfil", user);
    }

    [HttpPost]
    public IActionResult EditProfil(User user)
    {
        if (ModelState.IsValid)
        {
            if (user.Id != 0)
            {
                dashboardDal.EditProfil(user.Id, user.Name, user.Email, user.Phone, user.Gender, user.Country, user.City);
                return RedirectToAction("Index", new { id = user.Id });
            }
            else
            {
                return View("Error");
            }
        }
        else
        {
            return View(user);
        }
    }

}



