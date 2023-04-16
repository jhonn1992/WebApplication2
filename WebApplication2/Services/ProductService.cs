using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ProductService
    {
        private static string db_source = "devserverju.database.windows.net";
        private static string db_user = "sqladminuser";
        private static string db_password = "Mister2021*devserverju";
        private static string db_database = "az204CourseDB";

        private SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder _builder = new() 
            {
                DataSource = db_source,
                UserID = db_user,
                Password = db_password,
                InitialCatalog = db_database
            };
            return new SqlConnection( _builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _productList = new();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";

            conn.Open();

            SqlCommand cmd = new( statement, conn );
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _productList.Add(product);
                }
            }

            conn.Close();
            return _productList;
        }
    }
}
