using MySqlConnector;

namespace WebApplication3.Models;

public class Service
{
    public int id { get; set; }
    public int id_departement { get; set; }
    public string nom { get; set; }
    
    public static MySqlDataReader addService(Service service)
    {
        string query = $"INSERT INTO service (nom, id_departement) VALUES ('{service.nom}', {service.id_departement})";
        return DbConnection.requete(query);
        
    }
    
    public static MySqlDataReader deleteService(int id)
    {
        string query = $"DELETE FROM service WHERE id = {id}";
        return DbConnection.requete(query);
    }
    
    public static List<Service> getAllServices()
    {
        MySqlDataReader reader = DbConnection.requete("SELECT * FROM service;");
        List<Service> services = new List<Service>();
        while (reader.Read())
        {
            Service service = new Service();
            service.id = reader.GetInt32(0);
            service.nom = reader.GetString(2);
            service.id_departement = reader.GetInt32(1);
            services.Add(service);
        }
        
        reader.Close();

        return services;
    }
}