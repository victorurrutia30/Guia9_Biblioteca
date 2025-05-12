using Microsoft.Data.SqlClient;

namespace GestionBiblioteca.Models
{
    public class MantenimientoAutor
    {
        private SqlConnection conexion;

        public List<Autor> ListarTodos()
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            List<Autor> lista = new List<Autor>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autores", conexion);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Autor
                {
                    CodAutor = dr["CodAutor"].ToString(),
                    NombreAutor = dr["NombreAutor"].ToString(),
                    Nacionalidad = dr["Nacionalidad"].ToString(),
                    FechaNacimiento = dr["FechaNacimiento"] == DBNull.Value ? null : Convert.ToDateTime(dr["FechaNacimiento"])
                });
            }

            conexion.Close();
            return lista;
        }

        public Autor Consultar(string cod)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Autores WHERE CodAutor = @cod", conexion);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            Autor autor = new Autor();
            if (dr.Read())
            {
                autor.CodAutor = dr["CodAutor"].ToString();
                autor.NombreAutor = dr["NombreAutor"].ToString();
                autor.Nacionalidad = dr["Nacionalidad"].ToString();
                autor.FechaNacimiento = dr["FechaNacimiento"] == DBNull.Value ? null : Convert.ToDateTime(dr["FechaNacimiento"]);
            }

            conexion.Close();
            return autor;
        }

        public int Ingresar(Autor autor)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Autores (CodAutor, NombreAutor, Nacionalidad, FechaNacimiento) VALUES (@cod, @nom, @nac, @fec)", conexion);
            cmd.Parameters.AddWithValue("@cod", autor.CodAutor);
            cmd.Parameters.AddWithValue("@nom", autor.NombreAutor);
            cmd.Parameters.AddWithValue("@nac", autor.Nacionalidad ?? "");
            cmd.Parameters.AddWithValue("@fec", autor.FechaNacimiento.HasValue ? (object)autor.FechaNacimiento.Value : DBNull.Value);

            int i = cmd.ExecuteNonQuery();
            conexion.Close();
            return i;
        }

        public int Modificar(Autor autor)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Autores SET NombreAutor=@nom, Nacionalidad=@nac, FechaNacimiento=@fec WHERE CodAutor=@cod", conexion);
            cmd.Parameters.AddWithValue("@cod", autor.CodAutor);
            cmd.Parameters.AddWithValue("@nom", autor.NombreAutor);
            cmd.Parameters.AddWithValue("@nac", autor.Nacionalidad ?? "");
            cmd.Parameters.AddWithValue("@fec", autor.FechaNacimiento.HasValue ? (object)autor.FechaNacimiento.Value : DBNull.Value);

            int i = cmd.ExecuteNonQuery();
            conexion.Close();
            return i;
        }

        public bool TieneLibrosAsociados(string codAutor)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LibroAutor WHERE CodAutor = @cod", conexion);
            cmd.Parameters.AddWithValue("@cod", codAutor);

            int cantidad = (int)cmd.ExecuteScalar();
            conexion.Close();

            return cantidad > 0;
        }
        public int Borrar(string cod)
        {
            if (TieneLibrosAsociados(cod))
                throw new Exception("Este autor tiene libros asociados y no puede ser eliminado.");

            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Autores WHERE CodAutor = @cod", conexion);
            cmd.Parameters.AddWithValue("@cod", cod);

            int i = cmd.ExecuteNonQuery();
            conexion.Close();
            return i;
        }

        

    }
}
