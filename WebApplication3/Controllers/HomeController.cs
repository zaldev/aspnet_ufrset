using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private List<Professeur> professeurs;
    private List<Departement> departements;
    private List<Filiere> filieres;
    private List<Service> services;
    
    

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        init();
    }
    
    private void init(string m= "")
    {
        switch (m)
        {
            case "p":
                professeurs = Professeur.getAllProfesseurs();
                break;
            case "d":
                departements = Departement.getAllDepartements();
                break;
            case "f":
                filieres = Filiere.GetAllFilieres();
                break;
            case "s":
                services = Service.getAllServices();
                break;
            default:
                professeurs = Professeur.getAllProfesseurs();
                departements = Departement.getAllDepartements();
                filieres = Filiere.GetAllFilieres();
                services = Service.getAllServices();
                break;
        }
        
    }

    public IActionResult Index()
    {
        var items = new Tuple<IEnumerable<Departement>, IEnumerable<Professeur>, IEnumerable<Filiere>,IEnumerable<Service>>(departements, professeurs, filieres,services);
        return View(items);    }

    public IActionResult Departements()
    {
        var items = new Tuple<IEnumerable<Departement>, IEnumerable<Professeur>, IEnumerable<Filiere>,IEnumerable<Service>>(departements, professeurs, filieres,services);
        return View(items);
    } 
    
    public IActionResult addDepartement()
    {
        if (Request.Method == "POST")
        {
            Departement departement = new Departement();
            departement.nom = Request.Form["nom"];
            departement.id_filiere = int.Parse(Request.Form["id_filiere"]);
            departement.id_professeur = int.Parse(Request.Form["id_chef"]);
            MySqlDataReader reader = Departement.addDepartement(departement); 
            Console.WriteLine(reader.RecordsAffected);
            reader.Close();
            init("d");
            
        }
        
        return RedirectToAction("Departements");
    }
    
    public IActionResult delDepartement()
    {
        if (Request.Method == "POST")
        {
            int id = int.Parse(Request.Form["id"]);
            MySqlDataReader reader = Departement.deleteDepartement(id);
            reader.Close();
            init("d");
        }
        return RedirectToAction("Departements");
    }

    
    public IActionResult Filieres()
    {
        var items = new Tuple<IEnumerable<Filiere>, IEnumerable<Departement>>(filieres, departements);
        return View(items);
    } 
    
    public IActionResult addFiliere()
    {
        if (Request.Method == "POST")
        {
            Filiere filiere = new Filiere();
            filiere.nom = Request.Form["nom"];
            MySqlDataReader reader = Filiere.addFiliere(filiere);
            reader.Close();
            init("f");
        }
        
        return RedirectToAction("Filieres");
    }
    
    public IActionResult delFiliere()
    {
        if (Request.Method == "POST")
        {
            int id = int.Parse(Request.Form["id"]);
            MySqlDataReader reader = Filiere.deleteFiliere(id);
            reader.Close();
            init("f");
        }
        return RedirectToAction("Filieres");
    }

    
    public IActionResult Professeurs(){
        var items = new Tuple<IEnumerable<Professeur>, IEnumerable<Departement>>(professeurs, departements);
        return View(items);
    }
    
    public IActionResult addProfesseur()
    {
        if (Request.Method == "POST")
        {
            Professeur professeur = new Professeur();
            professeur.nom = Request.Form["nom"];
            professeur.specialite = Request.Form["specialite"];
            professeur.is_per = Request.Form["is_per"] == "True";
            MySqlDataReader reader = Professeur.addProf(professeur);
            reader.Close();
            init("p"); 
        }
        
        return RedirectToAction("Professeurs");
    }
    
    public IActionResult delProfesseur()
    {
        Console.WriteLine("delProfesseur");
        if (Request.Method == "POST")
        {
            int id = int.Parse(Request.Form["id"]);
            Console.WriteLine(id);
            MySqlDataReader reader = Professeur.deleteProfesseur(id);
            Console.WriteLine(reader.RecordsAffected);
            reader.Close();
            init("p");
        }
        return RedirectToAction("Professeurs");
    }
    
    
    public IActionResult Services()
    {
        var items = new Tuple<IEnumerable<Service>, IEnumerable<Departement>>(services, departements);
        return View(items);
    }
    
    public IActionResult addService()
    {
        if (Request.Method == "POST")
        {
            Service service = new Service();
            service.nom = Request.Form["nom"];
            service.id_departement = int.Parse(Request.Form["id_departement"]);
            MySqlDataReader reader = Service.addService(service);
            reader.Close();
            init("s");
        }
        
        return RedirectToAction("Services");
    }
    
    public IActionResult delService()
    {
        if (Request.Method == "POST")
        {
            int id = int.Parse(Request.Form["id"]);
            MySqlDataReader reader = Service.deleteService(id);
            reader.Close();
            init("s");
        }
        return RedirectToAction("Services");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}