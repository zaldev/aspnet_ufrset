using MySqlConnector;

namespace WebApplication3.Models;

public class Professeur
{
    public int id { get; set; }
    public string nom { get; set; }
    public string specialite { get; set; }
    
    public int id_filiere { get; set; }
    public bool is_per { get; set; }
    
    public static MySqlDataReader addProf(Professeur professeur)
    {
        string query = $"INSERT INTO professeur (nom, specialite, is_per, id_filiere) VALUES ('{professeur.nom}', '{professeur.specialite}', {professeur.is_per}, {professeur.id_filiere})";
        // string q2 = "INSERT INTO professeur (nom, specialite, is_per) VALUES (''" + professeur.nom + "', '" + professeur.specialite + "', " + professeur.is_per + ")";
        return DbConnection.requete(query);
        
    }
    
    public static MySqlDataReader deleteProfesseur(int id)
    {
        string query = $"DELETE FROM professeur WHERE id = {id}";
        return DbConnection.requete(query);
    }

    public static List<Professeur> getAllProfesseurs()
    {
        MySqlDataReader reader = DbConnection.requete("SELECT * FROM professeur;");
        List<Professeur> professeurs = new List<Professeur>();
        while (reader.Read())
        {
            Professeur professeur = new Professeur();
            professeur.id = reader.GetInt32(0);
            professeur.nom = reader.GetString(1);
            professeur.specialite = reader.GetString(2);
            professeur.is_per = reader.GetBoolean(3);
            professeur.id_filiere = reader.GetInt32(4);
            professeurs.Add(professeur);
        }
        
        reader.Close();

        return professeurs;
    }
}