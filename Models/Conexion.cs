namespace GestionBiblioteca.Models
{
    public class Conexion
    {
        // Variable para definir la cadena de conexión
        private string cadConexion = string.Empty;

        public Conexion()
        {
            // Crear una variable que utilizará el archivo "appsettings.json"
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // Definir la cadena de conexión
            cadConexion = builder.GetSection("ConnectionStrings:CadConexion").Value;

        }

        public string getCadConexion()
        {
            return cadConexion;
        }
    }
}
