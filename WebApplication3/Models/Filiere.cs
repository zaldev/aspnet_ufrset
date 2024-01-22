using MySqlConnector;

namespace WebApplication3.Models;

public class Filiere
{
    public int id { get; set; }
    public string nom { get; set; }
    
    public static MySqlDataReader addFiliere(Filiere filiere)
    {
        string query = $"INSERT INTO filiere (nom) VALUES ('{filiere.nom}')";
        return DbConnection.requete(query);
        
    }
    
    public static MySqlDataReader deleteFiliere(int id)
    {
        string query = $"DELETE FROM filiere WHERE id = {id}";
        return DbConnection.requete(query);
    }

    public static List<Filiere> GetAllFilieres()
    {
        MySqlDataReader reader = DbConnection.requete("SELECT * FROM filiere;");
        List<Filiere> filieres = new List<Filiere>();
        while (reader.Read())
        {
            Filiere filiere = new Filiere();
            filiere.id = reader.GetInt32(0);
            filiere.nom = reader.GetString(1);
            filieres.Add(filiere);
        }
        
        reader.Close();

        return filieres;
    }
}