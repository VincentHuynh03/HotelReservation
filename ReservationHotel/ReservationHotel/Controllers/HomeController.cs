﻿using Microsoft.AspNetCore.Mvc;
using ReservationHotel.Models;
using System.Diagnostics;

namespace ReservationHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Réserver()
        {
            return View();
        }

        public IActionResult GestionRéservation()
        {
            return View();
        }
        
        public IActionResult GestionChambres()
        {
            return View();
        }

        public IActionResult GestionUtilisateurs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}