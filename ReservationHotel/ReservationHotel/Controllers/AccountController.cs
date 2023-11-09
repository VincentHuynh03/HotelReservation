using Microsoft.AspNetCore.Mvc;

namespace ReservationHotel.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        // Action pour gérer la soumission du formulaire de connexion
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Vérifiez les informations d'identification (c'est une simulation)
            if (username == "root" && password == "root")
            {
                // Créez une session pour l'utilisateur connecté
                HttpContext.Session.SetString("UserName", username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // En cas d'échec de connexion, affichez un message d'erreur
                ViewBag.ErrorMessage = "Identifiants incorrects";
                return View();
            }
        }
        // Action pour se déconnecter (logout)
        public IActionResult Logout()
        {
            // Supprimez la session de l'utilisateur
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
