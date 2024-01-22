using MySqlConnector;

namespace WebApplication3.Models;

public class Departement
{
    public int id { get; set; }
    public string nom { get; set; }
    public int id_filiere { get; set; }
    public int id_professeur { get; set; }
    
    public static List<Departement> getAllDepartements()
    {
        MySqlDataReader reader = DbConnection.requete("SELECT * FROM departement;");
        List<Departement> departements = new List<Departement>();
        while (reader.Read())
        {
            Departement departement = new Departement();
            departement.id = reader.GetInt32(0);
            departement.nom = reader.GetString(2);
            departement.id_filiere = reader.GetInt32(1);
            departement.id_professeur = reader.GetInt32(3);
            departements.Add(departement);
        }
        
        reader.Close();

        return departements;
    }
    
    public static MySqlDataReader addDepartement(Departement departement)
    {
        string query = $"INSERT INTO departement (nom, id_filiere, id_chef) VALUES ('{departement.nom}', {departement.id_filiere}, {departement.id_professeur})";
        return DbConnection.requete(query);
        
    }
    
    public static MySqlDataReader deleteDepartement(int id)
    {
        string query = $"DELETE FROM departement WHERE id = {id}";
        return DbConnection.requete(query);
    }
}