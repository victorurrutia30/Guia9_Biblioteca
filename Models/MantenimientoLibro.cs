using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionBiblioteca.Models
{
    public class MantenimientoLibro
    {
        private SqlConnection conexion;

        // Listar todos los libros
        public List<Libros> ListarTodos()
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            List<Libros> lista = new List<Libros>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Libros", conexion);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Libros
                {
                    CodLibro = dr["CodLibro"].ToString(),
                    Titulo = dr["Titulo"].ToString(),
                    Editorial = dr["Editorial"].ToString(),
                    AnioPublicacion = dr["AnioPublicacion"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["AnioPublicacion"]),
                    ISBN = dr["ISBN"].ToString(),
                    FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"])
                });
            }

            conexion.Close();
            return lista;
        }

        // Consultar un libro por código
        public Libros Consultar(string cod)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Libros WHERE CodLibro = @cod", conexion);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            Libros libro = new Libros();
            if (dr.Read())
            {
                libro.CodLibro = dr["CodLibro"].ToString();
                libro.Titulo = dr["Titulo"].ToString();
                libro.Editorial = dr["Editorial"].ToString();
                libro.AnioPublicacion = dr["AnioPublicacion"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["AnioPublicacion"]);
                libro.ISBN = dr["ISBN"].ToString();
                libro.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
            }

            conexion.Close();
            return libro;
        }

        // Insertar nuevo libro + autores
        public int Ingresar(Libros libro, List<string> codAutores)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Libros (CodLibro, Titulo, Editorial, AnioPublicacion, ISBN, FechaIngreso) " +
                                                "VALUES (@cod, @titulo, @editorial, @anio, @isbn, @fecha)", conexion, transaccion);

                cmd.Parameters.AddWithValue("@cod", libro.CodLibro);
                cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@editorial", libro.Editorial ?? "");
                cmd.Parameters.AddWithValue("@anio", libro.AnioPublicacion.HasValue ? libro.AnioPublicacion.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@isbn", libro.ISBN ?? "");
                cmd.Parameters.AddWithValue("@fecha", libro.FechaIngreso);
                cmd.ExecuteNonQuery();

                foreach (var codAutor in codAutores)
                {
                    SqlCommand cmdAutor = new SqlCommand("INSERT INTO LibroAutor (CodLibro, CodAutor) VALUES (@codLibro, @codAutor)", conexion, transaccion);
                    cmdAutor.Parameters.AddWithValue("@codLibro", libro.CodLibro);
                    cmdAutor.Parameters.AddWithValue("@codAutor", codAutor);
                    cmdAutor.ExecuteNonQuery();
                }

                transaccion.Commit();
                conexion.Close();
                return 1;
            }
            catch
            {
                transaccion.Rollback();
                conexion.Close();
                return 0;
            }
        }

        // Modificar libro + autores
        public int Modificar(Libros libro, List<string> codAutores)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Libros SET Titulo = @titulo, Editorial = @editorial, AnioPublicacion = @anio, " +
                                                "ISBN = @isbn, FechaIngreso = @fecha WHERE CodLibro = @cod", conexion, transaccion);

                cmd.Parameters.AddWithValue("@cod", libro.CodLibro);
                cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@editorial", libro.Editorial ?? "");
                cmd.Parameters.AddWithValue("@anio", libro.AnioPublicacion.HasValue ? libro.AnioPublicacion.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@isbn", libro.ISBN ?? "");
                cmd.Parameters.AddWithValue("@fecha", libro.FechaIngreso);
                cmd.ExecuteNonQuery();

                SqlCommand eliminarAutores = new SqlCommand("DELETE FROM LibroAutor WHERE CodLibro = @cod", conexion, transaccion);
                eliminarAutores.Parameters.AddWithValue("@cod", libro.CodLibro);
                eliminarAutores.ExecuteNonQuery();

                foreach (var codAutor in codAutores)
                {
                    SqlCommand cmdAutor = new SqlCommand("INSERT INTO LibroAutor (CodLibro, CodAutor) VALUES (@codLibro, @codAutor)", conexion, transaccion);
                    cmdAutor.Parameters.AddWithValue("@codLibro", libro.CodLibro);
                    cmdAutor.Parameters.AddWithValue("@codAutor", codAutor);
                    cmdAutor.ExecuteNonQuery();
                }

                transaccion.Commit();
                conexion.Close();
                return 1;
            }
            catch
            {
                transaccion.Rollback();
                conexion.Close();
                return 0;
            }
        }

        // Borrar libro y sus autores asociados
        public int Borrar(string cod)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                SqlCommand borrarAutores = new SqlCommand("DELETE FROM LibroAutor WHERE CodLibro = @cod", conexion, transaccion);
                borrarAutores.Parameters.AddWithValue("@cod", cod);
                borrarAutores.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("DELETE FROM Libros WHERE CodLibro = @cod", conexion, transaccion);
                cmd.Parameters.AddWithValue("@cod", cod);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
                conexion.Close();
                return 1;
            }
            catch
            {
                transaccion.Rollback();
                conexion.Close();
                return 0;
            }
        }

        // Obtener autores asociados a un libro
        public List<string> ObtenerAutoresPorLibro(string codLibro)
        {
            Conexion conex = new Conexion();
            conexion = new SqlConnection(conex.getCadConexion());
            conexion.Open();

            List<string> autores = new List<string>();

            SqlCommand cmd = new SqlCommand(
                "SELECT A.NombreAutor " +
                "FROM LibroAutor LA " +
                "INNER JOIN Autores A ON LA.CodAutor = A.CodAutor " +
                "WHERE LA.CodLibro = @cod", conexion);

            cmd.Parameters.AddWithValue("@cod", codLibro);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                autores.Add(dr["NombreAutor"].ToString());
            }

            conexion.Close();
            return autores;
        }
    }
}
