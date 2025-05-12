using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;


namespace GestionBiblioteca.Models
{
    public class MantenimientoUsuario
    {
        private SqlConnection conexion;

        public List<Usuario> ListarTodos()
        {
            // Crear un objeto de tipo "Conexion" para iniciar la conexión a la base de datos "Biblioteca"
            Conexion conex = new Conexion();

            // Definimos una lista de tipo "Usuario"
            List<Usuario> ListaUsuarios = new List<Usuario>();

            // Definir la conexión a la BD
            conexion = new SqlConnection(conex.getCadConexion());

            conexion.Open();                // Abrimos la conexión

            // Definimos una variable para indicar las sentencias SQL a la tabla "Usuarios"
            SqlCommand comando = new SqlCommand("select * from Usuarios", conexion);

            // Definimos un objeto "DataReader" que almacenará los registros de la tabla "Usuarios"
            SqlDataReader Registros = comando.ExecuteReader();

            // Para cada registro
            while (Registros.Read())
            {
                // Creamos un objeto de tipo "Usuario"
                Usuario user = new Usuario()
                {
                    // Almacenamos los datos de cada registro en los atributos de la clase
                    CodUsuario = Registros["codUsuario"].ToString(),
                    Nombre = Registros["nombre"].ToString(),
                    Username = Registros["username"].ToString(),
                    Password = Registros["password"].ToString(),
                    FechaCreacion = Registros["fechaCreacion"].ToString()
                };

                // Agregamos el registro a la lista
                ListaUsuarios.Add(user);
            }
            conexion.Close();               // Cerramos la conexión
            return ListaUsuarios;
        }


        // Método para agregar registros a la tabla "Usuarios"
        // Recibe como parámetro un objeto de la clase "Usuario"
        public int Ingresar(Usuario user)
        {
            // Crear un objeto de tipo "Conexion" para iniciar la conexión a la base de datos "Biblioteca"
            Conexion conex = new Conexion();

            // Definir la conexión a la BD
            conexion = new SqlConnection(conex.getCadConexion());

            conexion.Open();  // Abrimos la conexión a la base de datos

            // Definimos una variable para indicar las sentencias SQL a la tabla "Usuarios"
            SqlCommand comando = new SqlCommand("insert into Usuarios(CodUsuario, Nombre, Username, Password, FechaCreacion) " +
                                                "values(@codUsuario, @nombre, @username, @password, @fechaCreacion)", conexion);

            // Definimos los tipos y valores parametrizados
            comando.Parameters.Add("@codUsuario", SqlDbType.VarChar);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@username", SqlDbType.VarChar);
            comando.Parameters.Add("@password", SqlDbType.VarChar);
            comando.Parameters.Add("@fechaCreacion", SqlDbType.VarChar);

            // Pasamos los datos de los campos a los parámetros de la instrucción SQL
            comando.Parameters["@codUsuario"].Value = user.CodUsuario;
            comando.Parameters["@nombre"].Value = user.Nombre;
            comando.Parameters["@username"].Value = user.Username;
            comando.Parameters["@password"].Value = user.Password;
            comando.Parameters["@fechaCreacion"].Value = user.FechaCreacion;

            int i = comando.ExecuteNonQuery(); // Ejecutamos la instrucción SQL
            conexion.Close(); // Cerramos la conexión
            return i; // Retornamos el número de filas afectadas
        }

        //Método para consultar la tabla "Usuarios"
        public Usuario Consultar(string codUsuario)
        {
            // Llamamos al método "Conectar" para iniciar la variable "conexion" con la conexión a la base de datos "Biblioteca"
            Conexion conex = new Conexion();

            // Definir la conexión a la BD
            conexion = new SqlConnection(conex.getCadConexion());

            conexion.Open();                 // Abrimos la conexión

            SqlCommand comando = new SqlCommand("select * from Usuarios where CodUsuario = @codUsuario", conexion);

            // Definimos los tipos y valores parametrizados:
            comando.Parameters.AddWithValue("@codUsuario", codUsuario);

            /* Definimos un objeto "DataReader" que almacenará los registros generados por la
               consulta "select" a la tabla "Usuarios" */
            SqlDataReader Registros = comando.ExecuteReader();

            // Creamos un objeto de tipo "Usuario"
            Usuario user = new Usuario();

            // Para cada registro en el DataReader
            if (Registros.Read())
            {
                // Asignamos valores a los atributos del objeto de tipo "Usuario"
                user.CodUsuario = Registros["codUsuario"].ToString();
                user.Nombre = Registros["nombre"].ToString();
                user.Username = Registros["username"].ToString();
                user.Password = Registros["password"].ToString();
                user.FechaCreacion = Registros["fechaCreacion"].ToString();
            }

            conexion.Close(); // Cerramos la conexión
            return user; // Retornamos el objeto de tipo "Usuario"
        }

        // Método para editar registros de la tabla "Usuarios"
        public int Modificar(Usuario user)
        {
            // Crear un objeto de tipo "Conexion" para iniciar la conexión a la base de datos "Biblioteca"
            Conexion conex = new Conexion();

            // Definir la conexión a la BD
            conexion = new SqlConnection(conex.getCadConexion());

            conexion.Open();    // Abrimos la conexión

            // Definimos una variable para indicar las sentencias SQL a la tabla "Usuarios"
            SqlCommand comando = new SqlCommand("update Usuarios set CodUsuario=@codUsuario, Nombre = @nombre, Username = @username, " +
                                                "Password = @password, FechaCreacion = @fechaCreacion where CodUsuario = @codUsuario", conexion);

            // Definimos los tipos y valores parametrizados
            comando.Parameters.AddWithValue("@nombre", user.Nombre);
            comando.Parameters.AddWithValue("@username", user.Username);
            comando.Parameters.AddWithValue("@password", user.Password);
            comando.Parameters.AddWithValue("@fechaCreacion", user.FechaCreacion);
            comando.Parameters.AddWithValue("@codUsuario", user.CodUsuario);

            // Ejecutamos el comando "update" a la tabla "Usuarios"
            int i = comando.ExecuteNonQuery();

            conexion.Close();   // Cerramos la conexión

            return i; // Retornamos el número de filas afectadas
        }

        // Método para eliminar registros de la tabla "Usuarios"
        public int Borrar(string codUsuario)
        {
            // Crear un objeto de tipo "Conexion" para iniciar la conexión a la base de datos "Biblioteca"
            Conexion conex = new Conexion();

            // Definir la conexión a la BD
            conexion = new SqlConnection(conex.getCadConexion());

            conexion.Open();    // Abrimos la conexión

            // Definimos una variable para indicar las sentencias SQL a la tabla "Usuarios"
            SqlCommand comando = new SqlCommand("delete from Usuarios where CodUsuario = @codUsuario", conexion);

            // Definimos los tipos y valores parametrizados:
            comando.Parameters.AddWithValue("@codUsuario", codUsuario);

            // Ejecutamos el comando "delete" a la tabla "Usuarios"
            int i = comando.ExecuteNonQuery();

            conexion.Close();   // Cerramos la conexión

            return i;// Retornamos el número de filas afectadas
        }

    }
}
